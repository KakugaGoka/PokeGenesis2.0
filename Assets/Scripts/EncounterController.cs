using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterController : MonoBehaviour {
    public GameObject pokedexPrefab;
    public GameObject contentPanel;
    public Slider encounterSlider;

    private List<Pokemon> pokemonToEncounter = new List<Pokemon>();
    private List<Pokemon> encounterablePokemon = new List<Pokemon>();

    private bool
        appendScan,
        allowShinies,
        alwaysShiny,
        allowLegendaries,
        allowHeldItems;

    public void OnScan() {
        // Get the check box fields to apply to the private bools. 
        appendScan = GameObject.Find("Append to List").GetComponent<Toggle>().isOn;
        allowShinies = GameObject.Find("Allow Shinies").GetComponent<Toggle>().isOn;
        alwaysShiny = GameObject.Find("Always Shiny").GetComponent<Toggle>().isOn;
        allowLegendaries = GameObject.Find("Allow Legendaries").GetComponent<Toggle>().isOn;
        allowHeldItems = GameObject.Find("Allow Held Items").GetComponent<Toggle>().isOn;

        // Clean the lists to ensure proper data is used fro the scans
        encounterablePokemon = new List<Pokemon>();

        if (!appendScan) {
            pokemonToEncounter = new List<Pokemon>();
        }

        // Clear out the old prefabs so that all UI items are correct ad their are no duplicates.
        // This is a quick and dirty method to do this, but could be optimized to just ensure no recreations of each encounterEntry in the future. 
        for (int i = 0; i < contentPanel.transform.childCount; i++) {
            Destroy(contentPanel.transform.GetChild(i).gameObject);
        }

        // Ensure that no pokemon is added that does not fit the parameters, like legendary or habitat. 
        // This should be pushed into its own function when all of the fields are added so to keep this function clean.
        foreach (Pokemon pokemon in PokedexManager.pokedex) {
            if (!allowLegendaries && pokemon.legendary) {
                continue;
            }
            encounterablePokemon.Add(pokemon);
        }

        // Add a number of new pokemon to the list equal to the slider value.
        AddPokemon(encounterSlider.value);

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

    void AddPokemon(float numberToEncounter) {
        for (float i = 0; i < numberToEncounter; i += 1) {
            int index = Random.Range(0, encounterablePokemon.Count);
            pokemonToEncounter.Add(encounterablePokemon[index]);

            if (allowShinies) {
                int shinyChance = Random.Range(0, 8192);
                if (alwaysShiny) { shinyChance = 0; }
                pokemonToEncounter[pokemonToEncounter.Count - 1].shiny = shinyChance == 0 ? true : false;
            } else {
                pokemonToEncounter[pokemonToEncounter.Count - 1].shiny = false;
            }
        }
    }

    public void OnSelected(Pokemon pokemon) {
        Text nameField = GameObject.Find("Name Field").GetComponent<Text>();
        Text numField = GameObject.Find("Number Field").GetComponent<Text>();
        Text typeField = GameObject.Find("Types Field").GetComponent<Text>();
        Text regionField = GameObject.Find("Region Field").GetComponent<Text>();
        Text sizeField = GameObject.Find("Size Field").GetComponent<Text>();
        Text weightField = GameObject.Find("Weight Field").GetComponent<Text>();
        Text genderField = GameObject.Find("Gender Field").GetComponent<Text>();
        Text eggField = GameObject.Find("Egg Field").GetComponent<Text>();
        Text hatchField = GameObject.Find("Hatch Field").GetComponent<Text>();
        Text dietField = GameObject.Find("Diet Field").GetComponent<Text>();
        Text habitatField = GameObject.Find("Habitat Field").GetComponent<Text>();
        Text entryField = GameObject.Find("Entry Field").GetComponent<Text>();

        nameField.text = pokemon.species == null ?
            "Unkown" :
            pokemon.species;
        numField.text = pokemon.number == 0 ?
            "???" :
            "#" + pokemon.number.ToString();
        typeField.text = pokemon.type == null ?
            "Unkown" :
            pokemon.type;
        regionField.text = pokemon.region == null ?
            "Unkown" :
            pokemon.region;
        sizeField.text = pokemon.size == null ?
            "Unkown" :
            pokemon.size;
        weightField.text = pokemon.weight == null ?
            "Unkown" :
            pokemon.weight;
        genderField.text = pokemon.gender == null ?
            "Unkown" :
            pokemon.gender;
        eggField.text = pokemon.egg == null ?
            "Unkown" :
            pokemon.egg;
        hatchField.text = pokemon.hatch == null ?
            "None" :
            pokemon.hatch;
        dietField.text = pokemon.diet == null ?
            "Unkown" :
            pokemon.diet;
        habitatField.text = pokemon.habitat == null ?
            "Unkown" :
            pokemon.habitat;
        entryField.text = pokemon.entry == null ?
            "No entry found..." :
            pokemon.entry;
    }

    public void UpdateSliderNumber() {
        Text sliderLabel = GameObject.Find("Slider Label").GetComponent<Text>();
        sliderLabel.text = encounterSlider.value.ToString();
    }
}