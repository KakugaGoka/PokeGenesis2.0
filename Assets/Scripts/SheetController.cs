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
        conditionDropdown;

    private Toggle
        conditionToggle;

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
        tradeNameField,
        myNameField,
        loyaltyField;

    private void Start() {
        myNameField = GameObject.Find("My Name Field").GetComponent<InputField>();
        tradeNameField = GameObject.Find("Trade Name Field").GetComponent<InputField>();
        nameField = GameObject.Find("Name Field").GetComponent<InputField>();
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
        loyaltyField = GameObject.Find("Loyalty Field").GetComponent<InputField>();

        conditionToggle = GameObject.Find("Condition Toggle").GetComponent<Toggle>();

        moveDropdown = GameObject.Find("Moves Dropdown").GetComponent<Dropdown>();
        capabilityDropdown = GameObject.Find("Capabilities Dropdown").GetComponent<Dropdown>();
        abilityDropdown = GameObject.Find("Abilities Dropdown").GetComponent<Dropdown>();
        skillDropdown = GameObject.Find("Skills Dropdown").GetComponent<Dropdown>();
        conditionDropdown = GameObject.Find("Conditions Dropdown").GetComponent<Dropdown>();

        heldItemImage.sprite = PokedexManager.LoadSprite("ItemIcons/None");
#if DEBUG
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
        }

        // Verify all item images
        foreach (Item item in PokedexManager.items) {
            if (!File.Exists(Application.streamingAssetsPath + "/ItemIcons/" + item.image + ".png")) {
                Debug.LogWarning("Image not found for item: " + item.name);
            }
        }
#endif

        // Load temp pokemon if they exist. 
        var myFiles = Directory.GetFiles(Application.streamingAssetsPath + "/Captured/", "*.json", SearchOption.TopDirectoryOnly);
        capturedJSONCount = myFiles.Length;

        foreach (var file in myFiles) {
            Pokemon pokemon = Pokemon.FromJson(file);
            PokedexManager.pokemonToEncounter.Add(pokemon);
            pokemon.GetMoves();
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

        var myFiles = Directory.GetFiles(Application.streamingAssetsPath + "/Captured/", "*.json", SearchOption.TopDirectoryOnly);
        if (capturedJSONCount != myFiles.Length) {
            capturedJSONCount = myFiles.Length;
            PokedexManager.pokemonToEncounter = new List<Pokemon>(); 
            for (int i = 0; i < contentPanel.transform.childCount; i++) {
                Destroy(contentPanel.transform.GetChild(i).gameObject);
            }
            foreach (var file in myFiles) {
                Pokemon pokemon = Pokemon.FromJson(file);
                PokedexManager.pokemonToEncounter.Add(pokemon);
                pokemon.GetMoves();
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
        pokemon.sprite = PokedexManager.LoadSprite("PokemonIcons/" + pokemon.image);

        if (pokemon.sprite != null) {
            controller.sprite.sprite = pokemon.sprite;
            if (pokemon.shiny) {
                controller.shiny.SetActive(true);
            }
        } else {
            string errorMessage = "Pokemon Sprite could not be loaded from: Icons/" + pokemon.image;
            PokedexManager.manager.CreateWarningDialog(errorMessage);
        }
    }

    public void OnSelected(Pokemon pokemon, GameObject entry) {
        PokedexManager.AssignCurrentPokemonAndEntry(entry);
        myNameField.text = PokedexManager.GetLocalIPAddress();

        nameField.text = pokemon.CheckForNickname();
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

        hpTotalField.text = (pokemon.hpLevel + (pokemon.hpCS * hpStage)).ToString();
        atkTotalField.text = (pokemon.atkLevel + (pokemon.atkCS * atkStage)).ToString();
        defTotalField.text = (pokemon.defLevel + (pokemon.defCS * defStage)).ToString();
        spatkTotalField.text = (pokemon.spatkLevel + (pokemon.spatkCS * spatkStage)).ToString();
        spdefTotalField.text = (pokemon.spdefLevel + (pokemon.spdefCS * spdefStage)).ToString();
        spdTotalField.text = (pokemon.spdLevel + (pokemon.spdCS * spdStage)).ToString();

        levelField.text = pokemon.level.ToString();

        List<Dropdown.OptionData> abilitiesList = new List<Dropdown.OptionData>();
        if (pokemon.basicAbility != null) { abilitiesList.Add(new Dropdown.OptionData(pokemon.basicAbility)); }
        if (pokemon.advancedAbility != null) { abilitiesList.Add(new Dropdown.OptionData(pokemon.advancedAbility)); }
        if (pokemon.highAbility != null) { abilitiesList.Add(new Dropdown.OptionData(pokemon.highAbility)); }
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
            pokemon.loyalty = int.Parse(loyaltyField.text);

            OnSelected(PokedexManager.currentPokemon, PokedexManager.currentEntry);
            pokemon.ToJson(pokemon.savePath);
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
        OnSelected(PokedexManager.currentPokemon, PokedexManager.currentEntry);
        pokemon.ToJson(pokemon.savePath);
    }

    public void ReleasePokemon() {
        Pokemon pokemon = PokedexManager.currentPokemon;
        string name = pokemon.CheckForNickname();
        string message = "Are you sure you wish to release " + name + "? They will appear in the encounter list until removed or this application is closed.";
        PokedexManager.manager.CreateConfirmationDialog(message, ConfirmationType.release);
    }

    public void TradePokemon() {
        Client.client.ip = tradeNameField.text;
        Pokemon pokemon = PokedexManager.currentPokemon;
        string name = pokemon.CheckForNickname();
        string message = "Are you sure you wish to send " + name + " to " + tradeNameField.text + "?";
        PokedexManager.manager.CreateConfirmationDialog(message, ConfirmationType.trade);
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
}
