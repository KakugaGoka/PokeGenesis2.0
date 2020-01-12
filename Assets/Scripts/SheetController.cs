using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SheetController : MonoBehaviour {

    // Known bugs:
    // NetworkServer need to not be started if there is no internet. Need a check for this. 

    public GameObject pokedexPrefab;
    public GameObject contentPanel;
    public AudioSource cryAudioSource;
    public Image heldItemImage;

    public int capturedJSONCount = 0;

    private Dropdown
        abilityDropdown,
        capabilityDropdown,
        skillDropdown,
        moveDropdown,
        conditionDropdown,
        edgeDropdown,
        featureDropdown;

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
        tradeNameField,
        myNameField,
        loyaltyField,
        tutorPointsField;

    private void Start() {
        myNameField = GameObject.Find("My Name Field").GetComponent<UnityEngine.UI.InputField>();
        tradeNameField = GameObject.Find("Trade Name Field").GetComponent<UnityEngine.UI.InputField>();
        nameField = GameObject.Find("Name Field").GetComponent<UnityEngine.UI.InputField>();
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
        loyaltyField = GameObject.Find("Loyalty Field").GetComponent<UnityEngine.UI.InputField>();
        tutorPointsField = GameObject.Find("Tutor Points Field").GetComponent<UnityEngine.UI.InputField>();

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
        edgeDropdown = GameObject.Find("Edges Dropdown").GetComponent<Dropdown>();
        featureDropdown = GameObject.Find("Features Dropdown").GetComponent<Dropdown>();

        heldItemImage.sprite = PokedexManager.LoadSprite("ItemIcons/None");
#if DEBUG
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
        }

        // Verify all item images
        foreach (Item item in PokedexManager.items) {
            if (!File.Exists(PokedexManager.dataPath + "/ItemIcons/" + item.image + ".png")) {
                Debug.LogWarning("Image not found for item: " + item.name);
            }
        }
#endif

        // Load temp pokemon if they exist. 
        var myFiles = Directory.GetFiles(PokedexManager.dataPath + "/Captured/", "*.json", SearchOption.TopDirectoryOnly);
        capturedJSONCount = myFiles.Length;

        foreach (var file in myFiles) {
            Pokemon pokemon = Pokemon.FromJson(file);
            PokedexManager.pokemonToEncounter.Add(pokemon);
            pokemon.GetCries();
            CreateListItem(pokemon);
        }

        if (!Server.server.serverStarted) {
            Server.server.startServer = true;
        }
        myNameField.text = PokedexManager.GetLocalIPAddress();
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

        var myFiles = Directory.GetFiles(PokedexManager.dataPath + "/Captured/", "*.json", SearchOption.TopDirectoryOnly);
        if (capturedJSONCount != myFiles.Length) {
            capturedJSONCount = myFiles.Length;
            PokedexManager.pokemonToEncounter = new List<Pokemon>(); 
            for (int i = 0; i < contentPanel.transform.childCount; i++) {
                Destroy(contentPanel.transform.GetChild(i).gameObject);
            }
            foreach (var file in myFiles) {
                Pokemon pokemon = Pokemon.FromJson(file);
                PokedexManager.pokemonToEncounter.Add(pokemon);
                pokemon.GetCries();
                CreateListItem(pokemon);
            }
        }
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
        myNameField.text = PokedexManager.GetLocalIPAddress();

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

        maxHealthField.text = pokemon.isDynamax ? pokemon.GetDynaMaxHealth().ToString() : pokemon.GetMaxHealth().ToString();
        currentHealthField.text = pokemon.currentHealth.ToString();

        int hpStage = pokemon.hpLevel / 10;
        int atkStage = pokemon.atkLevel / 10;
        int defStage = pokemon.defLevel / 10;
        int spatkStage = pokemon.spatkLevel / 10;
        int spdefStage = pokemon.spdefLevel / 10;
        int spdStage = pokemon.spdLevel / 10;

        hpTotalField.text = (pokemon.hp + pokemon.hpLevel + (pokemon.hpCS * hpStage)).ToString();
        atkTotalField.text = (pokemon.atk + pokemon.atkLevel + (pokemon.atkCS * atkStage)).ToString();
        defTotalField.text = (pokemon.def + pokemon.defLevel + (pokemon.defCS * defStage)).ToString();
        spatkTotalField.text = (pokemon.spatk + pokemon.spatkLevel + (pokemon.spatkCS * spatkStage)).ToString();
        spdefTotalField.text = (pokemon.spdef + pokemon.spdefLevel + (pokemon.spdefCS * spdefStage)).ToString();
        spdTotalField.text = (pokemon.spd + pokemon.spdLevel + (pokemon.spdCS * spdStage)).ToString();

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

        List<Dropdown.OptionData> edgeList = new List<Dropdown.OptionData>();
        foreach (var item in pokemon.edges) {
            edgeList.Add(new Dropdown.OptionData(item));
        }
        edgeDropdown.ClearOptions();
        edgeDropdown.AddOptions(edgeList);

        List<Dropdown.OptionData> featureList = new List<Dropdown.OptionData>();
        foreach (var item in pokemon.features) {
            featureList.Add(new Dropdown.OptionData(item));
        }
        featureDropdown.ClearOptions();
        featureDropdown.AddOptions(featureList);

        heldItemImage.sprite = pokemon.heldItem.sprite;
        heldItemNameField.text = pokemon.heldItem == null ? "None" : pokemon.heldItem.name;
        loyaltyField.text = pokemon.loyalty.ToString();
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
        loyaltyField.text = "";
        tutorPointsField.text = "";
        heldItemImage.sprite = PokedexManager.LoadSprite("ItemIcons/None");
        cryAudioSource.clip = null;
        moveDropdown.ClearOptions();
        skillDropdown.ClearOptions();
        capabilityDropdown.ClearOptions();
        abilityDropdown.ClearOptions();
        conditionDropdown.ClearOptions();
        edgeDropdown.ClearOptions();
        featureDropdown.ClearOptions();
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
            pokemon.loyalty = int.Parse(loyaltyField.text);
            pokemon.tutorPoints = int.Parse(tutorPointsField.text);

            pokemon.level = int.Parse((levelField.text));
            pokemon.dynamaxLevel = int.Parse((dynaLevelField.text));

            OnSelected(PokedexManager.currentPokemon, PokedexManager.currentEntry);
            pokemon.ToJson(pokemon.savePath, true);
        } catch {
            string errorMessage = "Failed to assign all values properly. Please check last input.";
            PokedexManager.manager.CreateWarningDialog(errorMessage);
        }
    }

    public void SetNature() {
        Pokemon pokemon = PokedexManager.currentPokemon;
        bool natureFound = false;
        foreach (var nature in PokedexManager.natures) {
            if (natureField.text == nature.name) {
                pokemon.ModifyBaseStatForNature(true);
                pokemon.GetNature(nature.name);
                natureFound = true;
            }
        }
        if (!natureFound) {
            PokedexManager.manager.CreateWarningDialog("This nature is not registered in the Natures.json: " + natureField.text);
        }
        OnSelected(pokemon, PokedexManager.currentEntry);
        pokemon.ToJson(pokemon.savePath, true);
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
        OnSelected(PokedexManager.currentPokemon, PokedexManager.currentEntry);
        pokemon.ToJson(pokemon.savePath, true);
    }

    public void ReleasePokemon() {
        Pokemon pokemon = PokedexManager.currentPokemon;
        string name = pokemon.CheckForNickname();
        string message = "Are you sure you wish to release " + name + "? They will appear in the encounter list until removed or this application is closed.";
        PokedexManager.manager.CreateConfirmationDialog(message, ConfirmationType.release);
    }

    public void TradePokemon() {
        if (Client.client.ValidateIPv4(tradeNameField.text)) {
            Client.client.ip = tradeNameField.text;
            Pokemon pokemon = PokedexManager.currentPokemon;
            string name = pokemon.CheckForNickname();
            string message = "Are you sure you wish to send " + name + " to " + tradeNameField.text + "?";
            PokedexManager.manager.CreateConfirmationDialog(message, ConfirmationType.trade);
        } else {
            PokedexManager.manager.CreateWarningDialog("'" + tradeNameField.text + "' is not a valid IP address.");
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
            PokedexManager.currentPokemon.GetCries();
            if (PokedexManager.currentPokemon.cryAudio != null) {
                cryAudioSource.clip = PokedexManager.currentPokemon.cryAudio;
                cryAudioSource.Play();
            } else {
                Debug.LogError("The current pokemon does not have a proper cry set up: " + PokedexManager.currentPokemon.species);
            }
        }
    }

    public void ToggleMega() {
        SetStats();
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
        SetStats();
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

    public void ToggleDynamax() {
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

    public void EditCapabilites() {
        PokedexManager.manager.CreateEditDialog(SaveType.capabilities);
    }

    public void EditAbilites() {
        PokedexManager.manager.CreateEditDialog(SaveType.abilities);
    }

    public void EditSkills() {
        PokedexManager.manager.CreateEditDialog(SaveType.skills);
    }

    public void EditMoves() {
        PokedexManager.manager.CreateEditDialog(SaveType.moves);
    }

    public void EditFeatures() {
        PokedexManager.manager.CreateEditDialog(SaveType.features);
    }

    public void EditEdges() {
        PokedexManager.manager.CreateEditDialog(SaveType.edges);
    }
}
