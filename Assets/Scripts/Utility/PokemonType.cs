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

    public string[]
        immuneTo,
        superResistantTo,
        resistantTo,
        normalDamage,
        weakTo,
        superWeakTo;

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