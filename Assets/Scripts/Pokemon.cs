using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Pokemon {
    public Sprite
        sprite;

    public Color
        color;

    public string
        image,
        audio,
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
        legendary,
        shiny,
        colorHasBeenSet;

    public string[]
        evolutions,
        capabilities,
        basicAbilites,
        advancedAbilities,
        highAbilities,
        moves;

    public Pokemon(string _image,
            string _audio,
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
        ) {
        shiny = false;
        colorHasBeenSet = false;
        color = new Vector4(1, 1, 1, 1);
        image = _image;
        audio = _audio;
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
