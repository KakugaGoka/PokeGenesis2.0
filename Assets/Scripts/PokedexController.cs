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
        // Get the name field so to set a default pokemon to it. 
        Text nameField = GameObject.Find("Name Field").GetComponent<Text>();

        foreach (Pokemon pokemon in PokedexManager.pokedex)
        {
            GameObject newPokemon = Instantiate(pokedexPrefab) as GameObject;
            PokedexEntry controller = newPokemon.GetComponent<PokedexEntry>();
            controller.pokemon = pokemon;
            controller.species.text = pokemon.number + " - " + pokemon.species;
            newPokemon.transform.SetParent(contentPanel.transform);
            newPokemon.transform.localScale = Vector3.one;
            pokemon.sprite = Resources.Load<Sprite>("Icons/" + pokemon.image);

            if (pokemon.sprite != null)
                controller.image.sprite = pokemon.sprite;
            else
                Debug.LogError("Pokemon Sprite could not be loaded from: Icons/" + pokemon.image);

            // Set default if there isn't already one
            if (nameField.text == "")
            {
                controller.OnSelected();
            }
        }
    }
}
