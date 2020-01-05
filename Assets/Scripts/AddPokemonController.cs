using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Newtonsoft.Json.Linq;

public class AddPokemonController : MonoBehaviour
{
    private List<Pokemon> pokemonToDex = new List<Pokemon>();
    private InputField[] inputFields;
    private int currentField = -1;

    private Toggle
        legendary,
        megaToggle1,
        megaToggle2;

    private InputField
        number,
        species,
        image,
        cry,
        type,
        size,
        weight,
        gender,
        egg,
        hatch,
        diet,
        habitat,
        entry,
        hp,
        atk,
        def,
        spatk,
        spdef,
        spd,
        stage,
        moves,
        capabilities,
        evolutions,
        skills,
        abilities,
        megaName1,
        megaType1,
        megaAbility1,
        megaHP1,
        megaATK1,
        megaDEF1,
        megaSPATK1,
        megaSPDEF1,
        megaSPD1,
        megaName2,
        megaType2,
        megaAbility2,
        megaHP2,
        megaATK2,
        megaDEF2,
        megaSPATK2,
        megaSPDEF2,
        megaSPD2;

    private void Start() {
        number = GameObject.Find("Number Field").GetComponent<InputField>();
        image = GameObject.Find("Image Field").GetComponent<InputField>();
        cry = GameObject.Find("Audio Field").GetComponent<InputField>();
        species = GameObject.Find("Name Field").GetComponent<InputField>();
        type = GameObject.Find("Types Field").GetComponent<InputField>();
        size = GameObject.Find("Size Field").GetComponent<InputField>();
        weight = GameObject.Find("Weight Field").GetComponent<InputField>();
        gender = GameObject.Find("Gender Field").GetComponent<InputField>();
        egg = GameObject.Find("Egg Field").GetComponent<InputField>();
        hatch = GameObject.Find("Hatch Field").GetComponent<InputField>();
        diet = GameObject.Find("Diet Field").GetComponent<InputField>();
        habitat = GameObject.Find("Habitat Field").GetComponent<InputField>();
        entry = GameObject.Find("Entry Field").GetComponent<InputField>();
        hp = GameObject.Find("HP Field").GetComponent<InputField>();
        atk = GameObject.Find("ATK Field").GetComponent<InputField>();
        def = GameObject.Find("DEF Field").GetComponent<InputField>();
        spatk = GameObject.Find("SPATK Field").GetComponent<InputField>();
        spdef = GameObject.Find("SPDEF Field").GetComponent<InputField>();
        spd = GameObject.Find("SPD Field").GetComponent<InputField>();
        stage = GameObject.Find("Stage Field").GetComponent<InputField>();
        capabilities = GameObject.Find("Capabilities Field").GetComponent<InputField>();
        moves = GameObject.Find("Moves Field").GetComponent<InputField>();
        evolutions = GameObject.Find("Evolution Field").GetComponent<InputField>();
        skills = GameObject.Find("Skills Field").GetComponent<InputField>();
        abilities = GameObject.Find("Ability Field").GetComponent<InputField>();

        megaName1 = GameObject.Find("Mega Name Field 1").GetComponent<InputField>();
        megaType1 = GameObject.Find("Mega Type Field 1").GetComponent<InputField>();
        megaAbility1 = GameObject.Find("Mega Ability Field 1").GetComponent<InputField>();
        megaHP1 = GameObject.Find("HP Field Mega 1").GetComponent<InputField>();
        megaATK1 = GameObject.Find("ATK Field Mega 1").GetComponent<InputField>();
        megaDEF1 = GameObject.Find("DEF Field Mega 1").GetComponent<InputField>();
        megaSPATK1 = GameObject.Find("SPATK Field Mega 1").GetComponent<InputField>();
        megaSPDEF1 = GameObject.Find("SPDEF Field Mega 1").GetComponent<InputField>();
        megaSPD1 = GameObject.Find("SPD Field Mega 1").GetComponent<InputField>();

        megaName2 = GameObject.Find("Mega Name Field 2").GetComponent<InputField>();
        megaType2 = GameObject.Find("Mega Type Field 2").GetComponent<InputField>();
        megaAbility2 = GameObject.Find("Mega Ability Field 2").GetComponent<InputField>();
        megaHP2 = GameObject.Find("HP Field Mega 2").GetComponent<InputField>();
        megaATK2 = GameObject.Find("ATK Field Mega 2").GetComponent<InputField>();
        megaDEF2 = GameObject.Find("DEF Field Mega 2").GetComponent<InputField>();
        megaSPATK2 = GameObject.Find("SPATK Field Mega 2").GetComponent<InputField>();
        megaSPDEF2 = GameObject.Find("SPDEF Field Mega 2").GetComponent<InputField>();
        megaSPD2 = GameObject.Find("SPD Field Mega 2").GetComponent<InputField>();

        legendary = GameObject.Find("Legendary Toggle").GetComponent<Toggle>();
        megaToggle1 = GameObject.Find("Mega Toggle 1").GetComponent<Toggle>();
        megaToggle2 = GameObject.Find("Mega Toggle 2").GetComponent<Toggle>();

        inputFields = new InputField[] {
                number,
                image,
                cry,
                species,
                type,
                size,
                weight,
                gender,
                egg,
                hatch,
                diet,
                habitat,
                entry,
                hp,
                atk,
                def,
                spatk,
                spdef,
                spd,
                stage,
                moves,
                capabilities,
                evolutions,
                skills,
                abilities,
            };
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            if (currentField > 0 && currentField < inputFields.Length) {
                inputFields[currentField].text = inputFields[currentField].text.Trim();
            }
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                currentField--;
                if (currentField < 0) {
                    currentField = inputFields.Length - 1;
                }
            } else {
                currentField++;
                if (currentField >= inputFields.Length) {
                    currentField = 0;
                }
            }
            EventSystem.current.SetSelectedGameObject(inputFields[currentField].gameObject, null);
            inputFields[currentField].OnPointerClick(new PointerEventData(EventSystem.current));
        } else {
            foreach (InputField field in inputFields) {
                if (field.isFocused) {
                    currentField = Array.IndexOf(inputFields, field);
                }
            }
        }
    }

    public void ClearFields() {
        Debug.Log("Clearing Fields");
        number.text = "";
        species.text = "";
        image.text = "";
        cry.text = "";
        type.text = "";
        size.text = "";
        weight.text = "";
        gender.text = "";
        egg.text = "";
        hatch.text = "";
        diet.text = "";
        habitat.text = "";
        entry.text = "";
        hp.text = "";
        atk.text = "";
        def.text = "";
        spatk.text = "";
        spdef.text = "";
        spd.text = "";
        stage.text = "";
        moves.text = "";
        capabilities.text = "";
        evolutions.text = "";
        skills.text = "";
        abilities.text = "";
        megaName1.text = "";
        megaType1.text = "";
        megaAbility1.text = "";
        megaHP1.text = "";
        megaATK1.text = "";
        megaDEF1.text = "";
        megaSPATK1.text = "";
        megaSPDEF1.text = "";
        megaSPD1.text = "";
        megaName2.text = "";
        megaType2.text = "";
        megaAbility2.text = "";
        megaHP2.text = "";
        megaATK2.text = "";
        megaDEF2.text = "";
        megaSPATK2.text = "";
        megaSPDEF2.text = "";
        megaSPD2.text = "";
        legendary.isOn = false;
    }

    public void SaveToPokedex() {
        Debug.Log("Saving to Pokedex");
        Pokemon pokemon = new Pokemon();

        try {
            pokemon.species = VerifyString(species.text, "Pokemon - Species");
            pokemon.image = VerifyString(image.text, "Pokemon - Image Name");
            pokemon.cry = VerifyString(cry.text, "Pokemon - Audio Name");
            pokemon.type = VerifyString(type.text, "Pokemon - Types");
            pokemon.size = VerifyString(size.text, "Pokemon - Height");
            pokemon.weight = VerifyString(weight.text, "Pokemon - Weight");
            pokemon.gender = VerifyString(gender.text, "Pokemon - Gender");
            pokemon.egg = VerifyString(egg.text, "Pokemon - Egg");
            pokemon.hatch = VerifyString(hatch.text, "Pokemon - Hatch");
            pokemon.diet = VerifyString(diet.text, "Pokemon - Diet");
            pokemon.habitat = VerifyString(habitat.text, "Pokemon - Habitat");
            pokemon.entry = VerifyString(entry.text, "Pokemon - Entry");

            pokemon.legendary = legendary.isOn;
            
            pokemon.number = VerifyInteger(number.text, "Pokemon - Number");
            pokemon.hp = VerifyInteger(hp.text, "Pokemon - HP");
            pokemon.atk = VerifyInteger(atk.text, "Pokemon - ATK");
            pokemon.def = VerifyInteger(def.text, "Pokemon - DEF");
            pokemon.spatk = VerifyInteger(spatk.text, "Pokemon - SPATK");
            pokemon.spdef = VerifyInteger(spdef.text, "Pokemon - SPDEF");
            pokemon.spd = VerifyInteger(spd.text, "Pokemon - SPD");
            pokemon.stage = VerifyInteger(stage.text, "Pokemon - Stage");

            if (megaToggle1.isOn) {
                pokemon.mega.name = VerifyString(megaName1.text, "Mega 1 - Name");
                pokemon.mega.type = VerifyString(megaType1.text, "Mega 1 - Type");
                pokemon.mega.ability = VerifyString(megaAbility1.text, "Mega 1 - Ability");
                pokemon.mega.hp = VerifyInteger(megaHP1.text, "Mega 1 - HP");
                pokemon.mega.atk = VerifyInteger(megaATK1.text, "Mega 1 - ATK");
                pokemon.mega.def = VerifyInteger(megaDEF1.text, "Mega 1 - DEF");
                pokemon.mega.spatk = VerifyInteger(megaSPATK1.text, "Mega 1 - SPATK");
                pokemon.mega.spdef = VerifyInteger(megaSPDEF1.text, "Mega 1 - SPDEF");
                pokemon.mega.spd = VerifyInteger(megaSPD1.text, "Mega 1 - SPD");
            }
            if (megaToggle2.isOn) {
                pokemon.altMega.name = VerifyString(megaName2.text, "Mega 2 - Name");
                pokemon.altMega.type = VerifyString(megaType2.text, "Mega 2 - Type");
                pokemon.altMega.ability = VerifyString(megaAbility2.text, "Mega 2 - Ability");
                pokemon.altMega.hp = VerifyInteger(megaHP2.text, "Mega 2 - HP");
                pokemon.altMega.atk = VerifyInteger(megaATK2.text, "Mega 2 - ATK");
                pokemon.altMega.def = VerifyInteger(megaDEF2.text, "Mega 2 - DEF");
                pokemon.altMega.spatk = VerifyInteger(megaSPATK2.text, "Mega 2 - SPATK");
                pokemon.altMega.spdef = VerifyInteger(megaSPDEF2.text, "Mega 2 - SPDEF");
                pokemon.altMega.spd = VerifyInteger(megaSPD2.text, "Mega 2 - SPD");
            }

            VerifyString(capabilities.text, "Pokemon - Capabilities");
            VerifyString(moves.text, "Pokemon - Moves");
            VerifyString(evolutions.text, "Pokemon - Evolutions");

            pokemon.capabilities = RemoveReturns(capabilities.text.Split(','));
            pokemon.moves = RemoveReturns(moves.text.Split('\n'));
            pokemon.evolutions = RemoveReturns(evolutions.text.Split('\n'));

            VerifyString(abilities.text, "Pokemon - Abilities");
            string[] cleanAbilities = RemoveReturns(CleanAbilites(abilities.text).Split('\n'));


            List<string> basic = new List<string>();
            List<string> adv = new List<string>();
            List<string> high = new List<string>();

            foreach (var ability in cleanAbilities) {
                if (ability.Contains("Basic: ")) {
                    basic.Add(ability.Replace("Basic: ", ""));
                } else if (ability.Contains("Adv: ")) {
                    adv.Add(ability.Replace("Adv: ", ""));
                } else if (ability.Contains("High: ")) {
                    high.Add(ability.Replace("High: ", ""));
                } else {
                    PokedexManager.manager.CreateWarningDialog("There seems to have been an error in abilities. Please take a look and see if they is any typos.");
                    return;
                }
            }

            pokemon.basicAbilities = basic.ToArray();
            pokemon.advancedAbilities = adv.ToArray();
            pokemon.highAbilities = high.ToArray();

        } catch {
            // The string/int handler functions will create the user facing error messaging. A bit hacky, but is what I can come up with at 1am...
            return;
        }

        try {
            string[] skillsArray = skills.text.Split(',');
            foreach (string skill in skillsArray) {
                string skillDie;
                if (skill.Contains("Athl")) {
                    skillDie = skill.Replace("Athl", "").Trim();
                    pokemon.athleticsDie = int.Parse(skillDie[0].ToString());
                    if (skillDie.Contains("+")) {
                        pokemon.athleticsBonus = int.Parse(skillDie[skillDie.Length - 1].ToString());
                    }
                } else if (skill.Contains("Acro")) {
                    skillDie = skill.Replace("Acro", "").Trim();
                    pokemon.acrobaticsDie = int.Parse(skillDie[0].ToString());
                    if (skillDie.Contains("+")) {
                        pokemon.acrobaticsBonus = int.Parse(skillDie[skillDie.Length - 1].ToString());
                    }
                } else if (skill.Contains("Combat")) {
                    skillDie = skill.Replace("Combat", "").Trim();
                    pokemon.combatDie = int.Parse(skillDie[0].ToString());
                    if (skillDie.Contains("+")) {
                        pokemon.combatBonus = int.Parse(skillDie[skillDie.Length - 1].ToString());
                    }
                } else if (skill.Contains("Focus")) {
                    skillDie = skill.Replace("Focus", "").Trim();
                    pokemon.focusDie = int.Parse(skillDie[0].ToString());
                    if (skillDie.Contains("+")) {
                        pokemon.focusBonus = int.Parse(skillDie[skillDie.Length - 1].ToString());
                    }
                } else if (skill.Contains("Percep")) {
                    skillDie = skill.Replace("Percep", "").Trim();
                    pokemon.perceptionDie = int.Parse(skillDie[0].ToString());
                    if (skillDie.Contains("+")) {
                        pokemon.perceptionBonus = int.Parse(skillDie[skillDie.Length - 1].ToString());
                    }
                } else if (skill.Contains("Stealth")) {
                    skillDie = skill.Replace("Stealth", "").Trim();
                    pokemon.stealthDie = int.Parse(skillDie[0].ToString());
                    if (skillDie.Contains("+")) {
                        pokemon.stealthBonus = int.Parse(skillDie[skillDie.Length - 1].ToString());
                    }
                } else if (skill.Contains("Edu: Tech")) {
                    skillDie = skill.Replace("Edu: Tech", "").Trim();
                    pokemon.techEduDie = int.Parse(skillDie[0].ToString());
                    if (skillDie.Contains("+")) {
                        pokemon.techEduBonus = int.Parse(skillDie[skillDie.Length - 1].ToString());
                    }
                } else {
                    PokedexManager.manager.CreateWarningDialog("Pokemon - Skills: Contains a skill entry that does not contina the expected skill name/abbreviation. Please look at your current entry.");
                    return;
                }
            }
        } catch {
            PokedexManager.manager.CreateWarningDialog("Pokemon - Skills: Failure to process. Please ensure you have copy pasted from the pokedex PDF or that you have entered them in properly");
            return;
        }

        string data = JsonUtility.ToJson(pokemon, true);
        data = JsonHelper.RemoveEmptyString(data);
        JObject json = JObject.Parse(data);

        if (String.IsNullOrWhiteSpace(megaName1.text)) {
            json.Property("mega").Remove();
        }
        if (String.IsNullOrWhiteSpace(megaName2.text)) {
            json.Property("altMega").Remove();
        }

        json.Property("cryAudio").Remove();
        json.Property("heldItem").Remove();
        json.Property("sprite").Remove();
        json.Property("knownMoveList").Remove();
        json.Property("movesList").Remove();
        json.Property("level").Remove();
        json.Property("loyalty").Remove();
        json.Property("hpLevel").Remove();
        json.Property("atkLevel").Remove();
        json.Property("defLevel").Remove();
        json.Property("spatkLevel").Remove();
        json.Property("spdefLevel").Remove();
        json.Property("spdLevel").Remove();
        json.Property("hpCS").Remove();
        json.Property("atkCS").Remove();
        json.Property("defCS").Remove();
        json.Property("spatkCS").Remove();
        json.Property("spdefCS").Remove();
        json.Property("spdCS").Remove();
        json.Property("maxHealth").Remove();
        json.Property("currentHealth").Remove();
        json.Property("injuries").Remove();
        json.Property("captureRate").Remove();
        json.Property("shiny").Remove();
        json.Property("asleep").Remove();
        json.Property("badlyAsleep").Remove();
        json.Property("blind").Remove();
        json.Property("totallyBlind").Remove();
        json.Property("burned").Remove();
        json.Property("confused").Remove();
        json.Property("cursed").Remove();
        json.Property("disabled").Remove();
        json.Property("enraged").Remove();
        json.Property("flinched").Remove();
        json.Property("frozen").Remove();
        json.Property("infatuated").Remove();
        json.Property("paralyzed").Remove();
        json.Property("poisoned").Remove();
        json.Property("badlyPoisoned").Remove();
        json.Property("slowed").Remove();
        json.Property("stuck").Remove();
        json.Property("suppressed").Remove();
        json.Property("trapped").Remove();
        json.Property("tripped").Remove();
        json.Property("vulnerable").Remove();
        json.Property("currentAbilities").Remove();
        json.Property("nature").Remove();

        JObject pokemonJSON = JObject.Parse(File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json")));
        JArray items = (JArray)pokemonJSON["Items"];
        items.Add(json);
        data = pokemonJSON.ToString();

        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json"), data);
        PokedexManager.manager.CreateWarningDialog("Pokemon successfully added to the Pokedex!");
    }

    public void BackUpPokemonJSON() {
        PokedexManager.manager.CreateConfirmationDialog("Are you sure you wish to backup the pokedex in its current state? This will overwrite any current Pokemon.json.bak you have.", ConfirmationType.backup);
    }

    public void RestorePokedexFromBackupJSON() {
        PokedexManager.manager.CreateConfirmationDialog("Are you sure you wish to restore the pokedex from the current backup? This cannot be undone.", ConfirmationType.restore);
    }

    public void MergePokedexWithBackupJSON() {
        PokedexManager.manager.CreateConfirmationDialog("Are you sure you wish to merge the pokedex with the current backup?", ConfirmationType.merge);
    }

    [Discardable]
    public string VerifyString(string text, string errorTitle) {
        string trimmedText = text.Trim();
        if (String.IsNullOrWhiteSpace(trimmedText)) {
            PokedexManager.manager.CreateWarningDialog(errorTitle + ": There is nothing entered in this required field...");
            throw new ArgumentNullException(trimmedText);
        }
        return trimmedText;
    }

    [Discardable]
    public int VerifyInteger(string integer, string errorTitle) {
        string trimmedInteger = integer.Trim();
        int endInteger = 0;
        if (String.IsNullOrWhiteSpace(trimmedInteger)) {
            PokedexManager.manager.CreateWarningDialog(errorTitle + ": There is nothing entered in this required field...");
            throw new ArgumentNullException("Emtpy or null string passed in a required field");
        }
        try {
            endInteger = int.Parse(trimmedInteger);
        } catch {
            PokedexManager.manager.CreateWarningDialog(errorTitle + ": Entered value is invalid. Please only enter whole numbers in this field.");
            throw new InvalidCastException("Could not cast " + trimmedInteger + " into an integer");
        }
        return endInteger;
    }

    public string[] RemoveReturns(string[] array) {
        List<string> list = array.ToList();
        for (int i = 0; i < list.Count(); i++) {
            list[i] = list[i].Replace("\r", "").Replace("\n", "").Trim();
        }
        return list.ToArray();
    }
    
    public string CleanAbilites(string abilities) {
        abilities = abilities.Replace(" Ability:", ":");
        if (Regex.IsMatch(abilities, @" Ability *[\d-]:"))
            abilities = Regex.Replace(abilities, @" Ability *[\d-]:", ":");
        return abilities;
    }

    public void SetTabList() {
        if (megaToggle1.isOn && megaToggle2.isOn) {
            megaName1.interactable = true;
            megaType1.interactable = true;
            megaAbility1.interactable = true;
            megaHP1.interactable = true;
            megaATK1.interactable = true;
            megaDEF1.interactable = true;
            megaSPATK1.interactable = true;
            megaSPDEF1.interactable = true;
            megaSPD1.interactable = true;

            megaName2.interactable = true;
            megaType2.interactable = true;
            megaAbility2.interactable = true;
            megaHP2.interactable = true;
            megaATK2.interactable = true;
            megaDEF2.interactable = true;
            megaSPATK2.interactable = true;
            megaSPDEF2.interactable = true;
            megaSPD2.interactable = true;

            inputFields = new InputField[] {
                number,
                image,
                cry,
                species,
                type,
                size,
                weight,
                gender,
                egg,
                hatch,
                diet,
                habitat,
                entry,
                hp,
                atk,
                def,
                spatk,
                spdef,
                spd,
                stage,
                moves,
                capabilities,
                evolutions,
                skills,
                abilities,
                megaName1,
                megaType1,
                megaAbility1,
                megaHP1,
                megaATK1,
                megaDEF1,
                megaSPATK1,
                megaSPDEF1,
                megaSPD1,
                megaName2,
                megaType2,
                megaAbility2,
                megaHP2,
                megaATK2,
                megaDEF2,
                megaSPATK2,
                megaSPDEF2,
                megaSPD2
            };
        } else if (megaToggle1.isOn) {
            megaToggle2.interactable = true;

            megaName1.interactable = true;
            megaType1.interactable = true;
            megaAbility1.interactable = true;
            megaHP1.interactable = true;
            megaATK1.interactable = true;
            megaDEF1.interactable = true;
            megaSPATK1.interactable = true;
            megaSPDEF1.interactable = true;
            megaSPD1.interactable = true;

            megaName2.interactable = false;
            megaType2.interactable = false;
            megaAbility2.interactable = false;
            megaHP2.interactable = false;
            megaATK2.interactable = false;
            megaDEF2.interactable = false;
            megaSPATK2.interactable = false;
            megaSPDEF2.interactable = false;
            megaSPD2.interactable = false;

            megaName2.text = "";
            megaType2.text = "";
            megaAbility2.text = "";
            megaHP2.text = "";
            megaATK2.text = "";
            megaDEF2.text = "";
            megaSPATK2.text = "";
            megaSPDEF2.text = "";
            megaSPD2.text = "";

            inputFields = new InputField[] {
                number,
                image,
                cry,
                species,
                type,
                size,
                weight,
                gender,
                egg,
                hatch,
                diet,
                habitat,
                entry,
                hp,
                atk,
                def,
                spatk,
                spdef,
                spd,
                stage,
                moves,
                capabilities,
                evolutions,
                skills,
                abilities,
                megaName1,
                megaType1,
                megaAbility1,
                megaHP1,
                megaATK1,
                megaDEF1,
                megaSPATK1,
                megaSPDEF1,
                megaSPD1
            };
        } else {
            megaToggle2.isOn = false;
            megaToggle2.interactable = false;

            megaName1.interactable = false;
            megaType1.interactable = false;
            megaAbility1.interactable = false;
            megaHP1.interactable = false;
            megaATK1.interactable = false;
            megaDEF1.interactable = false;
            megaSPATK1.interactable = false;
            megaSPDEF1.interactable = false;
            megaSPD1.interactable = false;

            megaName2.interactable = false;
            megaType2.interactable = false;
            megaAbility2.interactable = false;
            megaHP2.interactable = false;
            megaATK2.interactable = false;
            megaDEF2.interactable = false;
            megaSPATK2.interactable = false;
            megaSPDEF2.interactable = false;
            megaSPD2.interactable = false;

            megaName1.text = "";
            megaType1.text = "";
            megaAbility1.text = "";
            megaHP1.text = "";
            megaATK1.text = "";
            megaDEF1.text = "";
            megaSPATK1.text = "";
            megaSPDEF1.text = "";
            megaSPD1.text = "";

            megaName2.text = "";
            megaType2.text = "";
            megaAbility2.text = "";
            megaHP2.text = "";
            megaATK2.text = "";
            megaDEF2.text = "";
            megaSPATK2.text = "";
            megaSPDEF2.text = "";
            megaSPD2.text = "";

            inputFields = new InputField[] {
                number,
                image,
                cry,
                species,
                type,
                size,
                weight,
                gender,
                egg,
                hatch,
                diet,
                habitat,
                entry,
                hp,
                atk,
                def,
                spatk,
                spdef,
                spd,
                stage,
                moves,
                capabilities,
                evolutions,
                skills,
                abilities,
            };
        }
    }
}
