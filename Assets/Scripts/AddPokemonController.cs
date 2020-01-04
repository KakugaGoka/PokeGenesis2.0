using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Newtonsoft.Json.Linq;

public class AddPokemonController : MonoBehaviour
{
    private List<Pokemon> pokemonToDex = new List<Pokemon>();
    private InputField[] inputFields;
    private int currentField;
    private string intWarning = ": Entered value is invalid. Please only enter whole numbers in this field.";

    private Toggle
        legendary;

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
        basicAbilities,
        advAbilities,
        highAbilities,
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
        basicAbilities = GameObject.Find("Basic Ability Field").GetComponent<InputField>();
        advAbilities = GameObject.Find("Advanced Ability Field").GetComponent<InputField>();
        highAbilities = GameObject.Find("High Ability Field").GetComponent<InputField>();

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
            basicAbilities,
            advAbilities,
            highAbilities,
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
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
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
        basicAbilities.text = "";
        advAbilities.text = "";
        highAbilities.text = "";
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

        pokemon.species = species.text.Trim();
        pokemon.image = image.text.Trim();
        pokemon.cry = cry.text.Trim();
        try { pokemon.number = int.Parse(number.text.Trim());
        } catch {
            PokedexManager.manager.CreateWarningDialog("Pokemon - Number" + intWarning);
            return;
        }
        pokemon.type = type.text.Trim();
        pokemon.size = size.text.Trim();
        pokemon.weight = weight.text.Trim();
        pokemon.gender = gender.text.Trim();
        pokemon.egg = egg.text.Trim();
        pokemon.hatch = hatch.text.Trim();
        pokemon.diet = diet.text.Trim();
        pokemon.habitat = habitat.text.Trim();
        pokemon.entry = entry.text.Trim();
        try { pokemon.hp = int.Parse(hp.text.Trim());
        } catch {
            PokedexManager.manager.CreateWarningDialog("Pokemon - HP" + intWarning);
            return;
        }
        try { pokemon.atk = int.Parse(atk.text.Trim());
        } catch {
            PokedexManager.manager.CreateWarningDialog("Pokemon - ATK" + intWarning);
            return;
        }
        try { pokemon.def = int.Parse(def.text.Trim());
        } catch {
            PokedexManager.manager.CreateWarningDialog("Pokemon - DEF" + intWarning);
            return;
        }
        try { pokemon.spatk = int.Parse(spatk.text.Trim());
        } catch {
            PokedexManager.manager.CreateWarningDialog("Pokemon - SPATK" + intWarning);
            return;
        }
        try { pokemon.spdef = int.Parse(spdef.text.Trim());
        } catch {
            PokedexManager.manager.CreateWarningDialog("Pokemon - SPDEF" + intWarning);
            return;
        }
        try { pokemon.spd = int.Parse(spd.text.Trim());
        } catch {
            PokedexManager.manager.CreateWarningDialog("Pokemon - SPD" + intWarning);
            return;
        }
        try { pokemon.stage = int.Parse(stage.text.Trim());
        } catch {
            PokedexManager.manager.CreateWarningDialog("Pokemon - Stage" + intWarning);
            return;
        }
        pokemon.legendary = legendary.isOn;
        if (!String.IsNullOrWhiteSpace(megaName1.text)) {
            pokemon.mega.name = megaName1.text.Trim();
            pokemon.mega.type = megaType1.text.Trim();
            pokemon.mega.ability = megaAbility1.text.Trim();
            try { pokemon.mega.hp = int.Parse(megaHP1.text.Trim());
            } catch {
                PokedexManager.manager.CreateWarningDialog("Mega 1 - HP" + intWarning);
                return;
            }
            try { pokemon.mega.atk = int.Parse(megaATK1.text.Trim());
            } catch {
                PokedexManager.manager.CreateWarningDialog("Mega 1 - ATK" + intWarning);
                return;
            }
            try { pokemon.mega.def = int.Parse(megaDEF1.text.Trim());
            } catch {
                PokedexManager.manager.CreateWarningDialog("Mega 1 - DEF" + intWarning);
                return;
            }
            try { pokemon.mega.spatk = int.Parse(megaSPATK1.text.Trim());
            } catch {
                PokedexManager.manager.CreateWarningDialog("Mega 1 - SPATK" + intWarning);
                return;
            }
            try { pokemon.mega.spdef = int.Parse(megaSPDEF1.text.Trim());
            } catch {
                PokedexManager.manager.CreateWarningDialog("Mega 1 - SPDEF" + intWarning);
                return;
            }
            try { pokemon.mega.spd = int.Parse(megaSPD1.text.Trim());
            } catch {
                PokedexManager.manager.CreateWarningDialog("Mega 1 - SPD" + intWarning);
                return;
            }
        }
        if (!String.IsNullOrWhiteSpace(megaName2.text.Trim())) {
            pokemon.altMega.name = megaName2.text.Trim();
            pokemon.altMega.type = megaType2.text.Trim();
            pokemon.altMega.ability = megaAbility2.text.Trim();
            try { pokemon.altMega.hp = int.Parse(megaHP2.text.Trim());
            } catch {
                PokedexManager.manager.CreateWarningDialog("Mega 2 - HP" + intWarning);
                return;
            }
            try { pokemon.altMega.atk = int.Parse(megaATK2.text.Trim());
            } catch {
                PokedexManager.manager.CreateWarningDialog("Mega 2 - ATK" + intWarning);
                return;
            }
            try { pokemon.altMega.def = int.Parse(megaDEF2.text.Trim());
            } catch {
                PokedexManager.manager.CreateWarningDialog("Mega 2 - DEF" + intWarning);
                return;
            }
            try { pokemon.altMega.spatk = int.Parse(megaSPATK2.text.Trim());
            } catch {
                PokedexManager.manager.CreateWarningDialog("Mega 2 - SPATK" + intWarning);
                return;
            }
            try { pokemon.altMega.spdef = int.Parse(megaSPDEF2.text.Trim());
            } catch {
                PokedexManager.manager.CreateWarningDialog("Mega 2 - SPDEF" + intWarning);
                return;
            }
            try { pokemon.altMega.spd = int.Parse(megaSPD2.text.Trim());
            } catch {
                PokedexManager.manager.CreateWarningDialog("Mega 2 - SPD" + intWarning);
                return;
            }
        }
        pokemon.capabilities = RemoveReturns(capabilities.text.Split(','));
        pokemon.moves = RemoveReturns(moves.text.Split('\n'));
        pokemon.evolutions = RemoveReturns(evolutions.text.Split('\n'));
        pokemon.basicAbilities = RemoveReturns(basicAbilities.text.Split('\n'));
        pokemon.advancedAbilities = RemoveReturns(advAbilities.text.Split('\n'));
        pokemon.highAbilities = RemoveReturns(highAbilities.text.Split('\n'));

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
        File.Copy(Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json"), Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json.bak"));
    }

    public string[] RemoveReturns(string[] array) {
        List<string> list = array.ToList();
        for (int i = 0; i < list.Count(); i++) {
            list[i] = list[i].Replace("\r", "").Replace("\n", "").Trim();
        }
        return list.ToArray();
    }
}
