using System;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public void ShowMoveToolTip() {
        Dropdown dropdown = GameObject.Find("Moves Dropdown").GetComponent<Dropdown>();
        if (dropdown.options.Count < 1) { return; }
        string currentValue = dropdown.options[dropdown.value].text;
        if (currentValue == null || currentValue == "" || currentValue == "None") { return; }
        foreach (var move in PokedexManager.moves) {
            if (move.name == currentValue) {
                Color color = Color.clear;
                if (move.typeName == "Normal") {
                    color = new Color(56.5f / 100f, 56.1f / 100f, 37.3f / 100f);
                } else if (move.typeName == "Fire") {
                    color = new Color(88.2f / 100f, 42.7f / 100f, 9.4f / 100f);
                } else if (move.typeName == "Water") {
                    color = new Color(27.8f / 100f, 43.9f / 100f, 92.5f / 100f);
                } else if (move.typeName == "Electric") {
                    color = new Color(94.9f / 100f, 75.7f / 100f, 5.1f / 100f);
                } else if (move.typeName == "Grass") {
                    color = new Color(36.1f / 100f, 69.4f / 100f, 22.7f / 100f);
                } else if (move.typeName == "Ice") {
                    color = new Color(44.3f / 100f, 78f / 100f, 78.4f / 100f);
                } else if (move.typeName == "Fighting") {
                    color = new Color(61.2f / 100f, 16.5f / 100f, 12.5f / 100f);
                } else if (move.typeName == "Poison") {
                    color = new Color(53.3f / 100f, 20.4f / 100f, 53.7f / 100f);
                } else if (move.typeName == "Ground") {
                    color = new Color(83.1f / 100f, 67.5f / 100f, 23.1f / 100f);
                } else if (move.typeName == "Flying") {
                    color = new Color(58.4f / 100f, 50.2f / 100f, 79.6f / 100f);
                } else if (move.typeName == "Psychic") {
                    color = new Color(94.9f / 100f, 14.5f / 100f, 37.6f / 100f);
                } else if (move.typeName == "Bug") {
                    color = new Color(56.9f / 100f, 61.6f / 100f, 12.9f / 100f);
                } else if (move.typeName == "Rock") {
                    color = new Color(60f / 100f, 51.8f / 100f, 16.1f / 100f);
                } else if (move.typeName == "Ghost") {
                    color = new Color(35.7f / 100f, 27.8f / 100f, 48.6f / 100f);
                } else if (move.typeName == "Dragon") {
                    color = new Color(32.9f / 100f, 7.8f / 100f, 93.7f / 100f);
                } else if (move.typeName == "Dark") {
                    color = new Color(46.7f / 100f, 33.3f / 100f, 26.7f / 100f);
                } else if (move.typeName == "Steel") {
                    color = new Color(66.7f / 100f, 66.7f / 100f, 73.3f / 100f);
                } else if (move.typeName == "Fairy") {
                    color = new Color(93.3f / 100f, 60f / 100f, 93.3f / 100f);
                }
                PokedexManager.manager.CreateTooltipDialog(move.name, move.typeName, MoveToolTip(move), color);
                return;
            }
        }
        PokedexManager.manager.CreateWarningDialog("Move is not registered in Moves.json: '" + currentValue + "'");
    }

    public string MoveToolTip(Move move) {
        string toolTip = "";
        if (move.ac != 0) {
            toolTip += "AC: " + move.ac + Environment.NewLine;
        }
        if (move.db != 0) {
            int db = move.db;
            if (PokedexManager.currentPokemon.type.Contains(move.typeName)) {
                db++;
            }
            toolTip += "DB: " + db + Environment.NewLine;
        }
        if (move.effects != null) {
            toolTip += "Effect: " + move.effects + Environment.NewLine;
        }
        if (move.range != null) {
            toolTip += "Range: " + move.range + Environment.NewLine;
        }
        if (move.contestEffect != null) {
            toolTip += "Contest Effect: " + move.contestEffect + Environment.NewLine;
        }
        if (move.contestType != null) {
            toolTip += "Contest Type: " + move.contestType + Environment.NewLine;
        }
        return toolTip;
    }

    public void ShowAbilityToolTip() {
        Dropdown dropdown = GameObject.Find("Abilities Dropdown").GetComponent<Dropdown>();
        if (dropdown.options.Count < 1) { return; }
        string currentValue = dropdown.options[dropdown.value].text;
        if (currentValue == null || currentValue == "" || currentValue == "None") { return; }
        foreach (var ability in PokedexManager.abilities) {
            if (ability.name == currentValue) {
                PokedexManager.manager.CreateTooltipDialog(ability.name, "", AbilityToolTip(ability));
                return;
            }
        }
        PokedexManager.manager.CreateWarningDialog("Ability is not registered in Ability.json: '" + currentValue + "'");
    }

    public string AbilityToolTip(Ability ability) {
        string toolTip = "";
        if (ability.freq != null) {
            toolTip += "Freq: " + ability.freq + Environment.NewLine;
        }
        if (ability.target != null) {
            toolTip += "Target: " + ability.target + Environment.NewLine;
        }
        if (ability.trigger != null) {
            toolTip += "Trigger: " + ability.trigger + Environment.NewLine;
        }
        if (ability.effect != null) {
            toolTip += "Effect: " + ability.effect + Environment.NewLine;
        }
        return toolTip;
    }

    public enum Skill {
        athletics = 0,
        acrobatics,
        combat,
        focus,
        perception,
        stealth,
        techEdu
    }

    public void ShowSkillToolTip() {
        Dropdown dropdown = GameObject.Find("Skills Dropdown").GetComponent<Dropdown>();
        if (dropdown.options.Count < 1) { return; }
        string currentValue = dropdown.options[dropdown.value].text;
        if (currentValue.Contains("Athl")) {
            foreach (var info in PokedexManager.skillsInfo) {
                if (info.name == "Athletics") { PokedexManager.manager.CreateTooltipDialog(info.name, "", SkillToolTip(info)); }
            }
        } else if(currentValue.Contains("Acro")) {
            foreach (var info in PokedexManager.skillsInfo) {
                if (info.name == "Acrobatics") { PokedexManager.manager.CreateTooltipDialog(info.name, "", SkillToolTip(info)); }
            }
        } else if (currentValue.Contains("Combat")) {
            foreach (var info in PokedexManager.skillsInfo) {
                if (info.name == "Combat") { PokedexManager.manager.CreateTooltipDialog(info.name, "", SkillToolTip(info)); }
            }
        } else if (currentValue.Contains("Focus")) {
            foreach (var info in PokedexManager.skillsInfo) {
                if (info.name == "Focus") { PokedexManager.manager.CreateTooltipDialog(info.name, "", SkillToolTip(info)); }
            }
        } else if (currentValue.Contains("Percep")) {
            foreach (var info in PokedexManager.skillsInfo) {
                if (info.name == "Perception") { PokedexManager.manager.CreateTooltipDialog(info.name, "", SkillToolTip(info)); }
            }
        } else if (currentValue.Contains("Stealth")) {
            foreach (var info in PokedexManager.skillsInfo) {
                if (info.name == "Stealth") { PokedexManager.manager.CreateTooltipDialog(info.name, "", SkillToolTip(info)); }
            }
        } else if (currentValue.Contains("Tech")) {
            foreach (var info in PokedexManager.skillsInfo) {
                if (info.name == "Technology Education") { PokedexManager.manager.CreateTooltipDialog(info.name, "", SkillToolTip(info)); }
            }
        }
    }

    public string SkillToolTip(Info skill) {
        string toolTip = "";
        if (skill.description != null) {
            toolTip += "Description: " + skill.description;
        }
        return toolTip;
    }

    public void ShowCapabilityToolTip() {
        Dropdown dropdown = GameObject.Find("Capabilities Dropdown").GetComponent<Dropdown>();
        if (dropdown.options.Count < 1) { return; }
        string currentValue = dropdown.options[dropdown.value].text;
        if (currentValue == null || currentValue == "" || currentValue == "None") { return; }
        foreach (var capability in PokedexManager.capabilitiesInfo) {
            if (currentValue.Contains(capability.name)) {
                PokedexManager.manager.CreateTooltipDialog(currentValue, "", CapabilityToolTip(capability, currentValue));
                return;
            }
        }
        PokedexManager.manager.CreateWarningDialog("Capability is not registered in Capabilities.json: '" + currentValue + "'");
    }

    public string CapabilityToolTip(Info capability, string yourCapability) {
        string toolTip = "";
        if (capability.description != null) {
            toolTip += "Description: " + capability.description;
        }
        return toolTip;
    }

    public void ShowConditionToolTip() {
        Dropdown dropdown = GameObject.Find("Conditions Dropdown").GetComponent<Dropdown>();
        if (dropdown.options.Count < 1) { return; }
        string currentValue = dropdown.options[dropdown.value].text;
        if (currentValue == null || currentValue == "" || currentValue == "None") { return; }
        foreach (var condition in PokedexManager.conditionsInfo) {
            if (currentValue == condition.name) {
                PokedexManager.manager.CreateTooltipDialog(condition.name, "", ConditionToolTip(condition));
                return;
            }
        }
        PokedexManager.manager.CreateWarningDialog("Capability is not registered in Capabilities.json: '" + currentValue + "'");
    }

    public string ConditionToolTip(Info condition) {
        string toolTip = "";
        if (condition.description != null) {
            toolTip += "Description: " + condition.description;
        }
        return toolTip;
    }

    public void ShowItemToolTip() {
        UnityEngine.UI.InputField inputField = GameObject.Find("Held Item Name Field").GetComponent<UnityEngine.UI.InputField>();
        if (inputField.text == null || inputField.text == "" || inputField.text == "None") { return; }
        foreach (var item in PokedexManager.items) {
            if (inputField.text == item.name) {
                PokedexManager.manager.CreateTooltipDialog(item.name, "", ItemToolTip(item));
                return;
            }
        }
        PokedexManager.manager.CreateWarningDialog("Capability is not registered in Capabilities.json: '" + inputField.text + "'");
    }

    public string ItemToolTip(Item item) {
        string toolTip = "";
        if (item.desc != null) {
            toolTip += "Description: " + item.desc;
        }
        return toolTip;
    }

    public void ShowEdgeToolTip() {
        Dropdown dropdown = GameObject.Find("Edges Dropdown").GetComponent<Dropdown>();
        if (dropdown.options.Count < 1) { return; }
        string currentValue = dropdown.options[dropdown.value].text;
        if (currentValue == null || currentValue == "" || currentValue == "None") { return; }
        PokedexManager.manager.CreateTooltipDialog(currentValue, "", EdgeToolTip(currentValue));
    }

    public string EdgeToolTip(string edge) {
        return " Edge Tooltips are not currently supported. For info on the Pokemon Edge \"" + edge + "\" please reference the source books.";
    }

    public void ShowFeatureToolTip() {
        Dropdown dropdown = GameObject.Find("Features Dropdown").GetComponent<Dropdown>();
        if (dropdown.options.Count < 1) { return; }
        string currentValue = dropdown.options[dropdown.value].text;
        if (currentValue == null || currentValue == "" || currentValue == "None") { return; }
        PokedexManager.manager.CreateTooltipDialog(currentValue, "", FeatureToolTip(currentValue));
    }

    public string FeatureToolTip(string feature) {
        return " Feature Tooltips are not currently supported. For info on the Pokemon Feature \"" + feature + "\" please reference the source books.";
    }
}
