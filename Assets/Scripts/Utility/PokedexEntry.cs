﻿using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PokedexEntry : MonoBehaviour {
    public Image sprite;
    public Text species;
    public Button background;
    public Color backgroundTint;
    public GameObject shiny;
    public GameObject dynaFront;
    public GameObject dynaBack;

    [HideInInspector]
    public Pokemon pokemon;

    public void OnSelected() {
        if (SceneManager.GetActiveScene().name.Contains("Pokedex")) {
            PokedexController pokedexController = GameObject.Find("Pokedex Scroll View").GetComponent<PokedexController>();
            pokedexController.OnSelected(pokemon, this.gameObject);
        }

        if (SceneManager.GetActiveScene().name.Contains("Encounter")) {
            EncounterController encounterController = GameObject.Find("Scroll View").GetComponent<EncounterController>();
            encounterController.OnSelected(pokemon, this.gameObject);
        }

        if (SceneManager.GetActiveScene().name.Contains("Sheet")) {
            SheetController sheetController = GameObject.Find("Scroll View").GetComponent<SheetController>();
            sheetController.OnSelected(pokemon, this.gameObject);
        }
    }

    public void SetNormalColor(Color color) {
        if (PokedexManager.currentEntry != null) {
            var colors = background.colors;
            colors.normalColor = color;
            background.colors = colors;
        }
    }
}
