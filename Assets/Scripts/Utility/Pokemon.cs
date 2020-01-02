using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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
        nickname,
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
        loyalty,
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

    public void SetBaseRelations() {
        baseRelations = new BaseRelations[] {
            new BaseRelations("HP", hp),
            new BaseRelations("ATK", atk),
            new BaseRelations("DEF", def),
            new BaseRelations("SPATK", spatk),
            new BaseRelations("SPDEF", spdef),
            new BaseRelations("SPD", spd),
        };

        baseRelations = baseRelations.OrderBy(x => x.value).ToArray();

        for (int i = 0; i < baseRelations.Length; i++) {
            baseRelations[i].position = i;
        }
    }

    public void GetShiny(bool alwaysShiny) {
        int shinyChance = UnityEngine.Random.Range(0, 8192);
        if (alwaysShiny) { shinyChance = 0; }
        shiny = shinyChance == 0 ? true : false;

        if (shiny) {
            PokemonType shinyType = PokedexManager.types[UnityEngine.Random.Range(0, PokedexManager.types.Length)];
            string[] types = type.Split('/');
            if (shinyType.typeName == types[0].Trim()) {
                type = types[0].Trim();
            } else {
                type = types[0].Trim() + " / " + shinyType.typeName;
            }

            List<TM> typeTMs = new List<TM>();
            foreach (var item in PokedexManager.TMs) {
                if (shinyType.typeName == item.type) {
                    typeTMs.Add(item);
                }
            }
            List<Move> shinyMoves = knownMoveList.ToList();
            TM chosenTM = typeTMs[UnityEngine.Random.Range(0, typeTMs.Count())];
            Move chosenMove = new Move(chosenTM.name, 1, chosenTM.type);
            shinyMoves.Add(chosenMove);
            if (shinyMoves.Count() > 6) {
                shinyMoves.RemoveAt(0);
            }
            knownMoveList = shinyMoves.ToArray();
        }
    }

    public void GetGender() {
        string[] genderSplit = gender.Split(' ');
        if (genderSplit.Count() == 5) {
            float male = float.Parse(genderSplit[0].Replace("%", ""));

            float chance = UnityEngine.Random.Range(0, 101);
            if (chance > male) {
                gender = "Female";
            } else {
                gender = "Male";
            }
        }
    }

    public void GetAbilities() {
        int choice = UnityEngine.Random.Range(0, basicAbilities.Length);
        basicAbility = basicAbilities.Length == 0 ? "None" : basicAbilities[choice];
        if (level >= 20) {
            choice = UnityEngine.Random.Range(0, advancedAbilities.Length);
            advancedAbility = advancedAbilities.Length == 0 ? "None" : advancedAbilities[choice];
            if (level >= 40) {
                choice = UnityEngine.Random.Range(0, highAbilities.Length);
                highAbility = highAbilities.Length == 0 ? "None" : highAbilities[choice];
            } else {
                highAbility = "None";
            }
        } else {
            advancedAbility = "None";
            highAbility = "None";
        }
    }

    public void GetNature(string natureString) {
        Nature newNature;
        if (natureString != "Any Nature") {
            newNature = PokedexManager.natures.First(x => x.name == natureString);
        } else {
            newNature = PokedexManager.natures[UnityEngine.Random.Range(0, PokedexManager.natures.Length)];
        }
        nature = newNature;
        ModifyBaseStatForNature();
        ModifyBaseStatForNature(true);
    }

    private void ModifyBaseStatForNature(bool decrease = false) {
        int value = 2;
        if (decrease) {
            value *= -1;
            string name = nature.down.ToLower();
        } else {
            string name = nature.up.ToLower();
        }
        if (nickname == "hp") {
            hp += value / 2;
        } else if (nickname == "atk") {
            atk += value;
        } else if (nickname == "def") {
            def += value;
        } else if (nickname == "spatk") {
            spatk += value;
        } else if (nickname == "spdef") {
            spdef += value;
        } else if (nickname == "spd") {
            spd += value;
        }
    }

    public void LevelPokemon(int newLevel) {
        level = newLevel;
        int points = 10 + (level - 1);

        for (int i = 0; i < points; i++) {
            int stat = UnityEngine.Random.Range(0, 6);
            if (stat < 5) {
                if (baseRelations[stat].value >= baseRelations[stat + 1].value) {
                    AdjustStatByName(baseRelations[stat].name);
                } else {
                    i--;
                }
            } else {
                AdjustStatByName(baseRelations[stat].name);
            }
        }
    }

    private void AdjustStatByName(string name) {
        if (name == "HP") {
            hpLevel++;
        } else if (name == "ATK") {
            atkLevel++;
        } else if (name == "DEF") {
            defLevel++;
        } else if (name == "SPATK") {
            spatkLevel++;
        } else if (name == "SPDEF") {
            spdefLevel++;
        } else if (name == "SPD") {
            spdLevel++;
        } else {
            Debug.LogError("ATTRIBUTE NAME NOT FOUND: " + name);
        }
    }

    public void GetMoves() {
        List<Move> newMoveList = new List<Move>();
        List<Move> newKnownMoveList = new List<Move>();
        foreach (string move in moves) {
            try {
                string[] moveSplit = move.Split(new string[] { " - " }, StringSplitOptions.None);
                string type = moveSplit[moveSplit.Length - 1].Trim();
                string[] levelAndName = moveSplit[0].Split(' ');
                int level = int.Parse(levelAndName[0].Trim());
                string name = "";
                for (int i = 1; i < levelAndName.Length; i++) {
                    name += " " + levelAndName[i];
                }
                name = name.Trim();
                newMoveList.Add(new Move(name, level, type));
            } catch {
                Debug.Log(move);
            }
        }
        movesList = newMoveList.ToArray();
        movesList = movesList.OrderBy(x => x.level).ToArray();
        foreach (Move move in movesList) {
            if (level >= move.level) {
                newKnownMoveList.Add(move);
            }
            if (newKnownMoveList.Count() > 7) {
                newKnownMoveList.RemoveAt(0);
            }
        }
        knownMoveList = newKnownMoveList.ToArray();
        knownMoveList = knownMoveList.OrderBy(x => x.level).ToArray();
    }

    public void GetHeldItem(bool alwaysHoldItem) {
        int chance = UnityEngine.Random.Range(0, 10);
        if (alwaysHoldItem) { chance = 0; }
        if (chance == 0) {
            chance = UnityEngine.Random.Range(0, PokedexManager.items.Length);
            heldItem = PokedexManager.items[chance];
        }
    }

    public void GetCaptureRate() {
        captureRate = 100 - (level * 2);

        float health = (float)currentHealth / (float)maxHealth;
        Debug.Log(health);
        if (health > 0.75) {
            captureRate += -30;
        } else if (health > 0.5) {
            captureRate += -15;
        } else if (health > 0.25) {
            captureRate += 0;
        } else {
            captureRate += 15;
        }

        string finalStage = evolutions[evolutions.Length - 1][0].ToString();
        int maxStage = int.Parse(finalStage);
        if (maxStage - stage == 0) {
            captureRate += -10;
        } else if (maxStage - stage == 1) {
            captureRate += 0;
        } else if (maxStage - stage == 2) {
            captureRate += 10;
        }

        if (shiny) { captureRate += -10; }
        if (legendary) { captureRate += -30; }

        if (burned) { captureRate += 10; }
        if (frozen) { captureRate += 10; }
        if (paralyzed) { captureRate += 10; }
        if (poisoned) { captureRate += 10; }
        if (stuck) { captureRate += 10; }

        if (confused) { captureRate += 5; }
        if (cursed) { captureRate += 5; }
        if (disabled) { captureRate += 5; }
        if (enraged) { captureRate += 5; }
        if (flinched) { captureRate += 5; }
        if (infatuated) { captureRate += 5; }
        if (asleep) { captureRate += 5; }
        if (suppressed) { captureRate += 5; }
        if (slowed) { captureRate += 5; }

        captureRate += 5 * injuries;
    }

    public void GetCries() {
        if (cryAudio == null) {
            string cryLocation = Path.Combine(Application.streamingAssetsPath, "Cries/" + cry + ".ogg");
            if (!File.Exists(cryLocation)) {
                Debug.LogError("Cry could not be found: " + cryLocation);
            } else {
                PokedexManager.manager.LoadClip("file:///" + cryLocation, this);
            }
        }
    }

    public void GetSkills() {
        List<string> skillsToAdjust = new List<string>();
        skillsToAdjust.Add("Athl");
        skillsToAdjust.Add("Acro");
        skillsToAdjust.Add("Combat");
        skillsToAdjust.Add("Focus");
        skillsToAdjust.Add("Percep");
        skillsToAdjust.Add("Stealth");
        skillsToAdjust.Add("TechEdu");

        int numberOfSkillsToSet = UnityEngine.Random.Range(0, 4);
        Debug.Log("Number of Skills to Set: " + numberOfSkillsToSet.ToString());

        bool positive = false;
        for (int i = 0; i < numberOfSkillsToSet * 2; i++) {
            positive = !positive;
            int skillIndex = UnityEngine.Random.Range(0, skillsToAdjust.Count());
            switch (skillsToAdjust[skillIndex]) {
                case "Athl":
                    if (athleticsDie < 2 && !positive) { positive = !positive; i--; continue; }
                    athleticsDie += positive ? 1 : -1;
                    skillsToAdjust.Remove("Athl");
                    continue;
                case "Acro":
                    if (acrobaticsDie < 2 && !positive) { positive = !positive; i--; continue; }
                    acrobaticsDie += positive ? 1 : -1;
                    skillsToAdjust.Remove("Acro");
                    continue;
                case "Combat":
                    if (combatDie < 2 && !positive) { positive = !positive; i--; continue; }
                    combatDie += positive ? 1 : -1;
                    skillsToAdjust.Remove("Combat");
                    continue;
                case "Focus":
                    if (focusDie < 2 && !positive) { positive = !positive; i--; continue; }
                    focusDie += positive ? 1 : -1;
                    skillsToAdjust.Remove("Focus");
                    continue;
                case "Percep":
                    if (perceptionDie < 2 && !positive) { positive = !positive; i--; continue; }
                    perceptionDie += positive ? 1 : -1;
                    skillsToAdjust.Remove("Percep");
                    continue;
                case "Stealth":
                    if (stealthDie < 2 && !positive) { positive = !positive; i--; continue; }
                    stealthDie += positive ? 1 : -1;
                    skillsToAdjust.Remove("Stealth");
                    continue;
                case "TechEdu":
                    if (techEduDie == 0) { positive = !positive; i--; continue; }
                    if (techEduDie < 2 && !positive) { positive = !positive; i--; continue; }
                    techEduDie += positive ? 1 : -1;
                    skillsToAdjust.Remove("TechEdu");
                    continue;
            }
        }
    }

    public string CheckForNickname() {
        return nickname == null || nickname == "" ? species == null || species == "" ? "???" : species : nickname;
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
