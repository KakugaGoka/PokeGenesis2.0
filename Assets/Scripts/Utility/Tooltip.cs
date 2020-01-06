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
                PokedexManager.manager.CreateTooltipDialog(MoveToolTip(move));
                return;
            }
        }
        PokedexManager.manager.CreateWarningDialog("Move is not registered in Moves.json: '" + currentValue + "'");
    }

    public string MoveToolTip(Move move) {
        string toolTip = "Name: " + move.name;
        if (move.typeName != null) { 
            toolTip += Environment.NewLine + "Type: " + move.typeName; 
        }
        if (move.ac != 0) {
            toolTip += Environment.NewLine + "AC: " + move.ac;
        }
        if (move.db != 0) {
            int db = move.db;
            if (PokedexManager.currentPokemon.type.Contains(move.typeName)) {
                db++;
            }
            toolTip += Environment.NewLine + "DB: " + db;
        }
        if (move.effects != null) {
            toolTip += Environment.NewLine + "Effect: " + move.effects;
        }
        if (move.range != null) {
            toolTip += Environment.NewLine + "Range: " + move.range;
        }
        if (move.contestEffect != null) {
            toolTip += Environment.NewLine + "Contest Effect: " + move.contestEffect;
        }
        if (move.contestType != null) {
            toolTip += Environment.NewLine + "Contest Type: " + move.contestType;
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
                PokedexManager.manager.CreateTooltipDialog(AbilityToolTip(ability));
                return;
            }
        }
        PokedexManager.manager.CreateWarningDialog("Ability is not registered in Ability.json: '" + currentValue + "'");
    }

    public string AbilityToolTip(Ability ability) {
        string toolTip = "Name: " + ability.name;
        if (ability.freq != null) {
            toolTip += Environment.NewLine + "Freq: " + ability.freq;
        }
        if (ability.target != null) {
            toolTip += Environment.NewLine + "Target: " + ability.target;
        }
        if (ability.trigger != null) {
            toolTip += Environment.NewLine + "Trigger: " + ability.trigger;
        }
        if (ability.effect != null) {
            toolTip += Environment.NewLine + "Effect: " + ability.effect;
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
                if (info.name == "Athletics") { PokedexManager.manager.CreateTooltipDialog(SKillToolTip(info)); }
            }
        } else if(currentValue.Contains("Acro")) {
            foreach (var info in PokedexManager.skillsInfo) {
                if (info.name == "Acrobatics") { PokedexManager.manager.CreateTooltipDialog(SKillToolTip(info)); }
            }
        } else if (currentValue.Contains("Combat")) {
            foreach (var info in PokedexManager.skillsInfo) {
                if (info.name == "Combat") { PokedexManager.manager.CreateTooltipDialog(SKillToolTip(info)); }
            }
        } else if (currentValue.Contains("Focus")) {
            foreach (var info in PokedexManager.skillsInfo) {
                if (info.name == "Focus") { PokedexManager.manager.CreateTooltipDialog(SKillToolTip(info)); }
            }
        } else if (currentValue.Contains("Percep")) {
            foreach (var info in PokedexManager.skillsInfo) {
                if (info.name == "Perception") { PokedexManager.manager.CreateTooltipDialog(SKillToolTip(info)); }
            }
        } else if (currentValue.Contains("Stealth")) {
            foreach (var info in PokedexManager.skillsInfo) {
                if (info.name == "Stealth") { PokedexManager.manager.CreateTooltipDialog(SKillToolTip(info)); }
            }
        } else if (currentValue.Contains("Tech")) {
            foreach (var info in PokedexManager.skillsInfo) {
                if (info.name == "Technology Education") { PokedexManager.manager.CreateTooltipDialog(SKillToolTip(info)); }
            }
        }
    }

    public string SKillToolTip(Info skill) {
        string toolTip = "Name: " + skill.name;
        if (skill.description != null) {
            toolTip += Environment.NewLine + "Description: " + skill.description;
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
                PokedexManager.manager.CreateTooltipDialog(CapabilityToolTip(capability, currentValue));
                return;
            }
        }
        PokedexManager.manager.CreateWarningDialog("Capability is not registered in Capabilities.json: '" + currentValue + "'");
    }

    public string CapabilityToolTip(Info capability, string yourCapability) {
        string toolTip = "Name: " + yourCapability;
        if (capability.description != null) {
            toolTip += Environment.NewLine + "Description: " + capability.description;
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
                PokedexManager.manager.CreateTooltipDialog(ConditionToolTip(condition));
                return;
            }
        }
        PokedexManager.manager.CreateWarningDialog("Capability is not registered in Capabilities.json: '" + currentValue + "'");
    }

    public string ConditionToolTip(Info condition) {
        string toolTip = "Name: " + condition.name;
        if (condition.description != null) {
            toolTip += Environment.NewLine + "Description: " + condition.description;
        }
        return toolTip;
    }

    public void ShowItemToolTip() {
        UnityEngine.UI.InputField inputField = GameObject.Find("Held Item Name Field").GetComponent<UnityEngine.UI.InputField>();
        if (inputField.text == null || inputField.text == "" || inputField.text == "None") { return; }
        foreach (var item in PokedexManager.items) {
            if (inputField.text == item.name) {
                PokedexManager.manager.CreateTooltipDialog(ItemToolTip(item));
                return;
            }
        }
        PokedexManager.manager.CreateWarningDialog("Capability is not registered in Capabilities.json: '" + inputField.text + "'");
    }

    public string ItemToolTip(Item item) {
        string toolTip = "Name: " + item.name;
        if (item.desc != null) {
            toolTip += Environment.NewLine + "Description: " + item.desc;
        }
        return toolTip;
    }
}
