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
        movesListField0,
        movesListField1,
        movesListField2,
        movesListField3,
        movesListField4,
        movesListField5,
        skillsListField0,
        skillsListField1,
        skillsListField2,
        skillsListField3,
        skillsListField4,
        skillsListField5,
        skillsListField6,
        capabilitiesListField0,
        capabilitiesListField1,
        capabilitiesListField2,
        capabilitiesListField3,
        capabilitiesListField4,
        capabilitiesListField5,
        capabilitiesListField6,
        capabilitiesListField7,
        capabilitiesListField8,
        capabilitiesListField9,
        capabilitiesListField10,
        capabilitiesListField11,
        capabilitiesListField12,
        capabilitiesListField13,
        capabilitiesListField14,
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
        movesListField0 = GameObject.Find("Moves List Field 0").GetComponent<InputField>();
        movesListField1 = GameObject.Find("Moves List Field 1").GetComponent<InputField>();
        movesListField2 = GameObject.Find("Moves List Field 2").GetComponent<InputField>();
        movesListField3 = GameObject.Find("Moves List Field 3").GetComponent<InputField>();
        movesListField4 = GameObject.Find("Moves List Field 4").GetComponent<InputField>();
        movesListField5 = GameObject.Find("Moves List Field 5").GetComponent<InputField>();
        skillsListField0 = GameObject.Find("Skills List Field 0").GetComponent<InputField>();
        skillsListField1 = GameObject.Find("Skills List Field 1").GetComponent<InputField>();
        skillsListField2 = GameObject.Find("Skills List Field 2").GetComponent<InputField>();
        skillsListField3 = GameObject.Find("Skills List Field 3").GetComponent<InputField>();
        skillsListField4 = GameObject.Find("Skills List Field 4").GetComponent<InputField>();
        skillsListField5 = GameObject.Find("Skills List Field 5").GetComponent<InputField>();
        skillsListField6 = GameObject.Find("Skills List Field 6").GetComponent<InputField>();
        capabilitiesListField0 = GameObject.Find("Capabilities List Field 0").GetComponent<InputField>();
        capabilitiesListField1 = GameObject.Find("Capabilities List Field 1").GetComponent<InputField>();
        capabilitiesListField2 = GameObject.Find("Capabilities List Field 2").GetComponent<InputField>();
        capabilitiesListField3 = GameObject.Find("Capabilities List Field 3").GetComponent<InputField>();
        capabilitiesListField4 = GameObject.Find("Capabilities List Field 4").GetComponent<InputField>();
        capabilitiesListField5 = GameObject.Find("Capabilities List Field 5").GetComponent<InputField>();
        capabilitiesListField6 = GameObject.Find("Capabilities List Field 6").GetComponent<InputField>();
        capabilitiesListField7 = GameObject.Find("Capabilities List Field 7").GetComponent<InputField>();
        capabilitiesListField8 = GameObject.Find("Capabilities List Field 8").GetComponent<InputField>();
        capabilitiesListField9 = GameObject.Find("Capabilities List Field 9").GetComponent<InputField>();
        capabilitiesListField10 = GameObject.Find("Capabilities List Field 10").GetComponent<InputField>();
        capabilitiesListField11 = GameObject.Find("Capabilities List Field 11").GetComponent<InputField>();
        capabilitiesListField12 = GameObject.Find("Capabilities List Field 12").GetComponent<InputField>();
        capabilitiesListField13 = GameObject.Find("Capabilities List Field 13").GetComponent<InputField>();
        capabilitiesListField14 = GameObject.Find("Capabilities List Field 14").GetComponent<InputField>();
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

        movesListField0.text = pokemon.knownMoveList.Length > 0 ? pokemon.knownMoveList[0].name : "";
        movesListField1.text = pokemon.knownMoveList.Length > 1 ? pokemon.knownMoveList[1].name : "";
        movesListField2.text = pokemon.knownMoveList.Length > 2 ? pokemon.knownMoveList[2].name : "";
        movesListField3.text = pokemon.knownMoveList.Length > 3 ? pokemon.knownMoveList[3].name : "";
        movesListField4.text = pokemon.knownMoveList.Length > 4 ? pokemon.knownMoveList[4].name : "";
        movesListField5.text = pokemon.knownMoveList.Length > 5 ? pokemon.knownMoveList[5].name : "";

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

        skillsListField0.text = "Athl " + pokemon.athleticsDie.ToString() + "d6+" + pokemon.athleticsBonus.ToString();
        skillsListField1.text = "Acro " + pokemon.acrobaticsDie.ToString() + "d6+" + pokemon.acrobaticsBonus.ToString();
        skillsListField2.text = "Combat " + pokemon.combatDie.ToString() + "d6+" + pokemon.combatBonus.ToString();
        skillsListField3.text = "Focus " + pokemon.focusDie.ToString() + "d6+" + pokemon.focusBonus.ToString();
        skillsListField4.text = "Percep " + pokemon.perceptionDie.ToString() + "d6+" + pokemon.perceptionBonus.ToString();
        skillsListField5.text = "Stealth " + pokemon.stealthDie.ToString() + "d6+" + pokemon.stealthBonus.ToString();
        skillsListField6.text = "Edu:Tech " + pokemon.techEduDie.ToString() + "d6+" + pokemon.techEduBonus.ToString();

        capabilitiesListField0.text = pokemon.capabilities.Length > 0 ? pokemon.capabilities[0] : "";
        capabilitiesListField1.text = pokemon.capabilities.Length > 1 ? pokemon.capabilities[1] : "";
        capabilitiesListField2.text = pokemon.capabilities.Length > 2 ? pokemon.capabilities[2] : "";
        capabilitiesListField3.text = pokemon.capabilities.Length > 3 ? pokemon.capabilities[3] : "";
        capabilitiesListField4.text = pokemon.capabilities.Length > 4 ? pokemon.capabilities[4] : "";
        capabilitiesListField5.text = pokemon.capabilities.Length > 5 ? pokemon.capabilities[5] : "";
        capabilitiesListField6.text = pokemon.capabilities.Length > 6 ? pokemon.capabilities[6] : "";
        capabilitiesListField7.text = pokemon.capabilities.Length > 7 ? pokemon.capabilities[7] : "";
        capabilitiesListField8.text = pokemon.capabilities.Length > 8 ? pokemon.capabilities[8] : "";
        capabilitiesListField9.text = pokemon.capabilities.Length > 9 ? pokemon.capabilities[9] : "";
        capabilitiesListField10.text = pokemon.capabilities.Length > 10 ? pokemon.capabilities[10] : "";
        capabilitiesListField11.text = pokemon.capabilities.Length > 11 ? pokemon.capabilities[11] : "";
        capabilitiesListField12.text = pokemon.capabilities.Length > 12 ? pokemon.capabilities[12] : "";
        capabilitiesListField13.text = pokemon.capabilities.Length > 13 ? pokemon.capabilities[13] : "";
        capabilitiesListField14.text = pokemon.capabilities.Length > 14 ? pokemon.capabilities[14] : "";
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
        movesListField0.text = "";
        movesListField1.text = "";
        movesListField2.text = "";
        movesListField3.text = "";
        movesListField4.text = "";
        movesListField5.text = "";
        skillsListField0.text = "";
        skillsListField1.text = "";
        skillsListField2.text = "";
        skillsListField3.text = "";
        skillsListField4.text = "";
        skillsListField5.text = "";
        skillsListField6.text = "";
        capabilitiesListField0.text = "";
        capabilitiesListField1.text = "";
        capabilitiesListField2.text = "";
        capabilitiesListField3.text = "";
        capabilitiesListField4.text = "";
        capabilitiesListField5.text = "";
        capabilitiesListField6.text = "";
        capabilitiesListField7.text = "";
        capabilitiesListField8.text = "";
        capabilitiesListField9.text = "";
        capabilitiesListField10.text = "";
        capabilitiesListField11.text = "";
        capabilitiesListField12.text = "";
        capabilitiesListField13.text = "";
        capabilitiesListField14.text = "";
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

    public void PlayPokemonCry() {
        if (PokedexManager.currentPokemon == null) {
            Debug.Log("No pokemon currently selected");
            return;
        }
        if (PokedexManager.currentPokemon.cryAudio != null) {
            cryAudioSource.clip = PokedexManager.currentPokemon.cryAudio;
            cryAudioSource.Play();
        } else {
            GetCries(PokedexManager.currentPokemon);
            if (PokedexManager.currentPokemon.cryAudio != null) {
                cryAudioSource.clip = PokedexManager.currentPokemon.cryAudio;
                cryAudioSource.Play();
            } else {
                Debug.LogError("The current pokemon does not have a proper cry set up: " + PokedexManager.currentPokemon.species);
            }
        }
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
