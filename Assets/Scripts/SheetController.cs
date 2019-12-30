using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheetController : MonoBehaviour {

    // Known bugs:
    // NetworkServer need to not be started if there is no internet. Need a check for this. 

    public GameObject pokedexPrefab;
    public GameObject contentPanel;
    public AudioSource cryAudioSource;
    public Image heldItemImage;

    public int capturedJSONCount = 0;

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
        tradeNameField,
        myNameField;

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
        basicAbilityField = GameObject.Find("Basic Ability Field").GetComponent<InputField>();
        advanceAbilityField = GameObject.Find("Advanced Ability Field").GetComponent<InputField>();
        highAbilityField = GameObject.Find("High Ability Field").GetComponent<InputField>();
        levelField = GameObject.Find("Level Field").GetComponent<InputField>();
        movesListField = GameObject.Find("Moves List Field").GetComponent<InputField>();
        skillsListField = GameObject.Find("Skills List Field").GetComponent<InputField>();
        capabilitiesListField = GameObject.Find("Capabilities List Field").GetComponent<InputField>();
        heldItemDescriptionField = GameObject.Find("Held Item Description Field").GetComponent<InputField>();
        heldItemNameField = GameObject.Find("Held Item Name Field").GetComponent<InputField>();

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
            EncounterController.GetMoves(pokemon);
            CreateListItem(pokemon);
        }

        if (!Server.server.serverStarted) {
            Server.server.startServer = true;
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
                EncounterController.GetMoves(pokemon);
                CreateListItem(pokemon);
            }
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
            string errorMessage = "Pokemon Sprite could not be loaded from: Icons/" + pokemon.image;
            PokedexManager.manager.CreateWarningDialog(errorMessage);
        }
    }

    public void OnSelected(Pokemon pokemon, GameObject entry) {
        PokedexManager.AssignCurrentPokemonAndEntry(entry);
        myNameField.text = PokedexManager.GetLocalIPAddress();

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

    public void TradePokemon() {
        Client.client.ip = tradeNameField.text;
        Pokemon pokemon = PokedexManager.currentPokemon;
        string name = pokemon.name == null || pokemon.name == "" ? pokemon.species == null || pokemon.species == "" ? "???" : pokemon.species : pokemon.name;
        string message = "Are you sure you wish to send " + name + " to " + tradeNameField.text + "?";
        PokedexManager.manager.CreateConfirmationDialog(message, ConfirmationType.trade);
    }
}
