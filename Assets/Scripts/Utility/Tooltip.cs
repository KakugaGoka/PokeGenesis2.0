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
                Color color = move.type.GetColor();
                PokedexManager.manager.CreateTooltipDialog(move.name, move.typeName, MoveToolTip(move), color);
                return;
            }
        }
        PokedexManager.manager.CreateWarningDialog("Move is not registered in Moves.json: '" + currentValue + "'");
    }

    public void ShowMoveToolTipFromPrefab(Move move) {
        Color color = move.type.GetColor();
        PokedexManager.manager.CreateTooltipDialog(move.name, move.typeName, MoveToolTip(move), color);
    }

    public void ShowItemToolTipFromPrefab(Item item) {
        PokedexManager.manager.CreateTooltipDialog(item.name, item.group, ItemToolTip(item), Color.gray);
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
            if (currentValue.Contains(ability.name)) {
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
