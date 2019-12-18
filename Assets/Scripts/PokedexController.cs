using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PokedexController : MonoBehaviour
{
    public GameObject pokedexPrefab;
    public GameObject contentPanel;

    private void Start()
    {
        // Load Pokemon From JSON and apply them to the entry prefab
        string pokemonString = File.ReadAllText(Path.Combine(Application.dataPath, "JSON/Pokemon.json"));
        Pokemon[] pokedexEntries = JsonHelper.FromJson<Pokemon>(pokemonString);
        Debug.Log(pokedexEntries.Length);
        pokedexEntries = pokedexEntries.OrderBy(x => x.number).ToArray<Pokemon>();

        // Get the name field so to set a default pokemon to it. 
        Text nameField = GameObject.Find("Name Field").GetComponent<Text>();

        foreach (Pokemon pokemon in pokedexEntries)
        {
#if DEBUG
            string filePath = "Icons/" + pokemon.image;
            string dataPath = Application.dataPath;
            string combinedPath = Path.Combine(dataPath, filePath + ".png");
            if (!File.Exists(combinedPath))
            {
                Debug.LogError("Sprite was not found: " + combinedPath);
            }
#endif
            GameObject newPokemon = Instantiate(pokedexPrefab) as GameObject;
            PokedexEntry controller = newPokemon.GetComponent<PokedexEntry>();
            controller.pokemon = pokemon;
            controller.species.text = pokemon.number + " - " + pokemon.species;
            newPokemon.transform.SetParent(contentPanel.transform);
            newPokemon.transform.localScale = Vector3.one;
            // Set default if there isn't already one
            if (nameField.text == "")
            {
                controller.OnSelected();
            }
        }



        string jsonString = File.ReadAllText(Path.Combine(Application.dataPath, "JSON/PokemonTypes.json"));
        PokemonType[] pokemonTypes = JsonHelper.FromJson<PokemonType>(jsonString);
        Debug.Log(pokemonTypes.Length);
    }
}
