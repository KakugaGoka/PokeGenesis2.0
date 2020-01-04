using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterController : MonoBehaviour {
    public GameObject pokedexPrefab;
    public GameObject contentPanel;
    public Slider encounterSlider;
    public Slider minLevelSlider;
    public Slider maxLevelSlider;
    public Image heldItemImage;
    public AudioSource cryAudioSource;

    private List<Pokemon> encounterablePokemon = new List<Pokemon>();

    private bool
        appendScan,
        allowShinies,
        alwaysShiny,
        allowLegendaries,
        allowHeldItems,
        alwaysHoldItem,
        scanInProgress;

    private Dropdown
        abilityDropdown,
        capabilityDropdown,
        skillDropdown,
        moveDropdown,
        conditionDropdown,
        natureDropdown,
        typeDropdown,
        pokemonDropdown,
        stageDropdown,
        habitatDropdown;

    private Toggle
        conditionToggle;

    private Button
        megaButton,
        altMegaButton;

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
        levelField,
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
        levelField = GameObject.Find("Level Field").GetComponent<InputField>();
        heldItemNameField = GameObject.Find("Held Item Name Field").GetComponent<InputField>();
        captureRateField = GameObject.Find("Capture Rate Field").GetComponent<InputField>();

        conditionToggle = GameObject.Find("Condition Toggle").GetComponent<Toggle>();

        megaButton = GameObject.Find("Mega Button").GetComponent<Button>();
        altMegaButton = GameObject.Find("Alt Mega Button").GetComponent<Button>();
        megaButton.interactable = false;
        altMegaButton.interactable = false;

        moveDropdown = GameObject.Find("Moves Dropdown").GetComponent<Dropdown>();
        capabilityDropdown = GameObject.Find("Capabilities Dropdown").GetComponent<Dropdown>();
        abilityDropdown = GameObject.Find("Abilities Dropdown").GetComponent<Dropdown>();
        skillDropdown = GameObject.Find("Skills Dropdown").GetComponent<Dropdown>();
        conditionDropdown = GameObject.Find("Conditions Dropdown").GetComponent<Dropdown>();
        natureDropdown = GameObject.Find("Nature Dropdown").GetComponent<Dropdown>();
        habitatDropdown = GameObject.Find("Habitat Dropdown").GetComponent<Dropdown>();
        stageDropdown = GameObject.Find("Stage Dropdown").GetComponent<Dropdown>();
        pokemonDropdown = GameObject.Find("Pokemon Dropdown").GetComponent<Dropdown>();
        typeDropdown = GameObject.Find("Type Dropdown").GetComponent<Dropdown>();

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
                Debug.LogWarning("Cry not found for pokemon: " + pokemon.species);
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

        // Verify all pokemon moves
        foreach (Pokemon pokemon in PokedexManager.pokedex) {
            pokemon.level = 100;
            pokemon.GetMoves();
            foreach (Move move in pokemon.movesList) {
                bool nameFound = false;
                foreach (Move registeredMove in PokedexManager.moves) {
                    if (move.name == registeredMove.name) {
                        nameFound = true;
                    }
                }
                if (!nameFound) {
                    Debug.LogWarning("Move not found: " + move.name);
                }
            }
        }

        // Verify all pokemon abilites
        foreach (Pokemon pokemon in PokedexManager.pokedex) {
            List<string> abilities = pokemon.basicAbilities.ToList();
            try {
                foreach (var item in pokemon.advancedAbilities) {
                    abilities.Add(item);
                }
            } catch { Debug.LogWarning("Pokemon does not have advanced abilities: " + pokemon.species); }
            try {
            foreach (var item in pokemon.highAbilities) {
                    abilities.Add(item);
            }
            } catch { Debug.LogWarning("Pokemon does not have high abilities: " + pokemon.species); }
            foreach (string ability in abilities) {
                bool nameFound = false;
                foreach (Ability registeredAbility in PokedexManager.abilities) {
                    if (ability == registeredAbility.name) {
                        nameFound = true;
                    }
                }
                if (!nameFound) {
                    Debug.LogWarning("Ability not found: " + ability);
                }
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
        PokedexManager.pokemonToEncounter = new List<Pokemon>();
        foreach (var file in myFiles) {
            Pokemon pokemon = Pokemon.FromJson(file);
            PokedexManager.pokemonToEncounter.Add(pokemon);
            pokemon.GetMoves();
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
        if (scanInProgress) { return; }
        Debug.Log("Scan Begin");
        scanInProgress = true;
        // Get the check box fields to apply to the private bools. 
        appendScan = GameObject.Find("Append to List").GetComponent<Toggle>().isOn;
        allowShinies = GameObject.Find("Allow Shinies").GetComponent<Toggle>().isOn;
        alwaysShiny = GameObject.Find("Always Shiny").GetComponent<Toggle>().isOn;
        allowLegendaries = GameObject.Find("Allow Legendaries").GetComponent<Toggle>().isOn;
        allowHeldItems = GameObject.Find("Allow Held Items").GetComponent<Toggle>().isOn;
        alwaysHoldItem = GameObject.Find("Always Hold Item").GetComponent<Toggle>().isOn;

        // Clean the lists to ensure proper data is used fro the scans
        encounterablePokemon = new List<Pokemon>();
        Debug.Log("UI Info Gathered");

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
        Debug.Log("Encounterable Pokemon Gathered");
        if (encounterablePokemon.Count() < 1) {
            Debug.LogError("No pokemon to encounter with these settings.");
            return;
        }

        if (!appendScan) {
            PokedexManager.pokemonToEncounter = new List<Pokemon>();
            foreach (var file in Directory.EnumerateFiles(Path.Combine(Application.streamingAssetsPath, "tmp/"))) {
                File.Delete(file);
            }
        }

        // Clear out the old prefabs so that all UI items are correct ad their are no duplicates.
        // This is a quick and dirty method to do this, but could be optimized to just ensure no recreations of each encounterEntry in the future. 
        for (int i = 0; i < contentPanel.transform.childCount; i++) {
            Destroy(contentPanel.transform.GetChild(i).gameObject);
        }
        Debug.Log("Old Displays Destroyed");

        // Add a number of new pokemon to the list equal to the slider value.
        AddPokemon(encounterSlider.value);

        foreach(Pokemon pokemon in PokedexManager.pokemonToEncounter) {
            CreateListItem(pokemon);
        }

        scanInProgress = false;
    }

    public void CreateListItem(Pokemon pokemon) {
        GameObject newPokemon = Instantiate(pokedexPrefab) as GameObject;
        PokedexEntry controller = newPokemon.GetComponent<PokedexEntry>();
        controller.pokemon = pokemon;
        controller.species.text = pokemon.CheckForNickname();
        newPokemon.transform.SetParent(contentPanel.transform);
        newPokemon.transform.localScale = Vector3.one;
        if (pokemon.mega.inMegaForm) {
            pokemon.mega.sprite = PokedexManager.LoadSprite("PokemonIcons/" + pokemon.mega.image);
            if (pokemon.mega.sprite != null) {
                controller.sprite.sprite = pokemon.mega.sprite;
            } else {
                string errorMessage = "Pokemon Sprite could not be loaded from: Icons/" + pokemon.mega.image;
                PokedexManager.manager.CreateWarningDialog(errorMessage);
            }
        } else if (pokemon.altMega.inMegaForm) {
            pokemon.altMega.sprite = PokedexManager.LoadSprite("PokemonIcons/" + pokemon.altMega.image);
            if (pokemon.altMega.sprite != null) {
                controller.sprite.sprite = pokemon.altMega.sprite;
            } else {
                string errorMessage = "Pokemon Sprite could not be loaded from: Icons/" + pokemon.altMega.image;
                PokedexManager.manager.CreateWarningDialog(errorMessage);
            }
        } else {
            pokemon.sprite = PokedexManager.LoadSprite("PokemonIcons/" + pokemon.image);
            if (pokemon.sprite != null) {
                controller.sprite.sprite = pokemon.sprite;
            } else {
                string errorMessage = "Pokemon Sprite could not be loaded from: Icons/" + pokemon.image;
                PokedexManager.manager.CreateWarningDialog(errorMessage);
            }
        }
        if (pokemon.shiny) {
            controller.shiny.SetActive(true);
        }
    }

    public void OnSelected(Pokemon pokemon, GameObject entry) {
        ClearFields();
        PokedexManager.AssignCurrentPokemonAndEntry(entry);

        nameField.text = pokemon.CheckForNickname();
        typeField.text = pokemon.GetCurrentType();
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

        hpTotalField.text = (pokemon.hp + pokemon.hpLevel + (pokemon.hpCS * hpStage)).ToString();
        atkTotalField.text = (pokemon.atk + pokemon.atkLevel + (pokemon.atkCS * atkStage)).ToString();
        defTotalField.text = (pokemon.def + pokemon.defLevel + (pokemon.defCS * defStage)).ToString();
        spatkTotalField.text = (pokemon.spatk + pokemon.spatkLevel + (pokemon.spatkCS * spatkStage)).ToString();
        spdefTotalField.text = (pokemon.spdef + pokemon.spdefLevel + (pokemon.spdefCS * spdefStage)).ToString();
        spdTotalField.text = (pokemon.spd + pokemon.spdLevel + (pokemon.spdCS * spdStage)).ToString();

        levelField.text = pokemon.level.ToString();

        megaButton.interactable = pokemon.HasMega();
        if (megaButton.interactable) {
            megaButton.GetComponentInChildren<Text>().text = pokemon.mega.name;
        }
        altMegaButton.interactable = pokemon.HasAltMega();
        if (altMegaButton.interactable) {
            altMegaButton.GetComponentInChildren<Text>().text = pokemon.altMega.name;
        }

        List<Dropdown.OptionData> abilitiesList = new List<Dropdown.OptionData>();
        foreach (var ability in pokemon.currentAbilities) {
            abilitiesList.Add(new Dropdown.OptionData(ability));
        }
        abilityDropdown.ClearOptions();
        abilityDropdown.AddOptions(abilitiesList);

        List<Dropdown.OptionData> capabilityList = new List<Dropdown.OptionData>();
        foreach (var item in pokemon.capabilities) {
            capabilityList.Add(new Dropdown.OptionData(item));
        }
        capabilityDropdown.ClearOptions();
        capabilityDropdown.AddOptions(capabilityList);

        List<Dropdown.OptionData> conditionsList = new List<Dropdown.OptionData>();
        conditionsList.Add(new Dropdown.OptionData("Blinded"));
        conditionsList.Add(new Dropdown.OptionData("Totally Blinded"));
        conditionsList.Add(new Dropdown.OptionData("Burned"));
        conditionsList.Add(new Dropdown.OptionData("Confused"));
        conditionsList.Add(new Dropdown.OptionData("Cursed"));
        conditionsList.Add(new Dropdown.OptionData("Disabled"));
        conditionsList.Add(new Dropdown.OptionData("Enraged"));
        conditionsList.Add(new Dropdown.OptionData("Flinched"));
        conditionsList.Add(new Dropdown.OptionData("Frozen"));
        conditionsList.Add(new Dropdown.OptionData("Infatuated"));
        conditionsList.Add(new Dropdown.OptionData("Paralyzed"));
        conditionsList.Add(new Dropdown.OptionData("Poisoned"));
        conditionsList.Add(new Dropdown.OptionData("Badly Poisoned"));
        conditionsList.Add(new Dropdown.OptionData("Sleeping"));
        conditionsList.Add(new Dropdown.OptionData("Heavily Sleeping"));
        conditionsList.Add(new Dropdown.OptionData("Slowed"));
        conditionsList.Add(new Dropdown.OptionData("Stuck"));
        conditionsList.Add(new Dropdown.OptionData("Suppressed"));
        conditionsList.Add(new Dropdown.OptionData("Trapped"));
        conditionsList.Add(new Dropdown.OptionData("Tripped"));
        conditionsList.Add(new Dropdown.OptionData("Vulnerable"));
        conditionDropdown.ClearOptions();
        conditionDropdown.AddOptions(conditionsList);
        GetCondition();

        List<Dropdown.OptionData> moveList = new List<Dropdown.OptionData>();
        foreach (var item in pokemon.knownMoveList) {
            moveList.Add(new Dropdown.OptionData(item.name));
        }
        moveDropdown.ClearOptions();
        moveDropdown.AddOptions(moveList);

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

        List<Dropdown.OptionData> skillList = new List<Dropdown.OptionData>();
        skillList.Add(new Dropdown.OptionData("Athl " + pokemon.athleticsDie.ToString() + "d6+" + pokemon.athleticsBonus.ToString()));
        skillList.Add(new Dropdown.OptionData("Acro " + pokemon.acrobaticsDie.ToString() + "d6+" + pokemon.acrobaticsBonus.ToString()));
        skillList.Add(new Dropdown.OptionData("Combat " + pokemon.combatDie.ToString() + "d6+" + pokemon.combatBonus.ToString()));
        skillList.Add(new Dropdown.OptionData("Focus " + pokemon.focusDie.ToString() + "d6+" + pokemon.focusBonus.ToString()));
        skillList.Add(new Dropdown.OptionData("Percep " + pokemon.perceptionDie.ToString() + "d6+" + pokemon.perceptionBonus.ToString()));
        skillList.Add(new Dropdown.OptionData("Stealth " + pokemon.stealthDie.ToString() + "d6+" + pokemon.stealthBonus.ToString()));
        skillList.Add(new Dropdown.OptionData("Edu:Tech " + pokemon.techEduDie.ToString() + "d6+" + pokemon.techEduBonus.ToString()));
        skillDropdown.ClearOptions();
        skillDropdown.AddOptions(skillList);

        heldItemImage.sprite = pokemon.heldItem.sprite;
        heldItemNameField.text = pokemon.heldItem == null ? "None" : pokemon.heldItem.name;

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

    public void DeletePokemon() {
        Pokemon pokemon = PokedexManager.currentPokemon;
        string name = pokemon.CheckForNickname();
        string message = "Are you sure you wish to remove " + name + " from the encounter list?";
        PokedexManager.manager.CreateConfirmationDialog(message, ConfirmationType.delete);
    }

    public void CapturePokemon() {
        Pokemon pokemon = PokedexManager.currentPokemon;
        string name = pokemon.CheckForNickname();
        string message = "Are you sure you wish to capture " + name + "?";
        PokedexManager.manager.CreateConfirmationDialog(message, ConfirmationType.capture);
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
        levelField.text = "";
        heldItemNameField.text = "";
        heldItemImage.sprite = PokedexManager.LoadSprite("ItemIcons/None");
        cryAudioSource.clip = null;
        moveDropdown.ClearOptions();
        skillDropdown.ClearOptions();
        capabilityDropdown.ClearOptions();
        abilityDropdown.ClearOptions();
        conditionDropdown.ClearOptions();
        megaButton.GetComponentInChildren<Text>().text = "";
        altMegaButton.GetComponentInChildren<Text>().text = "";
    }

    public void SetStats() {
        Pokemon pokemon = PokedexManager.currentPokemon;
        try {
            if (nameField.text != pokemon.species) {
                pokemon.nickname = nameField.text;
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
            pokemon.maxHealth = PokedexManager.GetMaxHealth(pokemon);

            pokemon.heldItem.name = heldItemNameField.text;

            pokemon.GetCaptureRate();

            OnSelected(PokedexManager.currentPokemon, PokedexManager.currentEntry);
            pokemon.ToJson(pokemon.savePath, true);
        } catch {
            string errorMessage = "Failed to assign all values properly. Please check last input.";
            PokedexManager.manager.CreateWarningDialog(errorMessage);
        }
    }

    public void GetCondition() {
        Pokemon pokemon = PokedexManager.currentPokemon;
        switch (conditionDropdown.options[conditionDropdown.value].text) {
            case "Blinded":
                conditionToggle.isOn = pokemon.blind;
                return;
            case "Totally Blinded":
                conditionToggle.isOn = pokemon.totallyBlind;
                return;
            case "Burned":
                conditionToggle.isOn = pokemon.burned;
                return;
            case "Confused":
                conditionToggle.isOn = pokemon.confused;
                return;
            case "Cursed":
                conditionToggle.isOn = pokemon.cursed;
                return;
            case "Disabled":
                conditionToggle.isOn = pokemon.disabled;
                return;
            case "Enraged":
                conditionToggle.isOn = pokemon.enraged;
                return;
            case "Flinched":
                conditionToggle.isOn = pokemon.flinched;
                return;
            case "Frozen":
                conditionToggle.isOn = pokemon.frozen;
                return;
            case "Infatuated":
                conditionToggle.isOn = pokemon.infatuated;
                return;
            case "Paralyzed":
                conditionToggle.isOn = pokemon.paralyzed;
                return;
            case "Poisoned":
                conditionToggle.isOn = pokemon.poisoned;
                return;
            case "Badly Poisoned":
                conditionToggle.isOn = pokemon.badlyPoisoned;
                return;
            case "Sleeping":
                conditionToggle.isOn = pokemon.asleep;
                return;
            case "Heavily Sleeping":
                conditionToggle.isOn = pokemon.badlyAsleep;
                return;
            case "Slowed":
                conditionToggle.isOn = pokemon.slowed;
                return;
            case "Stuck":
                conditionToggle.isOn = pokemon.stuck;
                return;
            case "Suppressed":
                conditionToggle.isOn = pokemon.suppressed;
                return;
            case "Trapped":
                conditionToggle.isOn = pokemon.trapped;
                return;
            case "Tripped":
                conditionToggle.isOn = pokemon.tripped;
                return;
            case "Vulnerable":
                conditionToggle.isOn = pokemon.vulnerable;
                return;
        }
    }

    public void SetCondition() {
        Pokemon pokemon = PokedexManager.currentPokemon;
        switch (conditionDropdown.options[conditionDropdown.value].text) {
            case "Blinded":
                pokemon.blind = conditionToggle.isOn;
                return;
            case "Totally Blinded":
                pokemon.totallyBlind = conditionToggle.isOn;
                return;
            case "Burned":
                pokemon.burned = conditionToggle.isOn;
                return;
            case "Confused":
                pokemon.confused = conditionToggle.isOn;
                return;
            case "Cursed":
                pokemon.cursed = conditionToggle.isOn;
                return;
            case "Disabled":
                pokemon.disabled = conditionToggle.isOn;
                return;
            case "Enraged":
                pokemon.enraged = conditionToggle.isOn;
                return;
            case "Flinched":
                pokemon.flinched = conditionToggle.isOn;
                return;
            case "Frozen":
                pokemon.frozen = conditionToggle.isOn;
                return;
            case "Infatuated":
                pokemon.infatuated = conditionToggle.isOn;
                return;
            case "Paralyzed":
                pokemon.paralyzed = conditionToggle.isOn;
                return;
            case "Poisoned":
                pokemon.poisoned = conditionToggle.isOn;
                return;
            case "Badly Poisoned":
                pokemon.badlyPoisoned = conditionToggle.isOn;
                return;
            case "Sleeping":
                pokemon.asleep = conditionToggle.isOn;
                return;
            case "Heavily Sleeping":
                pokemon.badlyAsleep = conditionToggle.isOn;
                return;
            case "Slowed":
                pokemon.slowed = conditionToggle.isOn;
                return;
            case "Stuck":
                pokemon.stuck = conditionToggle.isOn;
                return;
            case "Suppressed":
                pokemon.suppressed = conditionToggle.isOn;
                return;
            case "Trapped":
                pokemon.trapped = conditionToggle.isOn;
                return;
            case "Tripped":
                pokemon.tripped = conditionToggle.isOn;
                return;
            case "Vulnerable":
                pokemon.vulnerable = conditionToggle.isOn;
                return;
        }
        PokedexManager.currentPokemon.GetCaptureRate();
        OnSelected(PokedexManager.currentPokemon, PokedexManager.currentEntry);
        pokemon.ToJson(pokemon.savePath, true);
    }

    void AddPokemon(float numberToEncounter) {
        for (float i = 0; i < numberToEncounter; i += 1) {
            int number = 0;
            do {
                number = Random.Range(1, PokedexManager.manager.GetHighestNumberInPokemonArray(encounterablePokemon.ToArray()) + 1);
            } while (encounterablePokemon.Where(x => x.number == number).Count() < 1);
            Pokemon[] pokemonList = encounterablePokemon.Where(x => x.number == number).ToArray();
            int index = Random.Range(0, pokemonList.Count());
            PokedexManager.pokemonToEncounter.Add(pokemonList[index].Clone());
            Pokemon pokemon = PokedexManager.pokemonToEncounter[PokedexManager.pokemonToEncounter.Count - 1];

            string natureString = natureDropdown.options[natureDropdown.value].text;
            int levelToApply = (int)Random.Range(minLevelSlider.value, maxLevelSlider.value);

            pokemon.hpLevel = 0;
            pokemon.atkLevel = 0;
            pokemon.defLevel = 0;
            pokemon.spatkLevel = 0;
            pokemon.spdefLevel = 0;
            pokemon.spdLevel = 0;

            pokemon.GetCries();
            Debug.Log("Cries Gathered");
            pokemon.GetNature(natureString);
            Debug.Log("Nature Gathered");
            pokemon.SetBaseRelations();
            Debug.Log("Base Relations Gathered");
            pokemon.LevelPokemon(levelToApply);
            Debug.Log("Level Gathered");
            pokemon.GetGender();
            Debug.Log("Gender Gathered");
            pokemon.GetAbilities();
            Debug.Log("Abilities Gathered");
            pokemon.GetSkills();
            Debug.Log("Skills Gathered");
            pokemon.GetMoves();
            Debug.Log("Moves Gathered");
            if (allowShinies) pokemon.GetShiny(alwaysShiny);
            if (allowHeldItems) pokemon.GetHeldItem(alwaysHoldItem);

            pokemon.maxHealth = PokedexManager.GetMaxHealth(pokemon);
            pokemon.currentHealth = pokemon.maxHealth;
            pokemon.loyalty = 2;

            pokemon.GetCaptureRate();

            try {
                string path = Path.Combine("tmp/", pokemon.level + "_" + pokemon.species + ".json");
                pokemon.savePath = path;
                pokemon.ToJson(path);
            } catch { Debug.Log("Failed to save out " + pokemon.species); }
        }
    }

    public void ToggleMega() {
        Pokemon pokemon = PokedexManager.currentPokemon;
        Text megaText = megaButton.gameObject.GetComponentInChildren<Text>();
        if (pokemon.mega.inMegaForm) {
            megaText.text = "Mega Evolve";
            pokemon.UnapplyMega();
        } else {
            megaText.text = "De-Mega Evolve";
            pokemon.ApplyMega();
        }
        OnSelected(pokemon, PokedexManager.currentEntry);
        pokemon.ToJson(pokemon.savePath, true);
    }

    public void ToggleAltMega() {
        Pokemon pokemon = PokedexManager.currentPokemon;
        Text megaText = altMegaButton.gameObject.GetComponentInChildren<Text>();
        if (pokemon.altMega.inMegaForm) {
            megaText.text = "Mega Evolve";
            pokemon.UnapplyAltMega();
        } else {
            megaText.text = "De-Mega Evolve";
            pokemon.ApplyAltMega();
        }
        OnSelected(pokemon, PokedexManager.currentEntry);
        pokemon.ToJson(pokemon.savePath, true);
    }
}