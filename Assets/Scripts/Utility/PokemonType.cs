using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PokemonType
{
    public string typeName;
    public float
        normal,
        fire,
        water,
        electric,
        grass,
        ice,
        fighting,
        poison,
        ground,
        flying,
        bug,
        rock,
        psychic,
        ghost,
        dragon,
        dark,
        steel,
        fairy;

    /// <summary>
    /// A contructor for Pokemon Types. The float required for each type is the multiplier for damage of that type.
    /// </summary>
    /// <param name="_typeName"></param>
    /// <param name="_normal"></param>
    /// <param name="_fire"></param>
    /// <param name="_water"></param>
    /// <param name="_electric"></param>
    /// <param name="_grass"></param>
    /// <param name="_ice"></param>
    /// <param name="_fighting"></param>
    /// <param name="_poison"></param>
    /// <param name="_ground"></param>
    /// <param name="_flying"></param>
    /// <param name="_bug"></param>
    /// <param name="_rock"></param>
    /// <param name="_psychic"></param>
    /// <param name="_ghost"></param>
    /// <param name="_dragon"></param>
    /// <param name="_dark"></param>
    /// <param name="_steel"></param>
    /// <param name="_fairy"></param>
    public PokemonType(string _typeName, float _normal, float _fire, float _water, float _electric, float _grass, float _ice,
        float _fighting, float _poison, float _ground, float _flying, float _bug, float _rock, float _psychic, float _ghost, 
        float _dragon, float _dark, float _steel, float _fairy)
    {
        typeName = _typeName;
        normal = _normal;
        fire = _fire;
        water = _water;
        electric = _electric;
        grass = _grass;
        ice = _ice;
        fighting = _fighting;
        poison = _poison;
        ground = _ground;
        flying = _flying;
        bug = _bug;
        rock = _rock;
        psychic = _psychic;
        ghost = _ghost;
        dragon = _dragon;
        dark = _dark;
        steel = _steel;
        fairy = _fairy;
    }
}