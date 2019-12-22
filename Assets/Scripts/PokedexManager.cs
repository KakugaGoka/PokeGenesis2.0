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
        TextAsset pokemonString = Resources.Load<TextAsset>("JSON/Pokemon");
        pokedex = JsonHelper.FromJson<Pokemon>(pokemonString.text);
        pokedex = pokedex.OrderBy(x => x.number).ToArray();
        Debug.Log("Pokedex Count: " + pokedex.Count());

        // Load in Types from JSON
        TextAsset typesString = Resources.Load<TextAsset>("JSON/PokemonTypes");
        types = JsonHelper.FromJson<PokemonType>(typesString.text);
        types = types.OrderBy(x => x.typeName).ToArray();
        Debug.Log("Types Count: " + types.Count());

        // Load in Natures from JSON
        TextAsset naturesString = Resources.Load<TextAsset>("JSON/Natures");
        natures = JsonHelper.FromJson<Nature>(naturesString.text);
        natures = natures.OrderBy(x => x.name).ToArray();
        Debug.Log("Natures Count: " + natures.Count());

        // Load in Habitats from JSON
        TextAsset habitatsString = Resources.Load<TextAsset>("JSON/Habitats");
        habitats = JsonHelper.FromJson<string>(habitatsString.text);
        habitats = habitats.OrderBy(x => x).ToArray();
        Debug.Log("Habitats Count: " + habitats.Count());

        // Load in Items from JSON
        TextAsset itemsString = Resources.Load<TextAsset>("JSON/Items");
        Item[] tempItems = JsonHelper.FromJson<Item>(itemsString.text);
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
}
