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
                controller.OnSelected();
            }
        }
    }
}
