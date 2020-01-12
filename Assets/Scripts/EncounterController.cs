using System;
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
        altMegaButton,
        dynaButton;

    private Image
        megaImage,
        altMegaImage;

    private UnityEngine.UI.InputField
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
        dynaLevelField,
        heldItemNameField,
        captureRateField;

    private void Start() {
        nameField = GameObject.Find("Name Field").GetComponent<UnityEngine.UI.InputField>();
        typeField = GameObject.Find("Types Field").GetComponent<UnityEngine.UI.InputField>();
        sizeField = GameObject.Find("Size Field").GetComponent<UnityEngine.UI.InputField>();
        weightField = GameObject.Find("Weight Field").GetComponent<UnityEngine.UI.InputField>();
        genderField = GameObject.Find("Gender Field").GetComponent<UnityEngine.UI.InputField>();
        natureField = GameObject.Find("Nature Field").GetComponent<UnityEngine.UI.InputField>();
        hpBaseField = GameObject.Find("HP Base Field").GetComponent<UnityEngine.UI.InputField>();
        atkBaseField = GameObject.Find("ATK Base Field").GetComponent<UnityEngine.UI.InputField>();
        defBaseField = GameObject.Find("DEF Base Field").GetComponent<UnityEngine.UI.InputField>();
        spatkBaseField = GameObject.Find("SPATK Base Field").GetComponent<UnityEngine.UI.InputField>();
        spdefBaseField = GameObject.Find("SPDEF Base Field").GetComponent<UnityEngine.UI.InputField>();
        spdBaseField = GameObject.Find("SPD Base Field").GetComponent<UnityEngine.UI.InputField>();
        hpLevelField = GameObject.Find("HP Level Field").GetComponent<UnityEngine.UI.InputField>();
        atkLevelField = GameObject.Find("ATK Level Field").GetComponent<UnityEngine.UI.InputField>();
        defLevelField = GameObject.Find("DEF Level Field").GetComponent<UnityEngine.UI.InputField>();
        spatkLevelField = GameObject.Find("SPATK Level Field").GetComponent<UnityEngine.UI.InputField>();
        spdefLevelField = GameObject.Find("SPDEF Level Field").GetComponent<UnityEngine.UI.InputField>();
        spdLevelField = GameObject.Find("SPD Level Field").GetComponent<UnityEngine.UI.InputField>();
        hpCSField = GameObject.Find("HP CS Field").GetComponent<UnityEngine.UI.InputField>();
        atkCSField = GameObject.Find("ATK CS Field").GetComponent<UnityEngine.UI.InputField>();
        defCSField = GameObject.Find("DEF CS Field").GetComponent<UnityEngine.UI.InputField>();
        spatkCSField = GameObject.Find("SPATK CS Field").GetComponent<UnityEngine.UI.InputField>();
        spdefCSField = GameObject.Find("SPDEF CS Field").GetComponent<UnityEngine.UI.InputField>();
        spdCSField = GameObject.Find("SPD CS Field").GetComponent<UnityEngine.UI.InputField>();
        hpTotalField = GameObject.Find("HP Total Field").GetComponent<UnityEngine.UI.InputField>();
        atkTotalField = GameObject.Find("ATK Total Field").GetComponent<UnityEngine.UI.InputField>();
        defTotalField = GameObject.Find("DEF Total Field").GetComponent<UnityEngine.UI.InputField>();
        spatkTotalField = GameObject.Find("SPATK Total Field").GetComponent<UnityEngine.UI.InputField>();
        spdefTotalField = GameObject.Find("SPDEF Total Field").GetComponent<UnityEngine.UI.InputField>();
        spdTotalField = GameObject.Find("SPD Total Field").GetComponent<UnityEngine.UI.InputField>();
        currentHealthField = GameObject.Find("Current Health Field").GetComponent<UnityEngine.UI.InputField>();
        maxHealthField = GameObject.Find("Max Health Field").GetComponent<UnityEngine.UI.InputField>();
        levelField = GameObject.Find("Level Field").GetComponent<UnityEngine.UI.InputField>();
        dynaLevelField = GameObject.Find("Dynamax Level Field").GetComponent<UnityEngine.UI.InputField>();
        heldItemNameField = GameObject.Find("Held Item Name Field").GetComponent<UnityEngine.UI.InputField>();
        captureRateField = GameObject.Find("Capture Rate Field").GetComponent<UnityEngine.UI.InputField>();

        conditionToggle = GameObject.Find("Condition Toggle").GetComponent<Toggle>();

        megaButton = GameObject.Find("Mega Button").GetComponent<Button>();
        altMegaButton = GameObject.Find("Alt Mega Button").GetComponent<Button>();
        dynaButton = GameObject.Find("Dynamax Button").GetComponent<Button>();
        megaButton.interactable = false;
        altMegaButton.interactable = false;

        megaImage = GameObject.Find("Mega Image").GetComponent<Image>();
        altMegaImage = GameObject.Find("Alt Mega Image").GetComponent<Image>();

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
            if (!File.Exists(PokedexManager.dataPath + "/PokemonIcons/" + pokemon.image + ".png")) {
                Debug.LogWarning("Image not found for pokemon: " + pokemon.species);
            }
            if (!File.Exists(PokedexManager.dataPath + "/Cries/" + pokemon.cry + ".ogg")) {
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
            if (!File.Exists(PokedexManager.dataPath + "/ItemIcons/" + item.image + ".png")) {
                Debug.LogWarning("Image not found for item: " + item.name);
            }
        }

        // Load temp pokemon if they exist. 
        var myFiles = Directory.EnumerateFiles(PokedexManager.dataPath + "/tmp/", "*.json", SearchOption.TopDirectoryOnly);
        PokedexManager.pokemonToEncounter = new List<Pokemon>();
        foreach (var file in myFiles) {
            Pokemon pokemon = Pokemon.FromJson(file);
            PokedexManager.pokemonToEncounter.Add(pokemon);
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
            foreach (var file in Directory.EnumerateFiles(Path.Combine(PokedexManager.dataPath, "tmp/"))) {
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
        } else if (pokemon.isDynamax) {
            controller.dynaBack.SetActive(true);
            controller.dynaFront.SetActive(true);
            if (!String.IsNullOrWhiteSpace(pokemon.gigaImage)) {
                pokemon.sprite = PokedexManager.LoadSprite("PokemonIcons/" + pokemon.gigaImage);
                if (pokemon.sprite != null) {
                    controller.sprite.sprite = pokemon.sprite;
                }
            } else {
                pokemon.sprite = PokedexManager.LoadSprite("PokemonIcons/" + pokemon.image);
                if (pokemon.sprite != null) {
                    controller.sprite.sprite = pokemon.sprite;
                }
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

        entry.GetComponent<PokedexEntry>().species.text = pokemon.CheckForNickname();
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

        maxHealthField.text = pokemon.isDynamax ? pokemon.GetDynaMaxHealth().ToString() : pokemon.GetMaxHealth().ToString();
        currentHealthField.text = pokemon.currentHealth.ToString();

        levelField.text = pokemon.level.ToString();
        dynaLevelField.text = pokemon.dynamaxLevel.ToString();

        megaButton.interactable = pokemon.HasMega();
        altMegaButton.interactable = pokemon.HasAltMega();

        if (pokemon.HasMega() && pokemon.HasAltMega()) {
            megaImage.sprite = Resources.Load<Sprite>("MegaX");
            altMegaImage.sprite = Resources.Load<Sprite>("MegaY");
        } else if (pokemon.HasMega() && !pokemon.HasAltMega()) {
            megaImage.sprite = Resources.Load<Sprite>("Mega");
            altMegaImage.sprite = Resources.Load<Sprite>("MegaEmpty");
        } else {
            megaImage.sprite = Resources.Load<Sprite>("MegaEmpty");
            altMegaImage.sprite = Resources.Load<Sprite>("MegaEmpty");
        }

        if (pokemon.mega.inMegaForm) {
            megaButton.GetComponent<Image>().color = Color.gray;
        } else {
            megaButton.GetComponent<Image>().color = Color.white;
        }

        if (pokemon.altMega.inMegaForm) {
            altMegaButton.GetComponent<Image>().color = Color.gray;
        } else {
            altMegaButton.GetComponent<Image>().color = Color.white;
        }

        if (pokemon.isDynamax) {
            dynaButton.GetComponent<Image>().color = Color.gray;
        } else {
            dynaButton.GetComponent<Image>().color = Color.white;
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
        string athl = "Athl " + pokemon.athleticsDie + "d6";
        if (pokemon.athleticsBonus != 0) { athl += "+" + pokemon.athleticsBonus; }
        string acro = "Acro " + pokemon.acrobaticsDie + "d6";
        if (pokemon.acrobaticsBonus != 0) { acro += "+" + pokemon.acrobaticsBonus; }
        string combat = "Combat " + pokemon.combatDie + "d6";
        if (pokemon.combatBonus != 0) { combat += "+" + pokemon.combatBonus; }
        string focus = "Focus " + pokemon.focusDie + "d6";
        if (pokemon.focusBonus != 0) { focus += "+" + pokemon.focusBonus; }
        string percep = "Percep " + pokemon.perceptionDie + "d6";
        if (pokemon.perceptionBonus != 0) { percep += "+" + pokemon.perceptionBonus; }
        string stealth = "Stealth " + pokemon.stealthDie + "d6";
        if (pokemon.stealthBonus != 0) { stealth += "+" + pokemon.stealthBonus; }
        string techEdu = "Edu: Tech " + pokemon.techEduDie + "d6";
        if (pokemon.techEduBonus != 0) { techEdu += "+" + pokemon.techEduBonus; }
        skillList.Add(new Dropdown.OptionData(athl));
        skillList.Add(new Dropdown.OptionData(acro));
        skillList.Add(new Dropdown.OptionData(combat));
        skillList.Add(new Dropdown.OptionData(focus));
        skillList.Add(new Dropdown.OptionData(percep));
        skillList.Add(new Dropdown.OptionData(stealth));
        skillList.Add(new Dropdown.OptionData(techEdu));
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
    }

    public void SetStats() {
        Pokemon pokemon = PokedexManager.currentPokemon;
        try {
            pokemon.SetNickname(nameField.text);

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
            pokemon.maxHealth = pokemon.isDynamax ? pokemon.GetDynaMaxHealth() : pokemon.GetMaxHealth();

            pokemon.heldItem.name = heldItemNameField.text;

            pokemon.dynamaxLevel = int.Parse((dynaLevelField.text));

            pokemon.GetCaptureRate();

            OnSelected(PokedexManager.currentPokemon, PokedexManager.currentEntry);
            pokemon.ToJson(pokemon.savePath, true);
        } catch {
            string errorMessage = "Failed to assign all values properly. Please check last input.";
            PokedexManager.manager.CreateWarningDialog(errorMessage);
        }
    }

    public void SetHeldItem() {
        if (String.IsNullOrWhiteSpace(heldItemNameField.text) || heldItemNameField.text == "None") {
            return;
        }
        Pokemon pokemon = PokedexManager.currentPokemon;
        bool itemFound = false;
        foreach (var item in PokedexManager.items) {
            if (heldItemNameField.text == item.name) {
                pokemon.heldItem = item;
                pokemon.heldItem.sprite = PokedexManager.LoadSprite("ItemIcons/" + item.image);
                if (pokemon.heldItem.sprite == null) {
                    pokemon.heldItem.sprite = PokedexManager.LoadSprite("ItemIcons/None");
                }
                itemFound = true;
            }
        }
        if (!itemFound) {
            PokedexManager.manager.CreateWarningDialog("This item is not registered in the Items.json: " + heldItemNameField.text);
            return;
        }
        OnSelected(pokemon, PokedexManager.currentEntry);
        pokemon.ToJson(pokemon.savePath, true);
    }

    public void GetCondition() {
        Pokemon pokemon = PokedexManager.currentPokemon;
        switch (conditionDropdown.options[conditionDropdown.value].text) {
            case "Blinded":
                conditionToggle.isOn = pokemon.blind;
                break;
            case "Totally Blinded":
                conditionToggle.isOn = pokemon.totallyBlind;
                break;
            case "Burned":
                conditionToggle.isOn = pokemon.burned;
                break;
            case "Confused":
                conditionToggle.isOn = pokemon.confused;
                break;
            case "Cursed":
                conditionToggle.isOn = pokemon.cursed;
                break;
            case "Disabled":
                conditionToggle.isOn = pokemon.disabled;
                break;
            case "Enraged":
                conditionToggle.isOn = pokemon.enraged;
                break;
            case "Flinched":
                conditionToggle.isOn = pokemon.flinched;
                break;
            case "Frozen":
                conditionToggle.isOn = pokemon.frozen;
                break;
            case "Infatuated":
                conditionToggle.isOn = pokemon.infatuated;
                break;
            case "Paralyzed":
                conditionToggle.isOn = pokemon.paralyzed;
                break;
            case "Poisoned":
                conditionToggle.isOn = pokemon.poisoned;
                break;
            case "Badly Poisoned":
                conditionToggle.isOn = pokemon.badlyPoisoned;
                break;
            case "Sleeping":
                conditionToggle.isOn = pokemon.asleep;
                break;
            case "Heavily Sleeping":
                conditionToggle.isOn = pokemon.badlyAsleep;
                break;
            case "Slowed":
                conditionToggle.isOn = pokemon.slowed;
                break;
            case "Stuck":
                conditionToggle.isOn = pokemon.stuck;
                break;
            case "Suppressed":
                conditionToggle.isOn = pokemon.suppressed;
                break;
            case "Trapped":
                conditionToggle.isOn = pokemon.trapped;
                break;
            case "Tripped":
                conditionToggle.isOn = pokemon.tripped;
                break;
            case "Vulnerable":
                conditionToggle.isOn = pokemon.vulnerable;
                break;
        }
    }

    public void SetCondition() {
        Pokemon pokemon = PokedexManager.currentPokemon;
        switch (conditionDropdown.options[conditionDropdown.value].text) {
            case "Blinded":
                pokemon.blind = conditionToggle.isOn;
                break;
            case "Totally Blinded":
                pokemon.totallyBlind = conditionToggle.isOn;
                break;
            case "Burned":
                pokemon.burned = conditionToggle.isOn;
                break;
            case "Confused":
                pokemon.confused = conditionToggle.isOn;
                break;
            case "Cursed":
                pokemon.cursed = conditionToggle.isOn;
                break;
            case "Disabled":
                pokemon.disabled = conditionToggle.isOn;
                break;
            case "Enraged":
                pokemon.enraged = conditionToggle.isOn;
                break;
            case "Flinched":
                pokemon.flinched = conditionToggle.isOn;
                break;
            case "Frozen":
                pokemon.frozen = conditionToggle.isOn;
                break;
            case "Infatuated":
                pokemon.infatuated = conditionToggle.isOn;
                break;
            case "Paralyzed":
                pokemon.paralyzed = conditionToggle.isOn;
                break;
            case "Poisoned":
                pokemon.poisoned = conditionToggle.isOn;
                break;
            case "Badly Poisoned":
                pokemon.badlyPoisoned = conditionToggle.isOn;
                break;
            case "Sleeping":
                pokemon.asleep = conditionToggle.isOn;
                break;
            case "Heavily Sleeping":
                pokemon.badlyAsleep = conditionToggle.isOn;
                break;
            case "Slowed":
                pokemon.slowed = conditionToggle.isOn;
                break;
            case "Stuck":
                pokemon.stuck = conditionToggle.isOn;
                break;
            case "Suppressed":
                pokemon.suppressed = conditionToggle.isOn;
                break;
            case "Trapped":
                pokemon.trapped = conditionToggle.isOn;
                break;
            case "Tripped":
                pokemon.tripped = conditionToggle.isOn;
                break;
            case "Vulnerable":
                pokemon.vulnerable = conditionToggle.isOn;
                break;
        }
        pokemon.GetCaptureRate();
        captureRateField.text = pokemon.captureRate.ToString() + " or less";
        pokemon.ToJson(pokemon.savePath, true);
    }

    void AddPokemon(float numberToEncounter) {
        for (float i = 0; i < numberToEncounter; i += 1) {
            int number = 0;
            do {
                number = UnityEngine.Random.Range(1, PokedexManager.manager.GetHighestNumberInPokemonArray(encounterablePokemon.ToArray()) + 1);
            } while (encounterablePokemon.Where(x => x.number == number).Count() < 1);
            Pokemon[] pokemonList = encounterablePokemon.Where(x => x.number == number).ToArray();
            int index = UnityEngine.Random.Range(0, pokemonList.Count());
            PokedexManager.pokemonToEncounter.Add(pokemonList[index].Clone());
            Pokemon pokemon = PokedexManager.pokemonToEncounter[PokedexManager.pokemonToEncounter.Count - 1];

            string natureString = natureDropdown.options[natureDropdown.value].text;
            int levelToApply = (int)UnityEngine.Random.Range(minLevelSlider.value, maxLevelSlider.value);

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

            pokemon.maxHealth = pokemon.GetMaxHealth();
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
        if (PokedexManager.currentPokemon == null) { return; }
        SetStats();
        Pokemon pokemon = PokedexManager.currentPokemon;
        Text megaText = megaButton.gameObject.GetComponentInChildren<Text>();
        if (pokemon.mega.inMegaForm) {
            pokemon.UnapplyMega();
        } else {
            pokemon.ApplyMega();
        }
        OnSelected(pokemon, PokedexManager.currentEntry);
        pokemon.ToJson(pokemon.savePath, true);
    }

    public void ToggleAltMega() {
        if (PokedexManager.currentPokemon == null) { return; }
        SetStats();
        Pokemon pokemon = PokedexManager.currentPokemon;
        if (pokemon.altMega.inMegaForm) {
            pokemon.UnapplyAltMega();
        } else {
            pokemon.ApplyAltMega();
        }
        OnSelected(pokemon, PokedexManager.currentEntry);
        pokemon.ToJson(pokemon.savePath, true);
    }

    public void ToggleDynamax() {
        if (PokedexManager.currentPokemon == null) { return; }
        SetStats();
        Pokemon pokemon = PokedexManager.currentPokemon;
        if (pokemon.isDynamax) {
            pokemon.UnapplyDynamax();
        } else {
            pokemon.ApplyDynamax();
        }
        OnSelected(pokemon, PokedexManager.currentEntry);
        pokemon.ToJson(pokemon.savePath, true);
    }
}