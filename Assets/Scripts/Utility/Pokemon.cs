using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BaseRelations {
    public string 
        name;

    public int 
        value,
        position;

    public BaseRelations(string _name, int _value) {
        name = _name;
        value = _value;
    }
}

[Serializable]
public class Info {
    public string
        name,
        description;
}

[Serializable]
public class TM {
    public string
        name,
        number,
        type;
}

[Serializable]
public class Ability {
    public string
        name,
        freq,
        effect,
        target,
        trigger;
}

[Serializable]
public class Move {
    public string
        name,
        typeName,
        freq,
        damageClass,
        range,
        effects,
        contestType,
        contestEffect;

    public int
        ac,
        db,
        level;

    public PokemonType
        type;

    public Move(string _name, int _level, string _type) {
        name = _name;
        level = _level;
        type = PokedexManager.types.First(x => x.typeName == _type);
    }
}

[SerializeField]
public enum Condition {
    blinded = 0,
    totallyBlinded,
    burned,
    confused,
    cursed,
    disabled,
    enraged,
    flinched,
    frozen,
    infatuated,
    paralyzed,
    poisoned,
    badlyPoisoned,
    sleeping,
    badlySleeping,
    slowed,
    stuck,
    suppressed,
    trapped,
    tripped,
    vulnerable
}

[Serializable]
public class Item {
    public Sprite
        sprite;
    
    public string
        name,
        desc,
        image;

    public int
        tier;
}

[Serializable]
public class Pokemon {

    public BaseRelations[]
        baseRelations;

    public Move[]
        movesList,
        knownMoveList;

    public Sprite
        sprite;

    public Item
        heldItem;

    public AudioClip
        cryAudio;

    public string
        savePath,
        image,
        cry,
        species,
        name,
        region,
        type,
        size,
        weight,
        gender,
        egg,
        hatch,
        diet,
        habitat,
        entry,
        basicAbility,
        advancedAbility,
        highAbility;

    public int
        number,
        level,
        hp,
        atk,
        def,
        spatk,
        spdef,
        spd,
        hpLevel,
        atkLevel,
        defLevel,
        spatkLevel,
        spdefLevel,
        spdLevel,
        hpCS,
        atkCS,
        defCS,
        spatkCS,
        spdefCS,
        spdCS,
        maxHealth,
        currentHealth,
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
        techEduBonus,
        injuries,
        captureRate;

    public bool
        legendary,      // -30 CR
        shiny,          // -10 CR
        // Conditions
        asleep,         // +5 CR
        badlyAsleep,
        blind, 
        totallyBlind,
        burned,         // +10 CR
        confused,       // +5 CR
        cursed,         // +5 CR
        disabled,       // +5 CR
        enraged,        // +5 CR
        flinched,       // +5 CR
        frozen,         // +10 CR
        infatuated,     // +5 CR
        paralyzed,      // +10 CR
        poisoned,       // +10 CR
        badlyPoisoned,
        slowed,         // +5 CR
        stuck,          // +10 CR
        suppressed,     // +5 CR
        trapped,
        tripped,
        vulnerable;

    public string[]
        evolutions,
        capabilities,
        basicAbilities,
        advancedAbilities,
        highAbilities,
        moves;

    public Nature
        nature;

    public void ToJson(string path) {
        string data = JsonUtility.ToJson(this, true);
        string finalPath = ValidatePath(path);
        this.savePath = finalPath;
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, finalPath), data);
        Debug.Log("Pokemon saved to: " + finalPath);
    }

    public static Pokemon FromJson(string path) {
        string data = File.ReadAllText(path);
        return JsonUtility.FromJson<Pokemon>(data);
    }

    private string ValidatePath(string path, int iteration = 0) {
        string newPath = path;
        if (File.Exists(path)) {
            bool match = Regex.IsMatch(newPath, @"_[\d-]*[\d-]");
            if (match == true) {
                newPath = Regex.Replace(newPath, @"_[\d-]*[\d-]", "");
                Debug.Log(newPath);
            }
            newPath = ValidatePath(newPath.Replace(".json", "_" + iteration.ToString() + ".json"), iteration+1);
        }
        return newPath;
    }

    public Pokemon Clone() {
        string data = JsonUtility.ToJson(this);
        return JsonUtility.FromJson<Pokemon>(data);
    }
}
/*
Giga Charizard
Mega Charizard X
Mega Charizard Y
Giga Butterfree
Mega Bedrill
Mega Pidgeot
Cosplay Pikachu
Pikachu Rock Star
Pikachu Belle
Pikachu Pop Star
Pikachu Ph. D
Pikachu Libre
Original Cap Pikachu
Hoenn Cap Pikachu
Sinnoh Cap Pikachu
Unova Cap Pikachu
Kalos Cap Pikchu
Alola Cap Pikachu
Partner Cap Pikachu
Giga Pikachu
Alolan Raichu
Alolan Sandshrew
Alolan Sandslash
Alolan Vulpix
Alolan Ninetales
Alolan Meowth
Galar Meowth
Giag Meowth
Alolan Persion
Mega Alakazam
Giga Machamp
Alolan Geodude
Alolan Graveller
Alolan Golem
Galar Ponyta
Galar Rapidash
Mega Slowbro
Galar Farfetch'd
Alola Grimer
Alolan Muk
Mega Gengar
Giga Gengar
Giga Kingler
Alolan Exeggutor
Alolan Marowak
Galarian Weezing
Mega Kangaskhan
Mega Gyarados
Giga Lapras
Mega Aerodactyl
Giga Snorlax
Mega Mewtwo X
Mega Mewtwo Y
Mega Ampharos
Unown A-Z
Mega Steelix
Mega Scizor
Mega Heracross
Galar Corsala
Mega Houndoom
Mega Tyranitar
Mega Sceptile
Mega Blazakin
Mega Swampert
Galar Zigzagoon
Galar Linoone
Mega Gardivour
Mega Mawille
Mega Agron
Mega Medicham
Mega Manectric
Mega Sharpedo
Mega Camerupt
Mega Altaria
Castform Rain
Castform Snow
Castform Sun
Mega Shuppet
Mega Absol
Mega Glalie
Mega Salamance
Mega Metagross
Mega Latios
Mega Latias
Burmy (Sand)
Burmy (Trash)
Mega Lopunny
Mega Garchomp
Mega Lucario
Mega Abomasnow
Mega Gallade
Mega Audino
Galar Darumaka
Galar Darmanitan
Galar Darmanitan Zen
Darmanitan Zen
Galar Yamask
Giga Garbodor
Deerling and Sawsbuck forms?
Female Jellicent
Female Frillish
Galar Stunfisk
Vivilon Forms (1-19)
Blue, Yellow, and White Floette
Blue, Yellow, and White Florges
Furfru Forms (1-9)
Aegislagh Form
Zygarde Forms 1, complete, ultra
Minior Forms (1-7)
Giga Melmetal
All Galar Pokemon ********
Giga Corvinight
Giga 826
Giga Dreadnaw
Giga Coalassus
Giga Flapple
Giga AppleTurle
Giga Silacobra
Giga Toxtricity
Giga Sizzlpede
Giga Grimmsnarl
Alcremy Forms (1-8)
Giga Alcremy
Giga Duraludon
*/
