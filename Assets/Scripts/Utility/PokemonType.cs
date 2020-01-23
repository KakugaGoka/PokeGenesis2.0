using System;
using UnityEngine;

[Serializable]
public class TypeColor {
    public float
        r,
        g,
        b;
}

[Serializable]
public class PokemonType {
    public string 
        typeName;
    
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

    public TypeColor
        color;

    public Color GetColor() {
        Color value = Color.clear;
        if (color != null) {
            value = new Color(color.r, color.g, color.b);
        } else {
            Debug.LogError("Color could not be generated for PokemonType \"" + typeName + "\"");
        }
        return value;
    }
}