using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokedexController : MonoBehaviour {
    public GameObject pokedexPrefab;
    public GameObject contentPanel;

    private List<Pokemon> pokemonInView = new List<Pokemon>();

    private void Start() {
        GetPokemonToView("");
        EnumerateScrollView();
    }

    public void Search() {
        InputField searchInput = GameObject.Find("Search Field").GetComponent<InputField>();
        GetPokemonToView(searchInput.text);
        EnumerateScrollView();
    }

    private void GetPokemonToView(string query) {
        pokemonInView = new List<Pokemon>();
        foreach (Pokemon pokemon in PokedexManager.pokedex) {
            if (pokemon.species.ToLower().Contains(query.ToLower())) {
                pokemonInView.Add(pokemon);
            }
        }
    }

    private void EnumerateScrollView() {
        // Clear any entries so not to duplicate.
        for (int i = 0; i < contentPanel.transform.childCount; i++) {
            Destroy(contentPanel.transform.GetChild(i).gameObject);
        }

        // Get the name field so to set a default pokemon to it. 
        Text nameField = GameObject.Find("Name Field").GetComponent<Text>();

        foreach (Pokemon pokemon in pokemonInView) {
            GameObject newPokemon = Instantiate(pokedexPrefab) as GameObject;
            PokedexEntry controller = newPokemon.GetComponent<PokedexEntry>();
            controller.pokemon = pokemon;
            controller.species.text = pokemon.number + " - " + pokemon.species;
            newPokemon.transform.SetParent(contentPanel.transform);
            newPokemon.transform.localScale = Vector3.one;
            pokemon.sprite = Resources.Load<Sprite>("Icons/" + pokemon.image);

            if (pokemon.sprite != null)
                controller.sprite.sprite = pokemon.sprite;
            else
                Debug.LogError("Pokemon Sprite could not be loaded from: Icons/" + pokemon.image);

            // Set default if there isn't already one
            if (nameField.text == "") {
                OnSelected(pokemon);
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
        //AudioSource pokemonCry = GameObject.Find("Cry Button").GetComponent<AudioSource>();

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

        //if (pokemon.audio != null)
        //    pokemonCry.clip = Resources.Load<AudioClip>("Audio/" + pokemon.audio);
        //else
        //    Debug.LogError("Pokemon Cry could not be loaded from: Audio/Cries/" + pokemon.audio);
    }
}
