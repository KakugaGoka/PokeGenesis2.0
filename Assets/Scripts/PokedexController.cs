using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokedexController : MonoBehaviour {
    public GameObject pokedexPrefab;
    public GameObject contentPanel;

    private List<Pokemon> pokemonInView = new List<Pokemon>();
    private Text 
        nameField,
        numField,
        typeField,
        sizeField,
        weightField,
        genderField,
        eggField,
        hatchField,
        dietField,
        habitatField,
        entryField;

    private void Start() {
        nameField = GameObject.Find("Name Field").GetComponent<Text>();
        numField = GameObject.Find("Number Field").GetComponent<Text>();
        typeField = GameObject.Find("Types Field").GetComponent<Text>();
        sizeField = GameObject.Find("Size Field").GetComponent<Text>();
        weightField = GameObject.Find("Weight Field").GetComponent<Text>();
        genderField = GameObject.Find("Gender Field").GetComponent<Text>();
        eggField = GameObject.Find("Egg Field").GetComponent<Text>();
        hatchField = GameObject.Find("Hatch Field").GetComponent<Text>();
        dietField = GameObject.Find("Diet Field").GetComponent<Text>();
        habitatField = GameObject.Find("Habitat Field").GetComponent<Text>();
        entryField = GameObject.Find("Entry Field").GetComponent<Text>();

        GetPokemonToView("");
        EnumerateScrollView();
    }

    private void Update() {
        if (nameField.text == "" && pokemonInView.Count > 0) {
            OnSelected(pokemonInView[0]);
        } 
    }

    public void Search() {
        UnityEngine.UI.InputField searchInput = GameObject.Find("Search Field").GetComponent<UnityEngine.UI.InputField>();
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

        StartCoroutine(CreateListItem());
    }

    private IEnumerator<GameObject> CreateListItem() {
        foreach (Pokemon pokemon in pokemonInView) {
            GameObject newPokemon = Instantiate(pokedexPrefab) as GameObject;
            PokedexEntry controller = newPokemon.GetComponent<PokedexEntry>();
            controller.pokemon = pokemon;
            controller.species.text = pokemon.number + " - " + pokemon.species;
            newPokemon.transform.SetParent(contentPanel.transform);
            newPokemon.transform.localScale = Vector3.one;
            pokemon.sprite = PokedexManager.LoadSprite("PokemonIcons/" + pokemon.image);

            if (pokemon.sprite != null) {
                controller.sprite.sprite = pokemon.sprite;
            } else {
                Debug.LogError("Pokemon Sprite could not be loaded from: Icons/" + pokemon.image);
            }
            yield return newPokemon;
        }
    }

    public void OnSelected(Pokemon pokemon) {
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
}
