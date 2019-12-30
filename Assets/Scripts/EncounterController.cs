using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EncounterController : MonoBehaviour {
    public GameObject pokedexPrefab;
    public GameObject contentPanel;
    public Slider encounterSlider;
    public Slider minLevelSlider;
    public Slider maxLevelSlider;
    public Dropdown habitatDropdown;
    public Dropdown natureDropdown;
    public Dropdown typeDropdown;
    public Dropdown pokemonDropdown;
    public Dropdown stageDropdown;
    public Image heldItemImage;
    public AudioSource cryAudioSource;

    private List<Pokemon> encounterablePokemon = new List<Pokemon>();

    private bool
        appendScan,
        allowShinies,
        alwaysShiny,
        allowLegendaries,
        allowHeldItems,
        alwaysHoldItem;
    
    private Toggle
        blindedToggle,
        totallyBlindedToggle,
        burnedToggle,
        confusedToggle,
        cursedToggle,
        disabledToggle,
        enragedToggle,
        flinchedToggle,
        frozenToggle,
        infatuatedToggle,
        paralyzedToggle,
        poisonedToggle,
        badlyPoisonedToggle,
        asleepToggle,
        badlyAsleepToggle,
        slowedToggle,
        stuckToggle,
        suppressedToggle,
        trappedToggle,
        trippedToggle,
        vulnerableToggle;

    private InputField
        nameField,
        typeField,
        sizeField,
        weightField,
        genderField,
        natureField,
        hpBaseField,
        atkBaseField,
        defBaseField,
        spatkBaseField,
        spdefBaseField,
        spdBaseField,
        hpLevelField,
        atkLevelField,
        defLevelField,
        spatkLevelField,
        spdefLevelField,
        spdLevelField,
        hpCSField,
        atkCSField,
        defCSField,
        spatkCSField,
        spdefCSField,
        spdCSField,
        hpTotalField,
        atkTotalField,
        defTotalField,
        spatkTotalField,
        spdefTotalField,
        spdTotalField,
        currentHealthField,
        maxHealthField, 
        basicAbilityField,
        advanceAbilityField,
        highAbilityField,
        levelField,
        movesListField,
        skillsListField,
        capabilitiesListField,
        heldItemDescriptionField,
        heldItemNameField,
        captureRateField;

    private void Start() {
        nameField = GameObject.Find("Name Field").GetComponent<InputField>();
        typeField = GameObject.Find("Types Field").GetComponent<InputField>();
        sizeField = GameObject.Find("Size Field").GetComponent<InputField>();
        weightField = GameObject.Find("Weight Field").GetComponent<InputField>();
        genderField = GameObject.Find("Gender Field").GetComponent<InputField>();
        natureField = GameObject.Find("Nature Field").GetComponent<InputField>();
        hpBaseField = GameObject.Find("HP Base Field").GetComponent<InputField>();
        atkBaseField = GameObject.Find("ATK Base Field").GetComponent<InputField>();
        defBaseField = GameObject.Find("DEF Base Field").GetComponent<InputField>();
        spatkBaseField = GameObject.Find("SPATK Base Field").GetComponent<InputField>();
        spdefBaseField = GameObject.Find("SPDEF Base Field").GetComponent<InputField>();
        spdBaseField = GameObject.Find("SPD Base Field").GetComponent<InputField>();
        hpLevelField = GameObject.Find("HP Level Field").GetComponent<InputField>();
        atkLevelField = GameObject.Find("ATK Level Field").GetComponent<InputField>();
        defLevelField = GameObject.Find("DEF Level Field").GetComponent<InputField>();
        spatkLevelField = GameObject.Find("SPATK Level Field").GetComponent<InputField>();
        spdefLevelField = GameObject.Find("SPDEF Level Field").GetComponent<InputField>();
        spdLevelField = GameObject.Find("SPD Level Field").GetComponent<InputField>();
        hpCSField = GameObject.Find("HP CS Field").GetComponent<InputField>();
        atkCSField = GameObject.Find("ATK CS Field").GetComponent<InputField>();
        defCSField = GameObject.Find("DEF CS Field").GetComponent<InputField>();
        spatkCSField = GameObject.Find("SPATK CS Field").GetComponent<InputField>();
        spdefCSField = GameObject.Find("SPDEF CS Field").GetComponent<InputField>();
        spdCSField = GameObject.Find("SPD CS Field").GetComponent<InputField>();
        hpTotalField = GameObject.Find("HP Total Field").GetComponent<InputField>();
        atkTotalField = GameObject.Find("ATK Total Field").GetComponent<InputField>();
        defTotalField = GameObject.Find("DEF Total Field").GetComponent<InputField>();
        spatkTotalField = GameObject.Find("SPATK Total Field").GetComponent<InputField>();
        spdefTotalField = GameObject.Find("SPDEF Total Field").GetComponent<InputField>();
        spdTotalField = GameObject.Find("SPD Total Field").GetComponent<InputField>();
        currentHealthField = GameObject.Find("Current Health Field").GetComponent<InputField>();
        maxHealthField = GameObject.Find("Max Health Field").GetComponent<InputField>();
        basicAbilityField = GameObject.Find("Basic Ability Field").GetComponent<InputField>();
        advanceAbilityField = GameObject.Find("Advanced Ability Field").GetComponent<InputField>();
        highAbilityField = GameObject.Find("High Ability Field").GetComponent<InputField>();
        levelField = GameObject.Find("Level Field").GetComponent<InputField>();
        movesListField = GameObject.Find("Moves List Field").GetComponent<InputField>();
        skillsListField = GameObject.Find("Skills List Field").GetComponent<InputField>();
        capabilitiesListField = GameObject.Find("Capabilities List Field").GetComponent<InputField>();
        heldItemDescriptionField = GameObject.Find("Held Item Description Field").GetComponent<InputField>();
        heldItemNameField = GameObject.Find("Held Item Name Field").GetComponent<InputField>();
        captureRateField = GameObject.Find("Capture Rate Field").GetComponent<InputField>();

        blindedToggle = GameObject.Find("Blinded Toggle").GetComponent<Toggle>();
        totallyBlindedToggle = GameObject.Find("Totally Blinded Toggle").GetComponent<Toggle>();
        burnedToggle = GameObject.Find("Burned Toggle").GetComponent<Toggle>();
        confusedToggle = GameObject.Find("Confused Toggle").GetComponent<Toggle>();
        cursedToggle = GameObject.Find("Cursed Toggle").GetComponent<Toggle>();
        disabledToggle = GameObject.Find("Disabled Toggle").GetComponent<Toggle>();
        enragedToggle = GameObject.Find("Enraged Toggle").GetComponent<Toggle>();
        flinchedToggle = GameObject.Find("Flinched Toggle").GetComponent<Toggle>();
        frozenToggle = GameObject.Find("Frozen Toggle").GetComponent<Toggle>();
        infatuatedToggle = GameObject.Find("Infatuated Toggle").GetComponent<Toggle>();
        paralyzedToggle = GameObject.Find("Paralyzed Toggle").GetComponent<Toggle>();
        poisonedToggle = GameObject.Find("Poisoned Toggle").GetComponent<Toggle>();
        badlyPoisonedToggle = GameObject.Find("Badly Poisoned Toggle").GetComponent<Toggle>();
        asleepToggle = GameObject.Find("Sleeping Toggle").GetComponent<Toggle>();
        badlyAsleepToggle = GameObject.Find("Heavily Sleeping Toggle").GetComponent<Toggle>();
        slowedToggle = GameObject.Find("Slowed Toggle").GetComponent<Toggle>();
        stuckToggle = GameObject.Find("Stuck Toggle").GetComponent<Toggle>();
        suppressedToggle = GameObject.Find("Suppressed Toggle").GetComponent<Toggle>();
        trappedToggle = GameObject.Find("Trapped Toggle").GetComponent<Toggle>();
        trippedToggle = GameObject.Find("Tripped Toggle").GetComponent<Toggle>();
        vulnerableToggle = GameObject.Find("Vulnerable Toggle").GetComponent<Toggle>();

        List<Dropdown.OptionData> habitatOptions = new List<Dropdown.OptionData>();
        habitatOptions.Add(new Dropdown.OptionData("Any Habitat"));
        foreach (string habitat in PokedexManager.habitats) {
            habitatOptions.Add(new Dropdown.OptionData(habitat));
        }
        habitatDropdown.AddOptions(habitatOptions);

        List<Dropdown.OptionData> natureOptions = new List<Dropdown.OptionData>();
        natureOptions.Add(new Dropdown.OptionData("Any Nature"));
        foreach (Nature nature in PokedexManager.natures) {
            natureOptions.Add(new Dropdown.OptionData(nature.name));
        }
        natureDropdown.AddOptions(natureOptions);

        List<Dropdown.OptionData> typeOptions = new List<Dropdown.OptionData>();
        typeOptions.Add(new Dropdown.OptionData("Any Type"));
        foreach (PokemonType type in PokedexManager.types) {
            typeOptions.Add(new Dropdown.OptionData(type.typeName));
        }
        typeDropdown.AddOptions(typeOptions);

        List<Dropdown.OptionData> pokemonOptions = new List<Dropdown.OptionData>();
        pokemonOptions.Add(new Dropdown.OptionData("Any Pokemon"));
        foreach (Pokemon pokemon in PokedexManager.pokedex) {
            pokemonOptions.Add(new Dropdown.OptionData(pokemon.species));
        }
        pokemonDropdown.AddOptions(pokemonOptions);

        List<Dropdown.OptionData> stageOptions = new List<Dropdown.OptionData>();
        stageOptions.Add(new Dropdown.OptionData("Any Stage"));
        stageOptions.Add(new Dropdown.OptionData("Stage 1"));
        stageOptions.Add(new Dropdown.OptionData("Stage 2"));
        stageOptions.Add(new Dropdown.OptionData("Stage 3"));
        stageDropdown.AddOptions(stageOptions);

        heldItemImage.sprite = PokedexManager.LoadSprite("ItemIcons/None");

        // Verify all pokemon images and cries
        foreach (Pokemon pokemon in PokedexManager.pokedex) {
            if (!File.Exists(Application.streamingAssetsPath + "/PokemonIcons/" + pokemon.image + ".png")) {
                Debug.LogWarning("Image not found for pokemon: " + pokemon.species);
            }
            if (!File.Exists(Application.streamingAssetsPath + "/Cries/" + pokemon.cry + ".ogg")) {
                Debug.LogWarning( "Cry not found for pokemon: " + pokemon.species);
            }
            foreach (string evo in pokemon.evolutions) {
                try {
                    int maxStage = int.Parse(evo[0].ToString());
                } catch {
                    Debug.LogWarning("Pokemon evolution is not formated correctly: " + evo);
                }
            }
            try {
                int testFinalEvo = int.Parse(pokemon.evolutions[pokemon.evolutions.Length - 1][0].ToString());
            } catch {
                Debug.LogWarning("Final Stage imroperly formatted for: " + pokemon.species);
            }
        }

        // Verify all item images
        foreach (Item item in PokedexManager.items) {
            if (!File.Exists(Application.streamingAssetsPath + "/ItemIcons/" + item.image + ".png")) {
                Debug.LogWarning("Image not found for item: " + item.name);
            }
        }

        // Load temp pokemon if they exist. 
        var myFiles = Directory.EnumerateFiles(Application.streamingAssetsPath + "/tmp/", "*.json", SearchOption.TopDirectoryOnly);

        foreach (var file in myFiles) {
            Pokemon pokemon = Pokemon.FromJson(file);
            PokedexManager.pokemonToEncounter.Add(pokemon);
            GetMoves(pokemon);
            CreateListItem(pokemon);
        }
    }

    private void Update() {
        if (PokedexManager.pokemonToEncounter.Count > 0 && PokedexManager.currentEntry == null) {
            try {
                GameObject nextEntry = GameObject.Find("Encounter Content").transform.GetChild(0).gameObject;
                OnSelected(nextEntry.GetComponent<PokedexEntry>().pokemon, nextEntry);
            } catch {
                ClearFields();
            }
        }

        if (nameField.text != "" && PokedexManager.currentEntry == null) {
            ClearFields();
        }
    }

    public void OnScan() {
        // Get the check box fields to apply to the private bools. 
        appendScan = GameObject.Find("Append to List").GetComponent<Toggle>().isOn;
        allowShinies = GameObject.Find("Allow Shinies").GetComponent<Toggle>().isOn;
        alwaysShiny = GameObject.Find("Always Shiny").GetComponent<Toggle>().isOn;
        allowLegendaries = GameObject.Find("Allow Legendaries").GetComponent<Toggle>().isOn;
        allowHeldItems = GameObject.Find("Allow Held Items").GetComponent<Toggle>().isOn;
        alwaysHoldItem = GameObject.Find("Always Hold Item").GetComponent<Toggle>().isOn;

        // Clean the lists to ensure proper data is used fro the scans
        encounterablePokemon = new List<Pokemon>();

        // Ensure that no pokemon is added that does not fit the parameters, like legendary or habitat. 
        // This should be pushed into its own function when all of the fields are added so to keep this function clean.
        foreach (Pokemon pokemon in PokedexManager.pokedex) {
            if (!allowLegendaries && pokemon.legendary) {
                continue;
            }
            string habitat = habitatDropdown.options[habitatDropdown.value].text;
            if (habitat != "Any Habitat" && !pokemon.habitat.Contains(habitat)) {
                continue;
            }
            string type = typeDropdown.options[typeDropdown.value].text;
            if (type != "Any Type" && !pokemon.type.Contains(type)) {
                continue;
            }
            string stage = stageDropdown.options[stageDropdown.value].text;
            if (stage == "Stage 1" && pokemon.stage != 1) {
                continue;
            } else if (stage == "Stage 2" && pokemon.stage != 2) {
                continue;
            } else if (stage == "Stage 3" && pokemon.stage != 3) {
                continue;
            }
            string species = pokemonDropdown.options[pokemonDropdown.value].text;
            if (species != "Any Pokemon" && species != pokemon.species) {
                continue;
            }
            encounterablePokemon.Add(pokemon);
        }
        if (encounterablePokemon.Count() < 1) {
            Debug.LogError("No pokemon to encounter with these settings.");
            return;
        }

        if (!appendScan) {
            PokedexManager.pokemonToEncounter = new List<Pokemon>();
        }

        // Clear out the old prefabs so that all UI items are correct ad their are no duplicates.
        // This is a quick and dirty method to do this, but could be optimized to just ensure no recreations of each encounterEntry in the future. 
        for (int i = 0; i < contentPanel.transform.childCount; i++) {
            Destroy(contentPanel.transform.GetChild(i).gameObject);
        }

        // Add a number of new pokemon to the list equal to the slider value.
        AddPokemon(encounterSlider.value);

        foreach(Pokemon pokemon in PokedexManager.pokemonToEncounter) {
            CreateListItem(pokemon);
        }
    }

    public void CreateListItem(Pokemon pokemon) {
        GameObject newPokemon = Instantiate(pokedexPrefab) as GameObject;
        PokedexEntry controller = newPokemon.GetComponent<PokedexEntry>();
        controller.pokemon = pokemon;
        controller.species.text = pokemon.species;
        newPokemon.transform.SetParent(contentPanel.transform);
        newPokemon.transform.localScale = Vector3.one;
        pokemon.sprite = PokedexManager.LoadSprite("PokemonIcons/" + pokemon.image);

        if (pokemon.sprite != null) {
            controller.sprite.sprite = pokemon.sprite;
            if (pokemon.shiny) {
                if (!pokemon.colorHasBeenSet) {
                    float h = UnityEngine.Random.Range(0.0f, 1.0f);
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

    public void OnSelected(Pokemon pokemon, GameObject entry) {
        PokedexManager.AssignCurrentPokemonAndEntry(entry);

        nameField.text = pokemon.name == null || pokemon.name == "" ? pokemon.species == null || pokemon.species == "" ? "???" : pokemon.species : pokemon.name;
        typeField.text = pokemon.type == null ? "Unkown" : pokemon.type;
        sizeField.text = pokemon.size == null ? "Unkown" : pokemon.size;
        weightField.text = pokemon.weight == null ? "Unkown" : pokemon.weight;
        genderField.text = pokemon.gender == null ? "Unkown" : pokemon.gender;
        natureField.text = pokemon.nature.name;

        hpBaseField.text = pokemon.hp.ToString();
        atkBaseField.text = pokemon.atk.ToString();
        defBaseField.text = pokemon.def.ToString();
        spatkBaseField.text = pokemon.spatk.ToString();
        spdefBaseField.text = pokemon.spdef.ToString();
        spdBaseField.text = pokemon.spd.ToString();

        hpLevelField.text = pokemon.hpLevel.ToString();
        atkLevelField.text = pokemon.atkLevel.ToString();
        defLevelField.text = pokemon.defLevel.ToString();
        spatkLevelField.text = pokemon.spatkLevel.ToString();
        spdefLevelField.text = pokemon.spdefLevel.ToString();
        spdLevelField.text = pokemon.spdLevel.ToString();

        hpCSField.text = pokemon.hpCS.ToString();
        atkCSField.text = pokemon.atkCS.ToString();
        defCSField.text = pokemon.defCS.ToString();
        spatkCSField.text = pokemon.spatkCS.ToString();
        spdefCSField.text = pokemon.spdefCS.ToString();
        spdCSField.text = pokemon.spdCS.ToString();

        maxHealthField.text = pokemon.maxHealth.ToString();
        currentHealthField.text = pokemon.currentHealth.ToString();

        int hpStage = pokemon.hpLevel / 10;
        int atkStage = pokemon.atkLevel / 10;
        int defStage = pokemon.defLevel / 10;
        int spatkStage = pokemon.spatkLevel / 10;
        int spdefStage = pokemon.spdefLevel / 10;
        int spdStage = pokemon.spdLevel / 10;

        if (hpStage == 0) { hpStage = 1; }
        if (atkStage == 0) { hpStage = 1; }
        if (defStage == 0) { hpStage = 1; }
        if (spatkStage == 0) { hpStage = 1; }
        if (spdefStage == 0) { hpStage = 1; }
        if (spdStage == 0) { hpStage = 1; }

        hpTotalField.text = (pokemon.hpLevel + (pokemon.hpCS * hpStage)).ToString();
        atkTotalField.text = (pokemon.atkLevel + (pokemon.atkCS * atkStage)).ToString();
        defTotalField.text = (pokemon.defLevel + (pokemon.defCS * defStage)).ToString();
        spatkTotalField.text = (pokemon.spatkLevel + (pokemon.spatkCS * spatkStage)).ToString();
        spdefTotalField.text = (pokemon.spdefLevel + (pokemon.spdefCS * spdefStage)).ToString();
        spdTotalField.text = (pokemon.spdLevel + (pokemon.spdCS * spdStage)).ToString();

        basicAbilityField.text = pokemon.basicAbility;
        advanceAbilityField.text = pokemon.advancedAbility;
        highAbilityField.text = pokemon.highAbility;

        levelField.text = pokemon.level.ToString();

        blindedToggle.isOn = pokemon.blind;
        totallyBlindedToggle.isOn = pokemon.totallyBlind;
        burnedToggle.isOn = pokemon.burned;
        confusedToggle.isOn = pokemon.confused;
        cursedToggle.isOn = pokemon.cursed;
        disabledToggle.isOn = pokemon.disabled;
        enragedToggle.isOn = pokemon.enraged;
        flinchedToggle.isOn = pokemon.flinched;
        frozenToggle.isOn = pokemon.frozen;
        infatuatedToggle.isOn = pokemon.infatuated;
        paralyzedToggle.isOn = pokemon.paralyzed;
        poisonedToggle.isOn = pokemon.poisoned;
        badlyPoisonedToggle.isOn = pokemon.badlyPoisoned;
        asleepToggle.isOn = pokemon.asleep;
        badlyAsleepToggle.isOn = pokemon.badlyAsleep;
        slowedToggle.isOn = pokemon.slowed;
        stuckToggle.isOn = pokemon.stuck;
        suppressedToggle.isOn = pokemon.suppressed;
        trappedToggle.isOn = pokemon.trapped;
        trippedToggle.isOn = pokemon.tripped;
        vulnerableToggle.isOn = pokemon.vulnerable;

        movesListField.text = "";
        if (pokemon.knownMoveList.Length > 0) {
            for (int i = pokemon.knownMoveList.Length - 1; i >= 0; i--) {
                movesListField.text += pokemon.knownMoveList[i].name + Environment.NewLine;
            }
        }

        if (pokemon.heldItem.name == null || pokemon.heldItem.name == "None" || pokemon.heldItem.name == "") {
            pokemon.heldItem = new Item() {
                name = "None",
                desc = "",
                image = "",
                sprite = PokedexManager.LoadSprite("ItemIcons/None")
            };
        } else {
            pokemon.heldItem.sprite = PokedexManager.LoadSprite("ItemIcons/" + pokemon.heldItem.image);
        }

        heldItemImage.sprite = pokemon.heldItem.sprite;
        heldItemNameField.text = pokemon.heldItem == null ? "None" : pokemon.heldItem.name;
        heldItemDescriptionField.text = pokemon.heldItem == null ? "" : pokemon.heldItem.desc;

        skillsListField.text = "Athl " + pokemon.athleticsDie.ToString() + "d6+" + pokemon.athleticsBonus.ToString() + Environment.NewLine +
            "Acro " + pokemon.acrobaticsDie.ToString() + "d6+" + pokemon.acrobaticsBonus.ToString() + Environment.NewLine +
            "Combat " + pokemon.combatDie.ToString() + "d6+" + pokemon.combatBonus.ToString() + Environment.NewLine +
            "Focus " + pokemon.focusDie.ToString() + "d6+" + pokemon.focusBonus.ToString() + Environment.NewLine +
            "Percep " + pokemon.perceptionDie.ToString() + "d6+" + pokemon.perceptionBonus.ToString() + Environment.NewLine +
            "Stealth " + pokemon.stealthDie.ToString() + "d6+" + pokemon.stealthBonus.ToString() + Environment.NewLine +
            "Edu:Tech " + pokemon.techEduDie.ToString() + "d6+" + pokemon.techEduBonus.ToString();

        capabilitiesListField.text = "";
        if (pokemon.capabilities.Length > 0) {
            for (int i = 0; i < pokemon.capabilities.Length; i++) {
                capabilitiesListField.text += pokemon.capabilities[i] + Environment.NewLine;
            }
        }

        captureRateField.text = pokemon.captureRate.ToString() + " or less";
    }

    public void UpdateEncounterSliderNumber() {
        Text sliderLabel = GameObject.Find("Encounter Slider Label").GetComponent<Text>();
        sliderLabel.text = "Count: " + encounterSlider.value.ToString();
    }

    public void UpdateMinLevelSliderNumber() {
        Text sliderLabel = GameObject.Find("Min Level Slider Label").GetComponent<Text>();
        sliderLabel.text = "Min Lvl: " + minLevelSlider.value.ToString();
        if (minLevelSlider.value > maxLevelSlider.value) {
            maxLevelSlider.value = minLevelSlider.value;
        }
    }

    public void UpdateMaxLevelSliderNumber() {
        Text sliderLabel = GameObject.Find("Max Level Slider Label").GetComponent<Text>();
        sliderLabel.text = "Max Lvl: " + maxLevelSlider.value.ToString();
        if (maxLevelSlider.value < minLevelSlider.value) {
            minLevelSlider.value = maxLevelSlider.value;
        }
    }

    public void PlayPokemonCry() {
        if (PokedexManager.currentPokemon == null) {
            Debug.Log("No pokemon currently selected");
            return;
        }
        if (PokedexManager.currentPokemon.cryAudio != null) {
            cryAudioSource.clip = PokedexManager.currentPokemon.cryAudio;
            cryAudioSource.Play();
        } else {
            Debug.LogError("Current Pokemon does not have a registered common cry");
        }
    }

    public void CaptureCurrentSelected() {
        Debug.Log(PokedexManager.currentPokemon.savePath);
        string newPath = PokedexManager.currentPokemon.savePath.Replace("tmp/", "Captured/");
        Debug.Log(newPath);
        Pokemon pokemon = PokedexManager.currentPokemon.Clone();
        pokemon.savePath = newPath;
        pokemon.ToJson(pokemon.savePath);
        PokedexManager.manager.DeleteCurrentPokemonAndEntry();
    }

    public void ClearFields() {
        nameField.text = "";
        typeField.text = "";
        sizeField.text = "";
        weightField.text = "";
        genderField.text = "";
        natureField.text = "";
        hpBaseField.text = "";
        atkBaseField.text = "";
        defBaseField.text = "";
        spatkBaseField.text = "";
        spdefBaseField.text = "";
        spdBaseField.text = "";
        hpLevelField.text = "";
        atkLevelField.text = "";
        defLevelField.text = "";
        spatkLevelField.text = "";
        spdefLevelField.text = "";
        spdLevelField.text = "";
        hpCSField.text = "";
        atkCSField.text = "";
        defCSField.text = "";
        spatkCSField.text = "";
        spdefCSField.text = "";
        spdCSField.text = "";
        hpTotalField.text = "";
        atkTotalField.text = "";
        defTotalField.text = "";
        spatkTotalField.text = "";
        spdefTotalField.text = "";
        spdTotalField.text = "";
        currentHealthField.text = "";
        maxHealthField.text = "";
        basicAbilityField.text = "";
        advanceAbilityField.text = "";
        highAbilityField.text = "";
        levelField.text = "";
        movesListField.text = "";
        skillsListField.text = "";
        capabilitiesListField.text = "";
        heldItemNameField.text = "";
        heldItemDescriptionField.text = "";
        heldItemImage.sprite = PokedexManager.LoadSprite("ItemIcons/None");
        cryAudioSource.clip = null;
        blindedToggle.isOn = false;
        totallyBlindedToggle.isOn = false;
        burnedToggle.isOn = false;
        confusedToggle.isOn = false;
        cursedToggle.isOn = false;
        disabledToggle.isOn = false;
        enragedToggle.isOn = false;
        flinchedToggle.isOn = false;
        frozenToggle.isOn = false;
        infatuatedToggle.isOn = false;
        paralyzedToggle.isOn = false;
        poisonedToggle.isOn = false;
        badlyPoisonedToggle.isOn = false;
        asleepToggle.isOn = false;
        badlyAsleepToggle.isOn = false;
        slowedToggle.isOn = false;
        stuckToggle.isOn = false;
        suppressedToggle.isOn = false;
        trappedToggle.isOn = false;
        trippedToggle.isOn = false;
        vulnerableToggle.isOn = false;
    }

    public void SetStats() {
        Pokemon pokemon = PokedexManager.currentPokemon;
        try {
            if (nameField.text != pokemon.species) {
                pokemon.name = nameField.text;
            }

            pokemon.type = typeField.text;
            pokemon.size = sizeField.text;
            pokemon.weight = weightField.text;
            pokemon.gender = genderField.text;

            pokemon.hp = int.Parse(hpBaseField.text);
            pokemon.atk = int.Parse(atkBaseField.text);
            pokemon.def = int.Parse(defBaseField.text);
            pokemon.spatk = int.Parse(spatkBaseField.text);
            pokemon.spdef = int.Parse(spdefBaseField.text);
            pokemon.spd = int.Parse(spdBaseField.text);

            pokemon.hpLevel = int.Parse(hpLevelField.text);
            pokemon.atkLevel = int.Parse(atkLevelField.text);
            pokemon.defLevel = int.Parse(defLevelField.text);
            pokemon.spatkLevel = int.Parse(spatkLevelField.text);
            pokemon.spdefLevel = int.Parse(spdefLevelField.text);
            pokemon.spdLevel = int.Parse(spdLevelField.text);

            pokemon.hpCS = int.Parse(hpCSField.text);
            pokemon.atkCS = int.Parse(atkCSField.text);
            pokemon.defCS = int.Parse(defCSField.text);
            pokemon.spatkCS = int.Parse(spatkCSField.text);
            pokemon.spdefCS = int.Parse(spdefCSField.text);
            pokemon.spdCS = int.Parse(spdCSField.text);

            pokemon.currentHealth = int.Parse(currentHealthField.text);
            pokemon.maxHealth = pokemon.level + (pokemon.hpLevel * 3) + 10;

            GetCaptureRate(pokemon);

            OnSelected(PokedexManager.currentPokemon, PokedexManager.currentEntry);
            pokemon.ToJson(pokemon.savePath);
        } catch {
            string errorMessage = "Failed to assign all values properly. Please check last input.";
            PokedexManager.manager.CreateWarningDialog(errorMessage);
        }
    }

    [SerializeField]
    public void SetCondition(int condition) {
        Pokemon pokemon = PokedexManager.currentPokemon;
        if ((Condition)condition == Condition.blinded) {
            pokemon.blind = blindedToggle.isOn;
        } else if ((Condition)condition == Condition.totallyBlinded) {
            pokemon.totallyBlind = totallyBlindedToggle.isOn;
        } else if ((Condition)condition == Condition.burned) {
            pokemon.burned = burnedToggle.isOn;
        } else if ((Condition)condition == Condition.confused) {
            pokemon.confused = confusedToggle.isOn;
        } else if ((Condition)condition == Condition.cursed) {
            pokemon.cursed = cursedToggle.isOn;
        } else if ((Condition)condition == Condition.disabled) {
            pokemon.disabled = disabledToggle.isOn;
        } else if ((Condition)condition == Condition.enraged) {
            pokemon.enraged = enragedToggle.isOn;
        } else if ((Condition)condition == Condition.flinched) {
            pokemon.flinched = flinchedToggle.isOn;
        } else if ((Condition)condition == Condition.frozen) {
            pokemon.frozen = frozenToggle.isOn;
        } else if ((Condition)condition == Condition.infatuated) {
            pokemon.infatuated = infatuatedToggle.isOn;
        } else if ((Condition)condition == Condition.paralyzed) {
            pokemon.paralyzed = paralyzedToggle.isOn;
        } else if ((Condition)condition == Condition.poisoned) {
            pokemon.poisoned = poisonedToggle.isOn;
        } else if ((Condition)condition == Condition.badlyPoisoned) {
            pokemon.badlyPoisoned = badlyPoisonedToggle.isOn;
        } else if ((Condition)condition == Condition.sleeping) {
            pokemon.asleep = asleepToggle.isOn;
        } else if ((Condition)condition == Condition.badlySleeping) {
            pokemon.badlyAsleep = badlyAsleepToggle.isOn;
        } else if ((Condition)condition == Condition.slowed) {
            pokemon.slowed = slowedToggle.isOn;
        } else if ((Condition)condition == Condition.stuck) {
            pokemon.stuck = stuckToggle.isOn;
        } else if ((Condition)condition == Condition.suppressed) {
            pokemon.suppressed = suppressedToggle.isOn;
        } else if ((Condition)condition == Condition.trapped) {
            pokemon.trapped = trappedToggle.isOn;
        } else if ((Condition)condition == Condition.tripped) {
            pokemon.tripped = trippedToggle.isOn;
        } else if ((Condition)condition == Condition.vulnerable) {
            pokemon.vulnerable = vulnerableToggle.isOn;
        }
        GetCaptureRate(PokedexManager.currentPokemon);
        OnSelected(PokedexManager.currentPokemon, PokedexManager.currentEntry);
        pokemon.ToJson(pokemon.savePath);
    }

    void AddPokemon(float numberToEncounter) {
        for (float i = 0; i < numberToEncounter; i += 1) {
            int index = UnityEngine.Random.Range(0, encounterablePokemon.Count);
            PokedexManager.pokemonToEncounter.Add(encounterablePokemon[index].Clone());

            Pokemon pokemon = PokedexManager.pokemonToEncounter[PokedexManager.pokemonToEncounter.Count - 1];

            pokemon.hpLevel = pokemon.hp;
            pokemon.atkLevel = pokemon.atk;
            pokemon.defLevel = pokemon.def;
            pokemon.spatkLevel = pokemon.spatk;
            pokemon.spdefLevel = pokemon.spdef;
            pokemon.spdLevel = pokemon.spd;

            pokemon.maxHealth = pokemon.level + (pokemon.hpLevel * 3) + 10;
            pokemon.currentHealth = pokemon.maxHealth;

            GetCries(pokemon);
            SetBaseRelations(pokemon);
            LevelPokemon(pokemon);
            GetGender(pokemon);
            GetAbilities(pokemon);
            GetNature(pokemon);
            GetMoves(pokemon);
            GetHeldItem(pokemon);
            GetCaptureRate(pokemon);

            if (allowShinies) {
                int shinyChance = UnityEngine.Random.Range(0, 8192);
                if (alwaysShiny) { shinyChance = 0; }
                pokemon.shiny = shinyChance == 0 ? true : false;
            } else {
                pokemon.shiny = false;
            }

            try {
                string path = Path.Combine("tmp/", pokemon.level + "_" + pokemon.species + ".json");
                pokemon.ToJson(path);
            } catch { Debug.Log("Failed to save out " + pokemon.species); }
        }
    }

    void LevelPokemon(Pokemon pokemon) {
        pokemon.level = Mathf.RoundToInt(UnityEngine.Random.Range(minLevelSlider.value, maxLevelSlider.value));
        int points = 10 + (pokemon.level - 1);

        for (int i = 0; i < points; i++) {
            int stat = UnityEngine.Random.Range(0, 6);
            if (stat < 5) {
                if (pokemon.baseRelations[stat].value >= pokemon.baseRelations[stat + 1].value) {
                    AdjustStatByName(pokemon, pokemon.baseRelations[stat].name);
                } else {
                    i--;
                }
            } else {
                AdjustStatByName(pokemon, pokemon.baseRelations[stat].name);
            }
        }
    }

    void AdjustStatByName(Pokemon pokemon, string name) {
        if (name == "HP") {
            pokemon.hpLevel++;
        } else if (name == "ATK") {
            pokemon.atkLevel++;
        } else if (name == "DEF") {
            pokemon.defLevel++;
        } else if (name == "SPATK") {
            pokemon.spatkLevel++;
        } else if (name == "SPDEF") {
            pokemon.spdefLevel++;
        } else if (name == "SPD") {
            pokemon.spdLevel++;
        } else {
            Debug.LogError("ATTRIBUTE NAME NOT FOUND: " + name);
        }
    }

    void SetBaseRelations(Pokemon pokemon) {
        pokemon.baseRelations = new BaseRelations[] {
            new BaseRelations("HP", pokemon.hp),
            new BaseRelations("ATK", pokemon.atk),
            new BaseRelations("DEF", pokemon.def),
            new BaseRelations("SPATK", pokemon.spatk),
            new BaseRelations("SPDEF", pokemon.spdef),
            new BaseRelations("SPD", pokemon.spd),
        };

        pokemon.baseRelations = pokemon.baseRelations.OrderBy(x => x.value).ToArray();

        for (int i = 0; i < pokemon.baseRelations.Length; i++) {
            pokemon.baseRelations[i].position = i;
        }
    }

    void GetGender(Pokemon pokemon) {
        string[] genderSplit = pokemon.gender.Split(' ');
        if (genderSplit.Count() == 5) {
            float male = float.Parse(genderSplit[0].Replace("%", ""));

            float chance = UnityEngine.Random.Range(0, 101);
            if (chance > male) {
                pokemon.gender = "Female";
            } else {
                pokemon.gender = "Male";
            }
        }
    }

    void GetAbilities(Pokemon pokemon) {
        int choice = UnityEngine.Random.Range(0, pokemon.basicAbilities.Length);
        pokemon.basicAbility = pokemon.basicAbilities.Length == 0 ? "None" : pokemon.basicAbilities[choice];
        if (pokemon.level >= 20) {
            choice = UnityEngine.Random.Range(0, pokemon.advancedAbilities.Length);
            pokemon.advancedAbility = pokemon.advancedAbilities.Length == 0 ? "None" : pokemon.advancedAbilities[choice];
            if (pokemon.level >= 40) {
                choice = UnityEngine.Random.Range(0, pokemon.highAbilities.Length);
                pokemon.highAbility = pokemon.highAbilities.Length == 0 ? "None" : pokemon.highAbilities[choice];
            } else {
                pokemon.highAbility = "None";
            }
        } else {
            pokemon.advancedAbility = "None";
            pokemon.highAbility = "None";
        }
    }

    void GetNature(Pokemon pokemon) {
        string natureString = natureDropdown.options[natureDropdown.value].text;
        Nature nature;
        if (natureString != "Any Nature") {
            nature = PokedexManager.natures.First(x => x.name == natureString);
        } else {
            nature = PokedexManager.natures[UnityEngine.Random.Range(0, PokedexManager.natures.Length)];
        }
        pokemon.nature = nature;
        ModifyBaseStatForNature(pokemon);
        ModifyBaseStatForNature(pokemon, true);
    }

    void ModifyBaseStatForNature(Pokemon pokemon, bool decrease = false) {
        int value = 2;
        if (decrease) {
            value *= -1;
            string name = pokemon.nature.down.ToLower();
        } else {
            string name = pokemon.nature.up.ToLower();
        }
        if (name == "hp") {
            pokemon.hp += value / 2;
        } else if (name == "atk") {
            pokemon.atk += value;
        } else if (name == "def") {
            pokemon.def += value;
        } else if (name == "spatk") {
            pokemon.spatk += value;
        } else if (name == "spdef") {
            pokemon.spdef += value;
        } else if (name == "spd") {
            pokemon.spd += value;
        }
    }

    public static void GetMoves(Pokemon pokemon) {
        List<Move> moveList = new List<Move>();
        List<Move> knownMoveList = new List<Move>();
        foreach (string move in pokemon.moves) {
            try {
                string[] moveSplit = move.Split('-');
                string type = moveSplit[moveSplit.Length - 1].Trim();
                string[] levelAndName = moveSplit[0].Split(' ');
                int level = int.Parse(levelAndName[0].Trim());
                string name = "";
                for (int i = 1; i < levelAndName.Length; i++) {
                    name += " " + levelAndName[i];
                }
                name = name.Trim();
                moveList.Add(new Move(name, level, type));
            } catch {
                Debug.Log(move);
            }
        }
        pokemon.movesList = moveList.ToArray();
        pokemon.movesList = pokemon.movesList.OrderBy(x => x.level).ToArray();
        foreach (Move move in pokemon.movesList) {
            if (pokemon.level >= move.level) {
                knownMoveList.Add(move);
            }
            if (knownMoveList.Count() > 7) {
                knownMoveList.RemoveAt(0);
            }
        }
        pokemon.knownMoveList = knownMoveList.ToArray();
        pokemon.knownMoveList = pokemon.knownMoveList.OrderBy(x => x.level).ToArray();
    }

    void GetHeldItem(Pokemon pokemon) {
        if (allowHeldItems) {
            int chance = UnityEngine.Random.Range(0, 10);
            if (alwaysHoldItem) { chance = 0; }
            if (chance == 0) {
                chance = UnityEngine.Random.Range(0, PokedexManager.items.Length);
                pokemon.heldItem = PokedexManager.items[chance];
            }
        }
    }

    void GetCaptureRate(Pokemon pokemon) {
        pokemon.captureRate = 100 - (pokemon.level * 2);

        float health = (float)pokemon.currentHealth / (float)pokemon.maxHealth;
        Debug.Log(health);
        if (health > 0.75) {
            pokemon.captureRate += -30;
        } else if (health > 0.5) {
            pokemon.captureRate += -15;
        } else if (health > 0.25) {
            pokemon.captureRate += 0;
        } else {
            pokemon.captureRate += 15;
        }

        string finalStage = pokemon.evolutions[pokemon.evolutions.Length - 1][0].ToString();
        Debug.Log("Final Stage: " + finalStage);
        int maxStage = int.Parse(finalStage);
        if (maxStage - pokemon.stage == 0) {
            pokemon.captureRate += -10;
        } else if (maxStage - pokemon.stage == 1) {
            pokemon.captureRate += 0;
        } else if (maxStage - pokemon.stage == 2) {
            pokemon.captureRate += 10;
        }

        if (pokemon.shiny) { pokemon.captureRate += -10; }
        if (pokemon.legendary) { pokemon.captureRate += -30; }

        if (pokemon.burned) { pokemon.captureRate += 10; }
        if (pokemon.frozen) { pokemon.captureRate += 10; }
        if (pokemon.paralyzed) { pokemon.captureRate += 10; }
        if (pokemon.poisoned) { pokemon.captureRate += 10; }
        if (pokemon.stuck) { pokemon.captureRate += 10; }

        if (pokemon.confused) { pokemon.captureRate += 5; }
        if (pokemon.cursed) { pokemon.captureRate += 5; }
        if (pokemon.disabled) { pokemon.captureRate += 5; }
        if (pokemon.enraged) { pokemon.captureRate += 5; }
        if (pokemon.flinched) { pokemon.captureRate += 5; }
        if (pokemon.infatuated) { pokemon.captureRate += 5; }
        if (pokemon.asleep) { pokemon.captureRate += 5; }
        if (pokemon.suppressed) { pokemon.captureRate += 5; }
        if (pokemon.slowed) { pokemon.captureRate += 5; }

        pokemon.captureRate += 5 * pokemon.injuries;
    }

    void GetCries(Pokemon pokemon) {
        if (pokemon.cryAudio == null) {
            string cryLocation = Path.Combine(Application.streamingAssetsPath, "Cries/" + pokemon.cry + ".ogg");
            if (!File.Exists(cryLocation)) {
                Debug.LogError("Cry could not be found: " + cryLocation);
            } else {
                StartCoroutine(LoadClipCoroutine("file:///" + cryLocation, pokemon));
            }
        }
    }

    IEnumerator<UnityWebRequestAsyncOperation> LoadClipCoroutine(string file, Pokemon pokemon) {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(file, AudioType.OGGVORBIS)) {
            yield return www.SendWebRequest();

            if (www.isNetworkError) {
                Debug.Log(www.error);
            } else {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                Debug.Log(clip.name + " has a length of: " + clip.length);
                pokemon.cryAudio = clip; 
            }
        }
    }
}