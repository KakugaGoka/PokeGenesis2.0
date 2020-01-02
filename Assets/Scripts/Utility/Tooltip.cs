﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public InputField inputField;

    public void ShowMoveToolTip() {
        if (inputField.text == null || inputField.text == "" || inputField.text == "None") { return; }
        foreach (var move in PokedexManager.moves) {
            if (move.name == inputField.text) {
                PokedexManager.manager.CreateTooltipDialog(MoveToolTip(move));
                return;
            }
        }
        PokedexManager.manager.CreateWarningDialog("Move is not registered in Moves.json: '" + inputField.text + "'");
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
            toolTip += Environment.NewLine + "DB: " + move.db;
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
        if (inputField.text == null || inputField.text == "" || inputField.text == "None") { return; }
        foreach (var ability in PokedexManager.abilities) {
            if (ability.name == inputField.text) {
                PokedexManager.manager.CreateTooltipDialog(AbilityToolTip(ability));
                return;
            }
        }
        PokedexManager.manager.CreateWarningDialog("Ability is not registered in Ability.json: '" + inputField.text + "'");
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

    public void ShowSkillToolTip(int skillNumber) {
        Skill skill = (Skill)skillNumber;
        switch (skill) {
            case Skill.athletics:
                foreach (var info in PokedexManager.skillsInfo) {
                    if (info.name == "Athletics") { PokedexManager.manager.CreateTooltipDialog(SKillToolTip(info)); }
                }
                return;
            case Skill.acrobatics:
                foreach (var info in PokedexManager.skillsInfo) {
                    if (info.name == "Acrobatics") { PokedexManager.manager.CreateTooltipDialog(SKillToolTip(info)); }
                }
                return;
            case Skill.combat:
                foreach (var info in PokedexManager.skillsInfo) {
                    if (info.name == "Combat") { PokedexManager.manager.CreateTooltipDialog(SKillToolTip(info)); }
                }
                return;
            case Skill.focus:
                foreach (var info in PokedexManager.skillsInfo) {
                    if (info.name == "Focus") { PokedexManager.manager.CreateTooltipDialog(SKillToolTip(info)); }
                }
                return;
            case Skill.perception:
                foreach (var info in PokedexManager.skillsInfo) {
                    if (info.name == "Perception") { PokedexManager.manager.CreateTooltipDialog(SKillToolTip(info)); }
                }
                return;
            case Skill.stealth:
                foreach (var info in PokedexManager.skillsInfo) {
                    if (info.name == "Stealth") { PokedexManager.manager.CreateTooltipDialog(SKillToolTip(info)); }
                }
                return;
            case Skill.techEdu:
                foreach (var info in PokedexManager.skillsInfo) {
                    if (info.name == "Technology Education") { PokedexManager.manager.CreateTooltipDialog(SKillToolTip(info)); }
                }
                return;
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
        if (inputField.text == null || inputField.text == "" || inputField.text == "None") { return; }
        foreach (var capability in PokedexManager.capabilitiesInfo) {
            if (inputField.text.Contains(capability.name)) {
                PokedexManager.manager.CreateTooltipDialog(CapabilityToolTip(capability, inputField.text));
                return;
            }
        }
        PokedexManager.manager.CreateWarningDialog("Capability is not registered in Capabilities.json: '" + inputField.text + "'");
    }

    public string CapabilityToolTip(Info capability, string yourCapability) {
        string toolTip = "Name: " + yourCapability;
        if (capability.description != null) {
            toolTip += Environment.NewLine + "Description: " + capability.description;
        }
        return toolTip;
    }
}