using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Pokemon
{
    public Sprite
        sprite;

    public string 
        image,
        species, 
        region,
        type,
        size,
        weight,
        gender,
        egg,
        hatch,
        diet,
        habitat,
        entry;

    public int  
        number,
        hp,
        atk,
        def,
        spatk,
        spdef,
        spd,
        stage,
        athleticsDie,
        acrobaticsDie,
        combatDie,
        stealthDie,
        perceptionDie,
        focusDie,
        techEduDie,
        athleticsBonus,
        acrobaticsBonus,
        combatBonus,
        stealthBonus,
        perceptionBonus,
        focusBonus,
        techEduBonus;

    public bool
        legendary;

    public string[]
        evolutions,
        capabilities,
        basicAbilites,
        advancedAbilities,
        highAbilities,
        moves; 

    public Pokemon(string _image,
            int _number,
            int _hp,
            int _atk,
            int _def,
            int _spatk,
            int _spdef,
            int _spd,
            int _stage,
            int _athleticsDie,
            int _acrobaticsDie,
            int _combatDie,
            int _stealthDie,
            int _perceptionDie,
            int _focusDie,
            int _techEduDie,
            int _athleticsBonus,
            int _acrobaticsBonus,
            int _combatBonus,
            int _stealthBonus,
            int _perceptionBonus,
            int _focusBonus,
            int _techEduBonus,
            string _species,
            string _region,
            string _type,
            string _size,
            string _weight,
            string _gender,
            string _egg,
            string _hatch,
            string _diet,
            string _habitat,
            string _entry,
            bool _legendary,
            string[] _evolutions,
            string[] _capabilities,
            string[] _basicAbilites,
            string[] _advancedAbilites,
            string[] _highAbilities,
            string[] _moves
        )
    {
        image = _image;
        number = _number;
        hp = _hp;
        atk = _atk;
        def = _def;
        spatk = _spatk;
        spdef = _spdef;
        spd = _spd;
        stage = _stage;
        species = _species;
        region = _region;
        type = _type;
        size = _size;
        weight = _weight;
        gender = _gender;
        egg = _egg;
        hatch = _hatch;
        diet = _diet;
        habitat = _habitat;
        entry = _entry;
        legendary = _legendary;
        evolutions = _evolutions;
        capabilities = _capabilities;
        moves = _moves;
        basicAbilites = _basicAbilites;
        advancedAbilities = _advancedAbilites;
        highAbilities = _highAbilities;
        athleticsDie = _athleticsDie;
        acrobaticsDie = _acrobaticsDie;
        combatDie = _combatDie;
        stealthDie = _stealthDie;
        perceptionDie = _perceptionDie;
        focusDie = _focusDie;
        techEduDie = _techEduDie;
        athleticsBonus = _athleticsBonus;
        acrobaticsBonus = _acrobaticsBonus;
        combatBonus = _combatBonus;
        stealthBonus = _stealthBonus;
        perceptionBonus = _perceptionBonus;
        focusBonus = _focusBonus;
        techEduBonus = _techEduBonus;
    }
}
