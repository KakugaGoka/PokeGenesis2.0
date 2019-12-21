using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterController : MonoBehaviour {
    public GameObject pokedexPrefab;
    public GameObject contentPanel;

    private List<Pokemon> pokemonToEncounter = new List<Pokemon>();
    private List<Pokemon> encounterablePokemon = new List<Pokemon>();

    public void OnScan() {
        // Get the name field so to set a default pokemon to it. 
        bool appendScan = GameObject.Find("Append to List").GetComponent<Toggle>().isOn;
        bool allowShinies = GameObject.Find("Allow Shinies").GetComponent<Toggle>().isOn;
        bool alwaysShiny = GameObject.Find("Always Shiny").GetComponent<Toggle>().isOn;
        bool allowLegendaries = GameObject.Find("Allow Legendaries").GetComponent<Toggle>().isOn;
        bool allowHeldItems = GameObject.Find("Allow Held Items").GetComponent<Toggle>().isOn;

        encounterablePokemon = new List<Pokemon>();
        if (!allowShinies) { alwaysShiny = false; }

        foreach (Pokemon pokemon in PokedexManager.pokedex) {
            if (!allowLegendaries && pokemon.legendary) {
                continue;
            }
            encounterablePokemon.Add(pokemon);
        }

        if (!appendScan) {
            pokemonToEncounter = new List<Pokemon>();
        }

        int index = Random.Range(0, encounterablePokemon.Count);
        pokemonToEncounter.Add(encounterablePokemon[index]);

        if (allowShinies) {
            int shinyChance = Random.Range(0, 8192);
            if (alwaysShiny) { shinyChance = 0; }
            pokemonToEncounter[pokemonToEncounter.Count - 1].shiny = shinyChance == 0 ? true : false;
        } else {
            pokemonToEncounter[pokemonToEncounter.Count - 1].shiny = false;
        }

        for (int i = 0; i < contentPanel.transform.childCount; i++) {
            Destroy(contentPanel.transform.GetChild(i).gameObject);
        }

        foreach(Pokemon pokemon in pokemonToEncounter) {
            GameObject newPokemon = Instantiate(pokedexPrefab) as GameObject;
            PokedexEntry controller = newPokemon.GetComponent<PokedexEntry>();
            controller.pokemon = pokemon;
            controller.species.text = pokemon.number + " - " + pokemon.species;
            newPokemon.transform.SetParent(contentPanel.transform);
            newPokemon.transform.localScale = Vector3.one;
            pokemon.sprite = Resources.Load<Sprite>("Icons/" + pokemon.image);

            if (pokemon.sprite != null) {
                controller.sprite.sprite = pokemon.sprite;
                if (pokemon.shiny) {
                    if (!pokemon.colorHasBeenSet) {
                        float h = Random.Range(0.0f, 1.0f);
                        float s = 0.5f;
                        float v = 1.0f;
                        pokemon.color = Color.HSVToRGB(h, s, v);
                        pokemon.colorHasBeenSet = true;
                    }
                    controller.sprite.color = pokemon.color;
                }
            } else {
                Debug.LogError("Pokemon Sprite could not be loaded from: Icons/" + pokemon.image);
            }
        }
    }
}