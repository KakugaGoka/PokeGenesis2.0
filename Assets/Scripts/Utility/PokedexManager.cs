using System.IO;
using System.Linq;
using System.Collections.Generic;
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

    public GameObject warningBox;
    public GameObject confirmationBox;
    public GameObject tooltipBox;

    // Start is called before the first frame update
    void Awake() {
        if (manager == null) {
            manager = this;
        } else {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        if (!Directory.Exists(Path.Combine(Application.streamingAssetsPath, "Captured/"))) {
            Directory.CreateDirectory(Path.Combine(Application.streamingAssetsPath, "Captured/"));
        }

        if (!Directory.Exists(Path.Combine(Application.streamingAssetsPath, "tmp/"))) {
            Directory.CreateDirectory(Path.Combine(Application.streamingAssetsPath, "tmp/"));
        }

        // Load Pokemon From JSON
        string pokemonString = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json"));
        pokedex = JsonHelper.FromJson<Pokemon>(pokemonString);
        pokedex = pokedex.OrderBy(x => x.number).ToArray();
        Debug.Log("Pokedex Count: " + pokedex.Count());

        int capabilityCount = 0;
        foreach (var pokemon in pokedex) {
            if (pokemon.capabilities.Count() > capabilityCount) {
                capabilityCount = pokemon.capabilities.Count();
            }
        }
        Debug.Log("Highest Capability Count: " + capabilityCount.ToString());

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

        string tmString = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/TMs.json"));
        TMs = JsonHelper.FromJson<TM>(tmString);
        TMs = TMs.OrderBy(x => x.number).ToArray();
        Debug.Log("TM Count: " + TMs.Count());

        string abilityString = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Abilities.json"));
        abilities = JsonHelper.FromJson<Ability>(abilityString);
        abilities = abilities.OrderBy(x => x.name).ToArray();
        Debug.Log("Ability Count: " + abilities.Count());

        string moveString = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Moves.json"));
        moves = JsonHelper.FromJson<Move>(moveString);
        moves = moves.OrderBy(x => x.name).ToArray();
        Debug.Log("Move Count: " + TMs.Count());

        string skillString = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Skills.json"));
        skillsInfo = JsonHelper.FromJson<Info>(skillString);
        skillsInfo = skillsInfo.OrderBy(x => x.name).ToArray();
        Debug.Log("Skill Info Count: " + skillsInfo.Count());

        string capabilityString = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Capabilities.json"));
        capabilitiesInfo = JsonHelper.FromJson<Info>(capabilityString);
        capabilitiesInfo = capabilitiesInfo.OrderBy(x => x.name).ToArray();
        Debug.Log("Capability Info Count: " + capabilitiesInfo.Count());

        string conditionString = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Conditions.json"));
        conditionsInfo = JsonHelper.FromJson<Info>(conditionString);
        conditionsInfo = conditionsInfo.OrderBy(x => x.name).ToArray();
        Debug.Log("Condition Info Count: " + conditionsInfo.Count());
    }

    private void Update() {
        networkAvailable = Application.internetReachability != NetworkReachability.NotReachable;
    }

    public void ChangeScene(int sceneID) {
        SceneManager.LoadScene(sceneID);
    }

    static public Sprite LoadSprite(string path) {
        string fullPath = Path.Combine(Application.streamingAssetsPath, path + ".png");
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
        var myFiles = Directory.EnumerateFiles(Application.streamingAssetsPath + "/tmp/", "*.json", SearchOption.TopDirectoryOnly);

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
            File.Delete(Path.Combine(Application.streamingAssetsPath, PokedexManager.currentPokemon.savePath));
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

    public void CreateWarningDialog(string message) {
        GameObject dialog = Instantiate(warningBox);
        DialogController dialogController = dialog.GetComponent<DialogController>();
        dialogController.messageBox.text = message;
        Debug.LogWarning(message);
    }

    public void CreateConfirmationDialog(string message, ConfirmationType confirmation) {
        GameObject dialog = Instantiate(confirmationBox);
        DialogController dialogController = dialog.GetComponent<DialogController>();
        dialogController.messageBox.text = message;
        dialogController.confirmationType = confirmation;
        Debug.Log(message);
    }

    public void CreateTooltipDialog(string message) {
        GameObject dialog = Instantiate(tooltipBox);
        DialogController dialogController = dialog.GetComponent<DialogController>();
        dialogController.messageBox.text = message;
        Debug.Log(message);
    }

    static public int GetMaxHealth(Pokemon pokemon) {
        int health = pokemon.level + (pokemon.hpLevel * 3) + 10;
        return health;
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
}
