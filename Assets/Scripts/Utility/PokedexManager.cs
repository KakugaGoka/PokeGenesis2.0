using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PokedexManager : MonoBehaviour {
    static public PokedexManager manager;
    static public Pokemon[] pokedex;
    static public PokemonType[] types;
    static public Nature[] natures;
    static public string[] habitats;
    static public Item[] items;
    static public TM[] TMs;
    static public Ability[] abilities;
    static public Move[] moves;
    static public Info[] skillsInfo;
    static public Info[] capabilitiesInfo;
    static public Info[] conditionsInfo;

    static public Pokemon currentPokemon;
    static public GameObject currentEntry;
    static public List<Pokemon> pokemonToEncounter = new List<Pokemon>();
    static public bool networkAvailable;
    static public bool readyToLoadJSONs = false;

    static public Color
        frontGrey = new Color(0.8f, 0.8f, 0.8f, 1f),
        backGrey = new Color(0.5f, 0.5f, 0.5f, 1f);

    static public string dataPath;

    public GameObject warningBox;
    public GameObject confirmationBox;
    public GameObject tooltipBox;
    public GameObject sendingBox;
    public GameObject editDialogBox;
    public GameObject pokedexPrefab;
    public GameObject movePrefab;

    // Start is called before the first frame update
    void Awake() {
        if (manager == null) {
            manager = this;
        } else {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        dataPath = Application.persistentDataPath;
        if (!Directory.Exists(Path.Combine(dataPath, "Captured/"))) {
            Directory.CreateDirectory(Path.Combine(dataPath, "Captured/"));
        }
        if (!Directory.Exists(Path.Combine(dataPath, "tmp/"))) {
            Directory.CreateDirectory(Path.Combine(dataPath, "tmp/"));
        }
        WriteOutJSONs();
        WriteOutCries();
        WriteOutIcons();
        readyToLoadJSONs = true;
    }

    public static void WriteOutJSONs() {
        if (!Directory.Exists(Path.Combine(dataPath, "JSON/"))) {
            Directory.CreateDirectory(Path.Combine(dataPath, "JSON/"));
        }
        TextAsset currentJSON;
        // Pokedex
        if (!File.Exists(Path.Combine(dataPath, "JSON/Pokemon.json"))) {
            currentJSON = Resources.Load<TextAsset>("JSON/Pokemon");
            File.WriteAllText(Path.Combine(dataPath, "JSON/Pokemon.json"), currentJSON.text);
        }
        // Moves
        if (!File.Exists(Path.Combine(dataPath, "JSON/Moves.json"))) {
            currentJSON = Resources.Load<TextAsset>("JSON/Moves");
            File.WriteAllText(Path.Combine(dataPath, "JSON/Moves.json"), currentJSON.text);
        }
        // Capabilities
        if (!File.Exists(Path.Combine(dataPath, "JSON/Capabilities.json"))) {
            currentJSON = Resources.Load<TextAsset>("JSON/Capabilities");
            File.WriteAllText(Path.Combine(dataPath, "JSON/Capabilities.json"), currentJSON.text);
        }
        // Abilities
        if (!File.Exists(Path.Combine(dataPath, "JSON/Abilities.json"))) {
            currentJSON = Resources.Load<TextAsset>("JSON/Abilities");
            File.WriteAllText(Path.Combine(dataPath, "JSON/Abilities.json"), currentJSON.text);
        }
        // Conditions
        if (!File.Exists(Path.Combine(dataPath, "JSON/Conditions.json"))) {
            currentJSON = Resources.Load<TextAsset>("JSON/Conditions");
            File.WriteAllText(Path.Combine(dataPath, "JSON/Conditions.json"), currentJSON.text);
        }
        // Items
        if (!File.Exists(Path.Combine(dataPath, "JSON/Items.json"))) {
            currentJSON = Resources.Load<TextAsset>("JSON/Items");
            File.WriteAllText(Path.Combine(dataPath, "JSON/Items.json"), currentJSON.text);
        }
        // Natures
        if (!File.Exists(Path.Combine(dataPath, "JSON/Natures.json"))) {
            currentJSON = Resources.Load<TextAsset>("JSON/Natures");
            File.WriteAllText(Path.Combine(dataPath, "JSON/Natures.json"), currentJSON.text);
        }
        // PokemonTypes
        if (!File.Exists(Path.Combine(dataPath, "JSON/PokemonTypes.json"))) {
            currentJSON = Resources.Load<TextAsset>("JSON/PokemonTypes");
            File.WriteAllText(Path.Combine(dataPath, "JSON/PokemonTypes.json"), currentJSON.text);
        }
        // TMs
        if (!File.Exists(Path.Combine(dataPath, "JSON/TMs.json"))) {
            currentJSON = Resources.Load<TextAsset>("JSON/TMs");
            File.WriteAllText(Path.Combine(dataPath, "JSON/TMs.json"), currentJSON.text);
        }
        // Habitats
        if (!File.Exists(Path.Combine(dataPath, "JSON/Habitats.json"))) {
            currentJSON = Resources.Load<TextAsset>("JSON/Habitats");
            File.WriteAllText(Path.Combine(dataPath, "JSON/Habitats.json"), currentJSON.text);
        }
        // Skills
        if (!File.Exists(Path.Combine(dataPath, "JSON/Skills.json"))) {
            currentJSON = Resources.Load<TextAsset>("JSON/Skills");
            File.WriteAllText(Path.Combine(dataPath, "JSON/Skills.json"), currentJSON.text);
        }
    }

    public static void WriteOutCries() {
        if (!Directory.Exists(Path.Combine(dataPath, "Cries/"))) {
            Directory.CreateDirectory(Path.Combine(dataPath, "Cries/"));
        }

        // Write all items icons to file
        TextAsset[] cries = Resources.LoadAll<TextAsset>("Cries/");
        Debug.Log(cries.Length);
        foreach (var cry in cries) {
            if (!File.Exists(Path.Combine(dataPath, "Cries/", cry.name + ".ogg"))) {
                byte[] bytes = cry.bytes;
                File.WriteAllBytes(Path.Combine(dataPath, "Cries/", cry.name + ".ogg"), bytes);
            }
        }

    }

    public static void WriteOutIcons() {
        if (!Directory.Exists(Path.Combine(dataPath, "Icons/Items/"))) {
            Directory.CreateDirectory(Path.Combine(dataPath, "Icons/"));
        }
        if (!Directory.Exists(Path.Combine(dataPath, "Icons/Items/"))) {
            Directory.CreateDirectory(Path.Combine(dataPath, "Icons/Items/"));
        }
        if (!Directory.Exists(Path.Combine(dataPath, "Icons/Pokemon/"))) {
            Directory.CreateDirectory(Path.Combine(dataPath, "Icons/Pokemon/"));
        }
        // Write all items icons to file
        Texture2D[] itemSprites = Resources.LoadAll<Texture2D>("Icons/Items/");
        foreach (var sprite in itemSprites) {
            if (!File.Exists(Path.Combine(dataPath, "Icons/Items/", sprite.name + ".png"))) {
                byte[] bytes = sprite.EncodeToPNG();
                File.WriteAllBytes(Path.Combine(dataPath, "Icons/Items/", sprite.name + ".png"), bytes);
            }
        }

        // Write all items icons to file
        Texture2D[] pokeSprites = Resources.LoadAll<Texture2D>("Icons/Pokemon/");
        foreach (var sprite in pokeSprites) {
            if (!File.Exists(Path.Combine(dataPath, "Icons/Pokemon/", sprite.name + ".png"))) {
                byte[] bytes = sprite.EncodeToPNG();
                File.WriteAllBytes(Path.Combine(dataPath, "Icons/Pokemon/", sprite.name + ".png"), bytes);
            }
        }
    }

    private void Update() {
        networkAvailable = Application.internetReachability != NetworkReachability.NotReachable;
        if (readyToLoadJSONs) {
            // Load Pokemon From JSON
            string pokemonString = File.ReadAllText(Path.Combine(dataPath, "JSON/Pokemon.json"));
            pokedex = JsonHelper.FromJson<Pokemon>(pokemonString);
            pokedex = pokedex.OrderBy(x => x.number).ToArray();
            Debug.Log("Pokedex Count: " + pokedex.Count());

            // Load in Types from JSON
            string typesString = File.ReadAllText(Path.Combine(dataPath, "JSON/PokemonTypes.json"));
            types = JsonHelper.FromJson<PokemonType>(typesString);
            types = types.OrderBy(x => x.typeName).ToArray();
            Debug.Log("Types Count: " + types.Count());

            // Load in Natures from JSON
            string naturesString = File.ReadAllText(Path.Combine(dataPath, "JSON/Natures.json"));
            natures = JsonHelper.FromJson<Nature>(naturesString);
            natures = natures.OrderBy(x => x.name).ToArray();
            Debug.Log("Natures Count: " + natures.Count());

            // Load in Habitats from JSON
            string habitatsString = File.ReadAllText(Path.Combine(dataPath, "JSON/Habitats.json"));
            habitats = JsonHelper.FromJson<string>(habitatsString);
            habitats = habitats.OrderBy(x => x).ToArray();
            Debug.Log("Habitats Count: " + habitats.Count());

            // Load in Items from JSON
            string itemsString = File.ReadAllText(Path.Combine(dataPath, "JSON/Items.json"));
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

            string tmString = File.ReadAllText(Path.Combine(dataPath, "JSON/TMs.json"));
            TMs = JsonHelper.FromJson<TM>(tmString);
            TMs = TMs.OrderBy(x => x.number).ToArray();
            Debug.Log("TM Count: " + TMs.Count());

            string abilityString = File.ReadAllText(Path.Combine(dataPath, "JSON/Abilities.json"));
            abilities = JsonHelper.FromJson<Ability>(abilityString);
            abilities = abilities.OrderBy(x => x.name).ToArray();
            Debug.Log("Ability Count: " + abilities.Count());

            string moveString = File.ReadAllText(Path.Combine(dataPath, "JSON/Moves.json"));
            moves = JsonHelper.FromJson<Move>(moveString);
            moves = moves.OrderBy(x => x.name).ToArray();
            foreach (var move in moves) {
                foreach (var type in types) {
                    if (move.typeName == type.typeName) {
                        move.type = type;
                        break;
                    }
                }
                if (move.type.typeName == null && move.typeName != "--") {
                    Debug.LogError("Move \"" + move.name + "\" could not be assinged a full type value for the type of: \"" + move.typeName + "\"");
                }
            }
            Debug.Log("Move Count: " + TMs.Count());

            string skillString = File.ReadAllText(Path.Combine(dataPath, "JSON/Skills.json"));
            skillsInfo = JsonHelper.FromJson<Info>(skillString);
            skillsInfo = skillsInfo.OrderBy(x => x.name).ToArray();
            Debug.Log("Skill Info Count: " + skillsInfo.Count());

            string capabilityString = File.ReadAllText(Path.Combine(dataPath, "JSON/Capabilities.json"));
            capabilitiesInfo = JsonHelper.FromJson<Info>(capabilityString);
            capabilitiesInfo = capabilitiesInfo.OrderBy(x => x.name).ToArray();
            Debug.Log("Capability Info Count: " + capabilitiesInfo.Count());

            string conditionString = File.ReadAllText(Path.Combine(dataPath, "JSON/Conditions.json"));
            conditionsInfo = JsonHelper.FromJson<Info>(conditionString);
            conditionsInfo = conditionsInfo.OrderBy(x => x.name).ToArray();
            Debug.Log("Condition Info Count: " + conditionsInfo.Count());

            readyToLoadJSONs = false;
        }
    }

    public void ChangeScene(int sceneID) {
#if UNITY_ANDROID == false
        readyToLoadJSONs = true;
#endif
        SceneManager.LoadScene(sceneID);
    }

    public void Quit() {
        CreateConfirmationDialog("Are you sure you wish to quit?", ConfirmationType.quit);
    }

    public int GetHighestNumberInPokemonArray(Pokemon[] pokemonArray) {
        int maxNumber = 0;
        foreach (var pokemon in pokemonArray) {
            if (pokemon.number > maxNumber) { maxNumber = pokemon.number; }
        }
        return maxNumber;
    }

    static public Sprite LoadSprite(string path) {
        string fullPath = Path.Combine(dataPath, path + ".png");
        if (!File.Exists(fullPath)) {
            Debug.LogError("Failed to load sprite at: " + fullPath);
            return null;
        }
        byte[] bytes = File.ReadAllBytes(fullPath);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));

        return sprite;
    }

    private void OnApplicationQuit() {
        // Purge temp pokemon if they exist. 
        var myFiles = Directory.EnumerateFiles(dataPath + "/tmp/", "*.json", SearchOption.TopDirectoryOnly);

        foreach (var file in myFiles) {
            File.Delete(file);
        }
    }

    static public void AssignCurrentPokemonAndEntry(GameObject entry) {
        if (currentEntry != null) {
            Button oldButton = currentEntry.GetComponent<PokedexEntry>().background;
            Color unset = new Vector4(1, 1, 1, 0);
            SetButtonColors(oldButton, unset);
        }
        Button newButton = entry.GetComponent<PokedexEntry>().background;
        Color toset = new Vector4(1, 0, 0, 1);
        SetButtonColors(newButton, toset);
        PokedexEntry dexEntry = entry.GetComponent<PokedexEntry>();
        currentPokemon = dexEntry.pokemon;
        currentEntry = entry;
    }

    public void DeleteCurrentPokemonAndEntry() {
        if (currentEntry != null) {
            Destroy(currentEntry);
            pokemonToEncounter.Remove(currentPokemon);
            File.Delete(Path.Combine(dataPath, PokedexManager.currentPokemon.savePath));
            currentPokemon = null;
            currentEntry = null;
        }
    }

    public void CaptureCurrentSelected() {
        Debug.Log(PokedexManager.currentPokemon.savePath);
        string newPath = PokedexManager.currentPokemon.savePath.Replace("tmp/", "Captured/");
        Debug.Log(newPath);
        Pokemon pokemon = PokedexManager.currentPokemon.Clone();
        pokemon.savePath = newPath;
        pokemon.ToJson(pokemon.savePath);
        PokedexManager.manager.DeleteCurrentPokemonAndEntry();
    }

    public void ReleaseCurrentSelected() {
        Debug.Log(PokedexManager.currentPokemon.savePath);
        string newPath = PokedexManager.currentPokemon.savePath.Replace("Captured/", "tmp/");
        Debug.Log(newPath);
        Pokemon pokemon = PokedexManager.currentPokemon.Clone();
        pokemon.savePath = newPath;
        pokemon.ToJson(pokemon.savePath);
        PokedexManager.manager.DeleteCurrentPokemonAndEntry();
    }

    static public void SetButtonColors(Button button, Color color) {
        var colors = button.colors;
        colors.normalColor = color;
        colors.highlightedColor = color;
        colors.selectedColor = color;
        colors.pressedColor = color;
        button.colors = colors;
    }

    static public string GetLocalIPAddress() {
        if (networkAvailable) {
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (var ip in host.AddressList) {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                    return ip.ToString();
                }
            }
            Debug.LogError("No network adapters with an IPv4 address in the system!");
        }
        return "No network available";
    }

    [Discardable]
    public GameObject CreateWarningDialog(string message) {
        GameObject dialog = Instantiate(warningBox);
        DialogController dialogController = dialog.GetComponent<DialogController>();
        dialogController.messageBox.text = message;
        Debug.LogWarning(message);
        return dialog;
    }

    [Discardable]
    public GameObject CreateConfirmationDialog(string message, ConfirmationType confirmation) {
        GameObject dialog = Instantiate(confirmationBox);
        DialogController dialogController = dialog.GetComponent<DialogController>();
        dialogController.messageBox.text = message;
        dialogController.confirmationType = confirmation;
        Debug.Log(message);
        return dialog;
    }

    [Discardable]
    public GameObject CreateTooltipDialog(string title, string type, string message, Color? color = null) {
        if (color == null) { color = Color.clear; } 
        GameObject dialog = Instantiate(tooltipBox);
        DialogController dialogController = dialog.GetComponent<DialogController>();
        if (dialogController.titleBox != null) { dialogController.titleBox.text = title; }
        if (dialogController.typeBox != null) { dialogController.typeBox.text = type; }
        if (dialogController.typeImage != null) { dialogController.typeImage.color = (Color)color; }
        dialogController.messageBox.text = message;
        Debug.Log(message);
        return dialog;
    }

    [Discardable]
    public GameObject CreateSendingDialog(string message) {
        GameObject dialog = Instantiate(sendingBox);
        DialogController dialogController = dialog.GetComponent<DialogController>();
        dialogController.messageBox.text = message;
        Debug.Log(message);
        return dialog;
    }

    [Discardable]
    public GameObject CreateEditDialog(SaveType saveType) {
        GameObject dialog = Instantiate(editDialogBox);
        DialogController dialogController = dialog.GetComponent<DialogController>();
        dialogController.LoadEditDialog(saveType);
        return dialog;
    }

    public void LoadClip(string file, Pokemon pokemon) {
        StartCoroutine(LoadClipCoroutine(file, pokemon));
    }

    private IEnumerator<UnityWebRequestAsyncOperation> LoadClipCoroutine(string file, Pokemon pokemon) {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(file, AudioType.OGGVORBIS)) {
            yield return www.SendWebRequest();

            if (www.isNetworkError) {
                Debug.Log(www.error);
            } else {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                Debug.Log(clip.name + " has a length of: " + clip.length);
                pokemon.cryAudio = clip;
            }
        }
    }

    static public string[] RemoveReturns(string[] array) {
        List<string> list = array.ToList();
        for (int i = 0; i < list.Count(); i++) {
            list[i] = list[i].Replace("\r", "").Replace("\n", "").Trim();
        }
        return list.ToArray();
    }

    static public string CleanAbilites(string abilities) {
        abilities = abilities.Replace(" Ability:", ":");
        if (Regex.IsMatch(abilities, @" Ability *[\d-]:"))
            abilities = Regex.Replace(abilities, @" Ability *[\d-]:", ":");
        return abilities;
    }

    static public string[] CleanCapabilites(string capabilities, char split) {
        List<string> capList = capabilities.Split(split).ToList();
        for (int i = 0; i < capList.Count(); i++) {
            if (capList[i].Contains("Naturewalk") && !capList[i].Contains(")")) {
                string natureWalkRepair = capList[i] + "," + capList[i + 1];
                capList.Remove(capList[i + 1]);
                capList[i] = natureWalkRepair;
            }
        }
        return capList.ToArray();
    }
}
