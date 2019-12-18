using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PokedexEntry : MonoBehaviour
{
    public Text species;
    public Button background;
    public Color backgroundTint;

    [HideInInspector]
    public Pokemon pokemon;


    public void OnSelected()
    {
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
        Image pokemonImage = GameObject.Find("Pokedex Image").GetComponent<Image>();

        nameField.text = pokemon.species;
        numField.text = "#" + pokemon.number.ToString();
        typeField.text = pokemon.type == null ? "Unkown" : pokemon.type;
        regionField.text = pokemon.region == null ? "Unkown" : pokemon.region;
        sizeField.text = pokemon.size == null ? "Unkown" : pokemon.size;
        weightField.text = pokemon.weight == null ? "Unkown" : pokemon.weight;
        genderField.text = pokemon.gender == null ? "Unkown" : pokemon.gender;
        eggField.text = pokemon.egg == null ? "Unkown" : pokemon.egg;
        hatchField.text = pokemon.hatch == null ? "None" : pokemon.hatch;
        dietField.text = pokemon.diet == null ? "Unkown" : pokemon.diet;
        habitatField.text = pokemon.habitat == null ? "Unkown" : pokemon.habitat;
        entryField.text = pokemon.entry == null ? "No entry found..." : pokemon.entry;


        if (pokemon.sprite == null)
        {
            pokemon.sprite = LoadSprite("Icons/" + pokemon.image); // <- Work around?
        }
        pokemonImage.sprite = pokemon.sprite;

        background.image.color = backgroundTint;
    }

    Sprite LoadSprite(string filePath)
    {
        string dataPath = Application.dataPath;
        string combinedPath = Path.Combine(dataPath, filePath);
        if (!File.Exists(combinedPath))
        {
            Debug.LogError("Sprite was not found: " + filePath);
        }
        byte[] bytes = File.ReadAllBytes(combinedPath);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        return sprite;
    }
}
