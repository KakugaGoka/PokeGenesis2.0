using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PokedexManager : MonoBehaviour {
    static public PokedexManager manager;
    static public Pokemon[] pokedex;
    static public PokemonType[] types;
    static public Nature[] natures;
    static public string[] habitats;
    static public Item[] items;
    static public Pokemon currentPokemon;
    static public GameObject currentEntry;

    // Start is called before the first frame update
    void Awake() {
        if (manager == null) {
            manager = this;
        } else {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        // Load Pokemon From JSON
        string pokemonString = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json"));
        pokedex = JsonHelper.FromJson<Pokemon>(pokemonString);
        pokedex = pokedex.OrderBy(x => x.number).ToArray();
        Debug.Log("Pokedex Count: " + pokedex.Count());

        // Load in Types from JSON
        string typesString = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/PokemonTypes.json"));
        types = JsonHelper.FromJson<PokemonType>(typesString);
        types = types.OrderBy(x => x.typeName).ToArray();
        Debug.Log("Types Count: " + types.Count());

        // Load in Natures from JSON
        string naturesString = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Natures.json"));
        natures = JsonHelper.FromJson<Nature>(naturesString);
        natures = natures.OrderBy(x => x.name).ToArray();
        Debug.Log("Natures Count: " + natures.Count());

        // Load in Habitats from JSON
        string habitatsString = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Habitats.json"));
        habitats = JsonHelper.FromJson<string>(habitatsString);
        habitats = habitats.OrderBy(x => x).ToArray();
        Debug.Log("Habitats Count: " + habitats.Count());

        // Load in Items from JSON
        string itemsString = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Items.json"));
        Item[] tempItems = JsonHelper.FromJson<Item>(itemsString);
        List<Item> itemsList = new List<Item>();
        for (int i = 0; i < tempItems.Length; i++) {
            if (tempItems[i].tier == 1) {
                itemsList.Add(tempItems[i]);
                itemsList.Add(tempItems[i]);
                itemsList.Add(tempItems[i]);
                itemsList.Add(tempItems[i]);
            } else if (tempItems[i].tier == 2) {
                itemsList.Add(tempItems[i]);
                itemsList.Add(tempItems[i]);
                itemsList.Add(tempItems[i]);
            } else if (tempItems[i].tier == 3) {
                itemsList.Add(tempItems[i]);
                itemsList.Add(tempItems[i]);
            } else if (tempItems[i].tier == 4) {
                itemsList.Add(tempItems[i]);
            }
        }
        items = itemsList.ToArray();
        Debug.Log("Items Count: " + items.Count());
    }

    public void ChangeScene(int sceneID) {
        SceneManager.LoadScene(sceneID);
    }

    static public Sprite LoadSprite(string path) {
        string fullPath = Path.Combine(Application.streamingAssetsPath, path + ".png");
        if (!File.Exists(fullPath)) {
            Debug.LogError("Faield to load sprite at: " + fullPath);
            return null;
        }
        byte[] bytes = File.ReadAllBytes(fullPath);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));

        return sprite;
    }
}
