using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokedexController : MonoBehaviour {
    public GameObject pokedexPrefab;
    public GameObject contentPanel;

    private List<Pokemon> pokemonInView = new List<Pokemon>();
    private UnityEngine.UI.InputField
        numField,
        nameField,
        typeField,
        sizeField,
        weightField,
        genderField,
        eggField,
        hatchField,
        dietField,
        entryField,
        hpField,
        atkField,
        defField,
        spatkField,
        spdefField,
        spdField;

    private Dropdown
        evolutionsDropdown,
        abilitiesDropdown,
        skillsDropdown,
        capabilitiesDropdown,
        movesDropdown,
        habitatDropdown;

    private void Start() {
        nameField = GameObject.Find("Name Field").GetComponent<UnityEngine.UI.InputField>();
        numField = GameObject.Find("Number Field").GetComponent<UnityEngine.UI.InputField>();
        typeField = GameObject.Find("Types Field").GetComponent<UnityEngine.UI.InputField>();
        sizeField = GameObject.Find("Size Field").GetComponent<UnityEngine.UI.InputField>();
        weightField = GameObject.Find("Weight Field").GetComponent<UnityEngine.UI.InputField>();
        genderField = GameObject.Find("Gender Field").GetComponent<UnityEngine.UI.InputField>();
        eggField = GameObject.Find("Egg Field").GetComponent<UnityEngine.UI.InputField>();
        hatchField = GameObject.Find("Hatch Field").GetComponent<UnityEngine.UI.InputField>();
        dietField = GameObject.Find("Diet Field").GetComponent<UnityEngine.UI.InputField>();
        entryField = GameObject.Find("Entry Field").GetComponent<UnityEngine.UI.InputField>();
        hpField = GameObject.Find("HP Field").GetComponent<UnityEngine.UI.InputField>();
        atkField = GameObject.Find("ATK Field").GetComponent<UnityEngine.UI.InputField>();
        defField = GameObject.Find("DEF Field").GetComponent<UnityEngine.UI.InputField>();
        spatkField = GameObject.Find("SPATK Field").GetComponent<UnityEngine.UI.InputField>();
        spdefField = GameObject.Find("SPDEF Field").GetComponent<UnityEngine.UI.InputField>();
        spdField = GameObject.Find("SPD Field").GetComponent<UnityEngine.UI.InputField>();

        evolutionsDropdown = GameObject.Find("Evolutions Dropdown").GetComponent<Dropdown>();
        abilitiesDropdown = GameObject.Find("Abilities Dropdown").GetComponent<Dropdown>();
        skillsDropdown = GameObject.Find("Skills Dropdown").GetComponent<Dropdown>();
        capabilitiesDropdown = GameObject.Find("Capabilities Dropdown").GetComponent<Dropdown>();
        movesDropdown = GameObject.Find("Moves Dropdown").GetComponent<Dropdown>();
        habitatDropdown = GameObject.Find("Habitat Dropdown").GetComponent<Dropdown>();

        StartCoroutine(GetPokemonToView(""));
        EnumerateScrollView();
    }

    private void Update() {
        if (nameField.text == "" && pokemonInView.Count > 0) {
            OnSelected(pokemonInView[0]);
        } 
    }

    public void Search() {
        StopAllCoroutines();
        UnityEngine.UI.InputField searchInput = GameObject.Find("Search Field").GetComponent<UnityEngine.UI.InputField>();
        StartCoroutine(GetPokemonToView(searchInput.text));
        EnumerateScrollView();
    }

    private IEnumerator<List<Pokemon>> GetPokemonToView(string query) {
        pokemonInView = new List<Pokemon>();
        foreach (Pokemon pokemon in PokedexManager.pokedex) {
            if (pokemon.species.ToLower().Contains(query.ToLower())) {
                pokemonInView.Add(pokemon);
            }
        }
        yield return pokemonInView;
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
            pokemon.number.ToString();
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
        entryField.text = pokemon.entry == null ?
            "No entry found..." :
            pokemon.entry;

        hpField.text = pokemon.hp.ToString();
        atkField.text = pokemon.atk.ToString();
        defField.text = pokemon.def.ToString();
        spatkField.text = pokemon.spatk.ToString();
        spdefField.text = pokemon.spdef.ToString();
        spdField.text = pokemon.spd.ToString();

        List<Dropdown.OptionData> capList = new List<Dropdown.OptionData>();
        foreach (var cap in pokemon.capabilities) {
            capList.Add(new Dropdown.OptionData(cap));
        }
        capabilitiesDropdown.ClearOptions();
        capabilitiesDropdown.AddOptions(capList);

        List<Dropdown.OptionData> abilityList = new List<Dropdown.OptionData>();
        foreach (var ab in pokemon.AbilitiesToArray()) {
            abilityList.Add(new Dropdown.OptionData(ab));
        }
        abilitiesDropdown.ClearOptions();
        abilitiesDropdown.AddOptions(abilityList);

        List<Dropdown.OptionData> moveList = new List<Dropdown.OptionData>();
        foreach (var move in pokemon.moves) {
            moveList.Add(new Dropdown.OptionData(move));
        }
        movesDropdown.ClearOptions();
        movesDropdown.AddOptions(moveList);

        List<Dropdown.OptionData> skillList = new List<Dropdown.OptionData>();
        foreach (var skill in pokemon.SkillsToArray()) {
            skillList.Add(new Dropdown.OptionData(skill));
        }
        skillsDropdown.ClearOptions();
        skillsDropdown.AddOptions(skillList);

        List<Dropdown.OptionData> evoList = new List<Dropdown.OptionData>();
        foreach (var evo in pokemon.evolutions) {
            evoList.Add(new Dropdown.OptionData(evo));
        }
        evolutionsDropdown.ClearOptions();
        evolutionsDropdown.AddOptions(evoList);

        List<Dropdown.OptionData> habitatList = new List<Dropdown.OptionData>();
        foreach (var habitat in pokemon.HabitatsToArray()) {
            habitatList.Add(new Dropdown.OptionData(habitat));
        }
        habitatDropdown.ClearOptions();
        habitatDropdown.AddOptions(habitatList);
    }
}
