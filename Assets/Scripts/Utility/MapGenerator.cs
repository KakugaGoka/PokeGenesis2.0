using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour {

    // Variables for map generation
    public string 
        seed;

    public bool 
        useRandomSeed = true;

    [Range(100, 300)]
    public int
        width = 300,
        height = 300;

    [Range(35, 55)]
    public int
        randomFillPercent = 35;

    public int
        smoothingIterations = 5,
        wallSizeVariance = 4,
        borderSize = 5,
        wallSizeThreshold = 50,
        roomSizeThreshold = 50,
        passageRadius = 3;

    public Texture2D 
        mapTexture;

    private 
        System.Random psuedoRandom;

    private int[,] 
        map;

    private bool
        fillingMap = true,
        placeingEntrance = true,
        smoothingMap = true,
        processingMap = true;

    public void GenerateMap() {
        map = new int[width, height];

        if (useRandomSeed) {
            seed = Time.time.ToString();
        }
        psuedoRandom = new System.Random(seed.GetHashCode());

        RandomFillMap();
        //PlaceEntranceAndExit();
        SmoothMap();
        ProcessMap();
        FinishMap();
    }

    public void FinishMap() {
        int[,] borderedMap = new int[width + borderSize * 2, height + borderSize * 2];

        for (int x = 0; x < borderedMap.GetLength(0); x++) {
            for (int y = 0; y < borderedMap.GetLength(1); y++) {
                if (x >= borderSize && x < width + borderSize && y >= borderSize && y < height + borderSize) {
                    borderedMap[x, y] = map[x - borderSize, y - borderSize];
                } else {
                    borderedMap[x, y] = 1;
                }
            }
        }

        mapTexture = Array2DToTexture2D(borderedMap);
    }

    public void ProcessMap() {
        List<List<Coord>> roomRegions = GetRegion(0);
        List<Room> survivingRooms = new List<Room>();

        foreach (List<Coord> roomRegion in roomRegions) {
            if (roomRegion.Count < roomSizeThreshold) {
                foreach (Coord tile in roomRegion) {
                    map[tile.tileX, tile.tileY] = 1;
                }
            } else {
                survivingRooms.Add(new Room(roomRegion, map));
            }

            if (survivingRooms.Count > 0) {
                survivingRooms.Sort();
                survivingRooms[0].isMainRoom = true;
                survivingRooms[0].isAccessibleFromMainRoom = true;

                ConnectClosetsRooms(survivingRooms);
            } else {
                roomSizeThreshold--;
                randomFillPercent--;
                GenerateMap();
            }

            List<List<Coord>> wallRegions = GetRegion(1);

            foreach (List<Coord> wallRegion in wallRegions) {
                if (wallRegion.Count < wallSizeThreshold) {
                    foreach (Coord tile in wallRegion) {
                        map[tile.tileX, tile.tileY] = 0;
                    }
                }
            }
        }
    }

    public void PlaceEntranceAndExit() {
        int radius = (int)roomSizeThreshold / 5;
        int x = psuedoRandom.Next(radius + 1, width - (radius + 1));
        int y = psuedoRandom.Next(radius + (height / 2), height - (radius + 1));
        Coord startTile = new Coord(x, y);
        Coord playerTile = new Coord(startTile.tileX, startTile.tileY - 2);

        DrawCircle(startTile, radius);

        x = psuedoRandom.Next(radius + 1, width - (radius + 1));
        y = psuedoRandom.Next(radius + 1, height - (radius + (height / 2)));
        Coord endTile = new Coord(x, y);
        DrawCircle(endTile, radius);

    }

    public void ConnectClosetsRooms(List<Room> allRooms, bool forceAccessibilityFromMainRoom = false) {
        List<Room> roomListA = new List<Room>();
        List<Room> roomListB = new List<Room>();

        if (forceAccessibilityFromMainRoom) {
            foreach (Room room in allRooms) {
                if (room.isAccessibleFromMainRoom) {
                    roomListB.Add(room);
                } else {
                    roomListA.Add(room);
                }
            }
        } else {
            roomListA = allRooms;
            roomListB = allRooms;
        }

        int bestDistance = 0;
        Coord bestTileA = new Coord();
        Coord bestTileB = new Coord();
        Room bestRoomA = new Room();
        Room bestRoomB = new Room();
        bool possibleConnectionFound = false;

        foreach (Room roomA in roomListA) {
            if (!forceAccessibilityFromMainRoom) {
                possibleConnectionFound = false;

                if (roomA.connectedRooms.Count > 0) {
                    continue;
                }
            }

            foreach (Room roomB in roomListB) {
                if (roomA == roomB || roomA.IsConnected(roomB)) {
                    continue;
                }

                for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA++) {
                    for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB++) {
                        Coord tileA = roomA.edgeTiles[tileIndexA];
                        Coord tileB = roomB.edgeTiles[tileIndexB];

                        int distanceBetweenRooms = (int)(Mathf.Pow(tileA.tileX - tileB.tileX, 2) + Mathf.Pow(tileA.tileY - tileB.tileY, 2));

                        if (distanceBetweenRooms < bestDistance || !possibleConnectionFound) {
                            bestDistance = distanceBetweenRooms;
                            possibleConnectionFound = true;
                            bestTileA = tileA;
                            bestTileB = tileB;
                            bestRoomA = roomA;
                            bestRoomB = roomB;
                        }
                    }
                }
            }

            if (possibleConnectionFound && !forceAccessibilityFromMainRoom) {
                CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
            }
        }

        if (possibleConnectionFound && forceAccessibilityFromMainRoom) {
            CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
            ConnectClosetsRooms(allRooms, true);
        }

        if (!forceAccessibilityFromMainRoom) {
            ConnectClosetsRooms(allRooms, true);
        }
    }

    public void CreatePassage(Room roomA, Room roomB, Coord tileA, Coord tileB) {
        Room.ConnectRooms(roomA, roomB);
        Debug.DrawLine(CoordToWorldPoint(tileA), CoordToWorldPoint(tileB), Color.green, 100);

        List<Coord> line = GetLine(tileA, tileB);
        foreach (Coord c in line) {
            DrawCircle(c, passageRadius);
        }
    }

    public void DrawCircle(Coord c, int r) {
        for (int x = -r; x <= r; x++) {
            for (int y = -r; y <= r; y++) {
                if (x * x + y * y <= r * r) {
                    int drawX = c.tileX + x;
                    int drawY = c.tileY + y;
                    if (IsInMapRange(drawX, drawY)) {
                        map[drawX, drawY] = 0;
                    }
                }
            }
        }
    }

    public List<Coord> GetLine(Coord from, Coord to) {
        List<Coord> line = new List<Coord>();

        int x = from.tileX;
        int y = from.tileY;

        int dx = to.tileX - from.tileX;
        int dy = to.tileY - from.tileY;

        bool inverted = false;
        int step = Math.Sign(dx);
        int gradientStep = Math.Sign(dy);

        int longest = Mathf.Abs(dx);
        int shortest = Mathf.Abs(dy);

        if (longest < shortest) {
            inverted = true;
            longest = Mathf.Abs(dy);
            shortest = Mathf.Abs(dx);

            step = Math.Sign(dy);
            gradientStep = Math.Sign(dx);
        }

        int gradientAccumulation = longest / 2;
        for (int i = 0; i < longest; i++) {
            line.Add(new Coord(x, y));

            if (inverted) {
                y += step;
            } else {
                x += step;
            }

            gradientAccumulation += shortest;
            if (gradientAccumulation >= longest) {
                if (inverted) {
                    x += gradientStep;
                } else {
                    y += gradientStep;

                }
                gradientAccumulation -= longest;
            }
        }

        return line;
    }

    public Vector3 CoordToWorldPoint(Coord tile) {
        return new Vector3(-width / 2 + 0.5f + tile.tileX, 0, -height / 2 + 0.5f + tile.tileY);
    }

    public List<List<Coord>> GetRegion(int tileType) {
        List<List<Coord>> regions = new List<List<Coord>>();
        int[,] mapFlags = new int[width, height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (mapFlags[x, y] == 0 && map[x, y] == tileType) {
                    List<Coord> newRegion = GetRegionTiles(x, y);
                    regions.Add(newRegion);

                    foreach (Coord tile in newRegion) {
                        mapFlags[tile.tileX, tile.tileY] = 1;
                    }
                }
            }
        }

        return regions;
    }

    public List<Coord> GetRegionTiles(int startX, int startY) {
        List<Coord> tiles = new List<Coord>();
        int[,] mapFlags = new int[width, height];
        int tileType = map[startX, startY];

        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(new Coord(startX, startY));
        mapFlags[startX, startY] = 1;

        while (queue.Count > 0) {
            Coord tile = queue.Dequeue();
            tiles.Add(tile);

            for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++) {
                for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++) {
                    if (IsInMapRange(x, y) && (y == tile.tileY || x == tile.tileX)) {
                        if (mapFlags[x, y] == 0 && map[x, y] == tileType) {
                            mapFlags[x, y] = 1;
                            queue.Enqueue(new Coord(x, y));
                        }
                    }
                }
            }
        }

        return tiles;
    }

    public bool IsInMapRange(int x, int y) {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    public void RandomFillMap() {
        // 0 == floor
        // 1 == wall
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1) {
                    map[x, y] = 1;
                } else {
                    map[x, y] = (psuedoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }
    }

    public void SmoothMap() {
        for (int i = 0; i < smoothingIterations; i++) {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    int neighbotWallTiles = GetWallCount(x, y);

                    if (neighbotWallTiles > wallSizeVariance)
                        map[x, y] = 1;
                    else if (neighbotWallTiles < wallSizeVariance)
                        map[x, y] = 0;
                }
            }
        }
    }

    public int GetWallCount(int gridX, int gridY) {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++) {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++) {
                if (IsInMapRange(neighbourX, neighbourY)) {
                    if (neighbourX != gridX || neighbourY != gridY) {
                        wallCount += map[neighbourX, neighbourY];
                    }
                } else {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }

    public Texture2D Array2DToTexture2D(int[,] array) {
        Texture2D texture = new Texture2D(array.GetLength(0), array.GetLength(1), TextureFormat.ARGB32, false);
        for (int x = 0; x < array.GetLength(0); x++) {
            for (int y = 0; y < array.GetLength(1); y++) {
                texture.SetPixel(x, y, array[x, y] == 1 ? Color.black : Color.white);
            }
        }
        texture.Apply();
        return texture;
    }

    public struct Coord {
        public int tileX;
        public int tileY;

        public Coord(int x, int y) {
            tileX = x;
            tileY = y;
        }
    }

    public class Room : IComparable<Room> {
        public List<Coord> tiles;
        public List<Coord> edgeTiles;
        public List<Room> connectedRooms;
        public int roomSize;
        public bool isAccessibleFromMainRoom;
        public bool isMainRoom;

        public Room() { }

        public Room(List<Coord> roomTiles, int[,] map) {
            tiles = roomTiles;
            roomSize = tiles.Count;
            connectedRooms = new List<Room>();
            edgeTiles = new List<Coord>();

            foreach (Coord tile in tiles) {
                for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++) {
                    for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++) {
                        if (x == tile.tileX || y == tile.tileY) {
                            if (map[x, y] == 1) {
                                edgeTiles.Add(tile);
                            }
                        }
                    }
                }
            }
        }

        public static void ConnectRooms(Room roomA, Room roomB) {
            if (roomA.isAccessibleFromMainRoom) {
                roomB.SetAccessibleFromMainRoom();
            } else if (roomB.isAccessibleFromMainRoom) {
                roomA.SetAccessibleFromMainRoom();
            }

            roomA.connectedRooms.Add(roomB);
            roomB.connectedRooms.Add(roomA);
        }

        public bool IsConnected(Room otherRoom) {
            return connectedRooms.Contains(otherRoom);
        }

        public int CompareTo(Room otherRoom) {
            return otherRoom.roomSize.CompareTo(roomSize);
        }

        public void SetAccessibleFromMainRoom() {
            if (!isAccessibleFromMainRoom) {
                isAccessibleFromMainRoom = true;
                foreach (Room connectedRoom in connectedRooms) {
                    connectedRoom.SetAccessibleFromMainRoom();
                }
            }
        }

        public void PlaceObject(Coord tile, GameObject objectToPlace) {
            objectToPlace.transform.position = new Vector3(tile.tileX, 0, tile.tileY);
        }
    }
}
