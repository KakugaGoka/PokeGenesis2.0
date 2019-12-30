using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    static public List<Pokemon> pokemonToEncounter = new List<Pokemon>();

    public GameObject warningBox;
    public GameObject confirmationBox;

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

    static public void SetButtonColors(Button button, Color color) {
        var colors = button.colors;
        colors.normalColor = color;
        colors.highlightedColor = color;
        colors.selectedColor = color;
        colors.pressedColor = color;
        button.colors = colors;
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
        Debug.LogWarning(message);
    }
}
