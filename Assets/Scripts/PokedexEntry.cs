using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PokedexEntry : MonoBehaviour {
    public Image sprite;
    public Text species;
    public Button background;
    public Color backgroundTint;

    [HideInInspector]
    public Pokemon pokemon;

    public void OnSelected() {
        if (SceneManager.GetActiveScene().name.Contains("Pokedex")) {
            PokedexController pokedexController = GameObject.Find("Scroll View").GetComponent<PokedexController>();
            pokedexController.OnSelected(pokemon);
        }

        if (SceneManager.GetActiveScene().name.Contains("Encounter")) {
            EncounterController encounterController = GameObject.Find("Scroll View").GetComponent<EncounterController>();
            encounterController.OnSelected(pokemon);
        }
    }
}
