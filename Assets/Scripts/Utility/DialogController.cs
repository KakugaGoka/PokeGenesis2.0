using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class DialogController : MonoBehaviour
{
    public Text messageBox;
    public ConfirmationType confirmationType;
    public SaveType saveType;

    public void Close() {
        if (this.gameObject.name.Contains("Sending")) {
            Client.client.myClient.Disconnect();
        }
        Destroy(this.gameObject);
    }

    public void Confirm() {
        switch (confirmationType) {
            case ConfirmationType.trade:
                Client.client.startClient = true;
                break;
            case ConfirmationType.delete:
                PokedexManager.manager.DeleteCurrentPokemonAndEntry();
                break;
            case ConfirmationType.capture:
                PokedexManager.manager.CaptureCurrentSelected();
                break;
            case ConfirmationType.release:
                PokedexManager.manager.ReleaseCurrentSelected();
                break;
            case ConfirmationType.quit:
                Application.Quit();
                break;
            case ConfirmationType.backup:
                if (File.Exists(Path.Combine(PokedexManager.dataPath, "JSON/Pokemon.json.bak"))) {
                    File.Delete(Path.Combine(PokedexManager.dataPath, "JSON/Pokemon.json.bak"));
                }
                File.Copy(Path.Combine(PokedexManager.dataPath, "JSON/Pokemon.json"), Path.Combine(PokedexManager.dataPath, "JSON/Pokemon.json.bak"));
                break;
            case ConfirmationType.restore:
                File.Delete(Path.Combine(PokedexManager.dataPath, "JSON/Pokemon.json"));
                File.Copy(Path.Combine(PokedexManager.dataPath, "JSON/Pokemon.json.bak"), Path.Combine(PokedexManager.dataPath, "JSON/Pokemon.json"));
                break;
            case ConfirmationType.merge:
                string pokedexString = File.ReadAllText(Path.Combine(PokedexManager.dataPath, "JSON/Pokemon.json"));
                string backupString = File.ReadAllText(Path.Combine(PokedexManager.dataPath, "JSON/Pokemon.json.bak"));

                JObject pokedexJSON = JObject.Parse(pokedexString);
                JObject backupJSON = JObject.Parse(backupString);

                pokedexJSON.Merge(backupJSON, new JsonMergeSettings {
                    MergeArrayHandling = MergeArrayHandling.Union
                });

                string data = pokedexJSON.ToString();
                File.WriteAllText(Path.Combine(PokedexManager.dataPath, "JSON/Pokemon.json"), data);
                PokedexManager.manager.CreateWarningDialog("Pokedex successfully merged with the backup pokedex data!");
                break;

        }
        Destroy(this.gameObject);
    }

    public void LoadEditDialog(SaveType saveType) {
        this.saveType = saveType;
        string[] saveArray;
        string[] infoArray;
        Pokemon pokemon = PokedexManager.currentPokemon;
        Pokemon basePokemon = PokedexManager.pokedex.First(x => x.species == pokemon.species);

        if (saveType == SaveType.capabilities) {
            saveArray = pokemon.capabilities;
            infoArray = basePokemon.capabilities;
        } else if (saveType == SaveType.abilities) {
            saveArray = pokemon.currentAbilities;
            List<string> infoList = new List<string>();
            foreach (var bAb in pokemon.basicAbilities) {
                infoList.Add("Basic Ability: " + bAb);
            }
            foreach (var aAb in pokemon.advancedAbilities) {
                infoList.Add("Adv Ability: " + aAb);
            }
            foreach (var hAb in pokemon.highAbilities) {
                infoList.Add("High Ability: " + hAb);
            }
            infoArray = infoList.ToArray();
        } else if (saveType == SaveType.skills) {
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
            saveArray = new string[] {
                athl,
                acro,
                combat,
                focus,
                percep,
                stealth,
                techEdu
            };

            string athlBase = "Athl " + basePokemon.athleticsDie + "d6";
            if (basePokemon.athleticsBonus != 0) { athlBase += "+" + basePokemon.athleticsBonus; }
            string acroBase = "Acro " + basePokemon.acrobaticsDie + "d6";
            if (basePokemon.acrobaticsBonus != 0) { acroBase += "+" + basePokemon.acrobaticsBonus; }
            string combatBase = "Combat " + basePokemon.combatDie + "d6";
            if (basePokemon.combatBonus != 0) { combatBase += "+" + basePokemon.combatBonus; }
            string focusBase = "Focus " + basePokemon.focusDie + "d6";
            if (basePokemon.focusBonus != 0) { focusBase += "+" + basePokemon.focusBonus; }
            string percepBase = "Percep " + basePokemon.perceptionDie + "d6";
            if (basePokemon.perceptionBonus != 0) { percepBase += "+" + basePokemon.perceptionBonus; }
            string stealthBase = "Stealth " + basePokemon.stealthDie + "d6";
            if (basePokemon.stealthBonus != 0) { stealthBase += "+" + basePokemon.stealthBonus; }
            string techEduBase = "Edu: Tech " + basePokemon.techEduDie + "d6";
            if (basePokemon.techEduBonus != 0) { techEduBase += "+" + basePokemon.techEduBonus; }
            infoArray = new string[] {
                athlBase,
                acroBase,
                combatBase,
                focusBase,
                percepBase,
                stealthBase,
                techEduBase
            };

        } else if (saveType == SaveType.moves) {
            List<string> saveList = new List<string>();
            foreach (var move in pokemon.knownMoveList) {
                saveList.Add(move.level + " " + move.name + " - " + move.type.typeName);
            }
            saveArray = saveList.ToArray();
            infoArray = basePokemon.moves;
        } else if (saveType == SaveType.features) {
            saveArray = pokemon.features;
            infoArray = pokemon.features;
        } else if (saveType == SaveType.edges) {
            saveArray = pokemon.edges;
            infoArray = pokemon.edges;
        } else {
            return;
        }
        InputField saveBox = GameObject.Find("New Info Box").GetComponent<InputField>();
        Text infoBox = GameObject.Find("Original Info Box").GetComponent<Text>();

        saveBox.text = "";
        infoBox.text = "";

        foreach (var item in saveArray) {
            saveBox.text += item + "\n";
        }
        foreach (var item in infoArray) {
            infoBox.text += item + "\n";
        }
    }

    public void SaveEditDialog() {
        Pokemon pokemon = PokedexManager.currentPokemon.Clone();
        InputField saveBox = GameObject.Find("New Info Box").GetComponent<InputField>();
        if (saveType == SaveType.capabilities) {
            SaveCapabilities(pokemon, saveBox.text);
        } else if (saveType == SaveType.abilities) {
            SaveAbilities(pokemon, saveBox.text);
        } else if (saveType == SaveType.skills) {
            SaveSkills(pokemon, saveBox.text);
        } else if (saveType == SaveType.moves) {
            SaveMoves(pokemon, saveBox.text);
        }else if (saveType == SaveType.features) {
            SaveFeatures(pokemon, saveBox.text);
        } else if (saveType == SaveType.edges) {
            SaveEdges(pokemon, saveBox.text);
        } else {
            return;
        }
        PokedexManager.currentEntry.GetComponent<PokedexEntry>().pokemon = pokemon.Clone();
        PokedexManager.currentPokemon = pokemon.Clone();
        pokemon.ToJson(pokemon.savePath, true);
        SheetController sheetController = GameObject.Find("Scroll View").GetComponent<SheetController>();
        sheetController.OnSelected(PokedexManager.currentPokemon, PokedexManager.currentEntry);
        Destroy(this.gameObject);
    }

    public void SaveFeatures(Pokemon pokemon, string text) {
        pokemon.features = PokedexManager.RemoveReturns(text.Split('\n'));
    }

    public void SaveEdges(Pokemon pokemon, string text) {
        pokemon.edges = PokedexManager.RemoveReturns(text.Split('\n'));
    }

    public void SaveCapabilities(Pokemon pokemon, string text) {
        pokemon.capabilities = PokedexManager.RemoveReturns(PokedexManager.CleanCapabilites(text, '\n'));
    }

    public void SaveAbilities(Pokemon pokemon, string text) {
        pokemon.currentAbilities = PokedexManager.RemoveReturns(text.Split('\n'));
    }

    public void SaveMoves(Pokemon pokemon, string text) {
        string[] pokemonMoves = PokedexManager.RemoveReturns(text.Split('\n'));
        List<Move> newMoveList = new List<Move>();
        foreach (string move in pokemonMoves) {
            try {
                string[] moveSplit = move.Split(new string[] { " - " }, StringSplitOptions.None);
                string type = moveSplit[moveSplit.Length - 1].Trim();
                string[] levelAndName = moveSplit[0].Split(' ');
                int level = int.Parse(levelAndName[0].Trim());
                string name = "";
                for (int i = 1; i < levelAndName.Length; i++) {
                    name += " " + levelAndName[i];
                }
                name = name.Trim();
                newMoveList.Add(new Move(name, level, type));
            } catch {
                Debug.Log(move);
            }
        }
        pokemon.knownMoveList = newMoveList.ToArray();
        pokemon.knownMoveList = pokemon.knownMoveList.OrderBy(x => x.level).ToArray();
    }

    public void SaveSkills(Pokemon pokemon, string text) {
        try {
            string[] skillsArray = text.Trim().Split('\n');
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
                    PokedexManager.manager.CreateWarningDialog("Pokemon - Skills: Contains a skill entry that does not contian the expected skill name/abbreviation. Please look at your current entry.");
                    return;
                }
            }
        } catch {
            PokedexManager.manager.CreateWarningDialog("Pokemon - Skills: Failure to process. Please ensure you have copy pasted from the pokedex PDF or that you have entered them in properly");
            return;
        }
    }

}

public enum ConfirmationType {
    trade = 0,
    delete, 
    capture,
    release,
    quit,
    backup,
    restore,
    merge
}

public enum SaveType {
    capabilities = 0,
    abilities,
    skills,
    moves,
    edges,
    features
}
