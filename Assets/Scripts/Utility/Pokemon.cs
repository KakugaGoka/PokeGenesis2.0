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
public class Mega {
    public Sprite
        sprite;

    public string
        name,
        type,
        ability,
        image;

    public int
        hp,
        atk,
        def,
        spatk,
        spdef,
        spd;

    public bool
        inMegaForm;
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

    public Mega
        mega,
        altMega;

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
        currentAbilites,
        moves;

    public Nature
        nature;

    public void ToJson(string path, bool overwrite = false) {
        string data = JsonUtility.ToJson(this, true);
            string finalPath = path;
        if (!overwrite) {
            finalPath = ValidatePath(path);
        }
        this.savePath = finalPath;
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, finalPath), data);
    }

    public static Pokemon FromJson(string path) {
        string data = File.ReadAllText(path);
        return JsonUtility.FromJson<Pokemon>(data);
    }

    private string ValidatePath(string path, int iteration = 0) {
        string newPath = path;
        if (File.Exists(Path.Combine(Application.streamingAssetsPath, path))) {
            bool match = Regex.IsMatch(newPath, @"_[\d-]*[\d-]");
            if (match == true) {
                newPath = Regex.Replace(newPath, @"_[\d-]*[\d-]", "");
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
        int position = 0;
        for (int i = 0; i < baseRelations.Length; i++) {
            if (i == 0) {
                baseRelations[i].position = position;
            } else if (baseRelations[i].value > baseRelations[i - 1].value) {
                position++;
                baseRelations[i].position = position;
            } else {
                baseRelations[i].position = position;
            }
        }
    }

    public void LevelPokemon(int newLevel) {
        level = newLevel;
        int points = 10 + (level - 1);

        for (int i = 0; i < points; i++) {
            int stat = UnityEngine.Random.Range(0, 6);
            BaseRelations statToAdvance = baseRelations[stat];
            if (statToAdvance.position < baseRelations[baseRelations.Length - 1].position) {
                BaseRelations[] relationCheckArray = baseRelations.Where(x => x.position == baseRelations[stat].position + 1).ToArray();
                BaseRelations relationCheck = relationCheckArray.OrderByDescending(x => x.value).First();
                if (statToAdvance.value < relationCheck.value - 1) {
                    AdjustStatByName(statToAdvance);
                } else {
                    i--;
                }
            } else {
                AdjustStatByName(statToAdvance);
            }
        }
    }

    private void AdjustStatByName(BaseRelations baseRelation) {
        baseRelation.value++;
        if (baseRelation.name == "HP") {
            hpLevel++;
        } else if (baseRelation.name == "ATK") {
            atkLevel++;
        } else if (baseRelation.name == "DEF") {
            defLevel++;
        } else if (baseRelation.name == "SPATK") {
            spatkLevel++;
        } else if (baseRelation.name == "SPDEF") {
            spdefLevel++;
        } else if (baseRelation.name == "SPD") {
            spdLevel++;
        } else {
            Debug.LogError("ATTRIBUTE NAME NOT FOUND: " + baseRelation.name);
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
        List<string> newCurrentAbilities = currentAbilites.ToList();
        int choice = UnityEngine.Random.Range(0, basicAbilities.Length);
        if (basicAbilities.Length > 0) {
            basicAbility = basicAbilities[choice];
            newCurrentAbilities.Add(basicAbility);
        }
        if (level >= 20) {
            choice = UnityEngine.Random.Range(0, advancedAbilities.Length);
            if (advancedAbilities.Length > 0) {
                advancedAbility = advancedAbilities[choice];
                newCurrentAbilities.Add(advancedAbility);
            }
            if (level >= 40) {
                choice = UnityEngine.Random.Range(0, highAbilities.Length);
                if (highAbilities.Length > 0) {
                    highAbility = highAbilities[choice];
                    newCurrentAbilities.Add(highAbility);
                }
            } 
        }
        currentAbilites = newCurrentAbilities.ToArray();
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
    }

    private void ModifyBaseStatForNature() {
        int value = 2;
        if (nature.up == "hp") {
            hp += value / 2;
        } else if (nature.up == "atk") {
            atk += value;
        } else if (nature.up == "def") {
            def += value;
        } else if (nature.up == "spatk") {
            spatk += value;
        } else if (nature.up == "spdef") {
            spdef += value;
        } else if (nature.up == "spd") {
            spd += value;
        }
        value = -2;
        if (nature.down == "hp") {
            hp += value / 2;
        } else if (nature.down == "atk") {
            atk += value;
        } else if (nature.down == "def") {
            def += value;
        } else if (nature.down == "spatk") {
            spatk += value;
        } else if (nature.down == "spdef") {
            spdef += value;
        } else if (nature.down == "spd") {
            spd += value;
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
        List<int[]> skillsToAdjust = new List<int[]>();
        skillsToAdjust.Add(new int[] { 0, athleticsDie });
        skillsToAdjust.Add(new int[] { 1, acrobaticsDie });
        skillsToAdjust.Add(new int[] { 2, combatDie });
        skillsToAdjust.Add(new int[] { 3, focusDie });
        skillsToAdjust.Add(new int[] { 4, perceptionDie });
        skillsToAdjust.Add(new int[] { 5, stealthDie });
        skillsToAdjust.Add(new int[] { 6, techEduDie });

        int count = UnityEngine.Random.Range(0, 4);
        if (count == 0 ) { return; }

        for (int i = 0; i < count; i++) {
            Debug.Log("Skills in list to edit: " + skillsToAdjust.Count());
            int index0 = UnityEngine.Random.Range(0, skillsToAdjust.Count());
            int index1 = UnityEngine.Random.Range(0, skillsToAdjust.Count());
            do {
                index1 = UnityEngine.Random.Range(0, skillsToAdjust.Count());
            } while (index0 == index1);

            int[] one = skillsToAdjust[index0];
            int[] two = skillsToAdjust[index1];

            if (one[1] < 1) {
                i--;
                continue;
            }

            SetSkill(false, one);
            SetSkill(true, two);
            skillsToAdjust.Remove(one);
            skillsToAdjust.Remove(two);
        }
    }

    public void SetSkill(bool positive, int[] skill) {
        if (skill[0] == 0) {
            athleticsDie += positive ? 1 : -1;
        } else if (skill[0] == 1) {
            acrobaticsDie += positive ? 1 : -1;
        } else if (skill[0] == 2) {
            combatDie += positive ? 1 : -1;
        } else if (skill[0] == 3) {
            focusDie += positive ? 1 : -1;
        } else if (skill[0] == 4) {
            perceptionDie += positive ? 1 : -1;
        } else if (skill[0] == 5) {
            stealthDie += positive ? 1 : -1;
        } else if (skill[0] == 6) {
            techEduDie += positive ? 1 : -1;
        }
    }

    public string CheckForNickname() {
        if (String.IsNullOrEmpty(nickname)) {
            if (mega.inMegaForm) {
                return mega.name;
            } else if (altMega.inMegaForm) {
                return altMega.name;
            } else {
                return species;
            }
        } else {
            return nickname;
        }
    }

    public string GetCurrentType() {
        if (mega.inMegaForm) {
            return mega.type;
        } else if (altMega.inMegaForm) {
            return altMega.type;
        } else {
            return type;
        }
    }

    public bool HasMega() {
        if (String.IsNullOrWhiteSpace(mega.name)) {
            return false;
        } else {
            return true;
        }
    }

    public bool HasAltMega() {
        if (String.IsNullOrWhiteSpace(altMega.name)) {
            return false;
        } else {
            return true;
        }
    }

    public void ApplyMega() {
        PokedexEntry entry = PokedexManager.currentEntry.GetComponent<PokedexEntry>();
        if (altMega.inMegaForm) {
            UnapplyAltMega();
        }
        mega.sprite = PokedexManager.LoadSprite("PokemonIcons/" + mega.image);
        hpLevel += mega.hp;
        atkLevel += mega.atk;
        defLevel += mega.def;
        spatkLevel += mega.spatk;
        spdefLevel += mega.spdef;
        spdLevel += mega.spd;
        List<string> newCurrentAbilites = currentAbilites.ToList();
        newCurrentAbilites.Add(mega.ability);
        currentAbilites = newCurrentAbilites.ToArray();
        mega.inMegaForm = true;
        entry.sprite.sprite = mega.sprite;
        entry.species.text = CheckForNickname();
    }

    public void UnapplyMega() {
        PokedexEntry entry = PokedexManager.currentEntry.GetComponent<PokedexEntry>();
        sprite = PokedexManager.LoadSprite("PokemonIcons/" + image);
        hpLevel -= mega.hp;
        atkLevel -= mega.atk;
        defLevel -= mega.def;
        spatkLevel -= mega.spatk;
        spdefLevel -= mega.spdef;
        spdLevel -= mega.spd;
        List<string> newCurrentAbilites = currentAbilites.ToList();
        newCurrentAbilites.Remove(mega.ability);
        currentAbilites = newCurrentAbilites.ToArray();
        mega.inMegaForm = false;
        entry.sprite.sprite = sprite;
        entry.species.text = CheckForNickname();
    }

    public void ApplyAltMega() {
        PokedexEntry entry = PokedexManager.currentEntry.GetComponent<PokedexEntry>();
        if (mega.inMegaForm) {
            UnapplyMega();
        }
        altMega.sprite = PokedexManager.LoadSprite("PokemonIcons/" + altMega.image);
        hpLevel += altMega.hp;
        atkLevel += altMega.atk;
        defLevel += altMega.def;
        spatkLevel += altMega.spatk;
        spdefLevel += altMega.spdef;
        spdLevel += altMega.spd;
        List<string> newCurrentAbilites = currentAbilites.ToList();
        newCurrentAbilites.Add(altMega.ability);
        currentAbilites = newCurrentAbilites.ToArray();
        altMega.inMegaForm = true;
        entry.sprite.sprite = altMega.sprite;
        entry.species.text = CheckForNickname();
    }

    public void UnapplyAltMega() {
        PokedexEntry entry = PokedexManager.currentEntry.GetComponent<PokedexEntry>();
        sprite = PokedexManager.LoadSprite("PokemonIcons/" + image);
        hpLevel -= altMega.hp;
        atkLevel -= altMega.atk;
        defLevel -= altMega.def;
        spatkLevel -= altMega.spatk;
        spdefLevel -= altMega.spdef;
        spdLevel -= altMega.spd;
        List<string> newCurrentAbilites = currentAbilites.ToList();
        newCurrentAbilites.Remove(altMega.ability);
        currentAbilites = newCurrentAbilites.ToArray();
        altMega.inMegaForm = false;
        entry.sprite.sprite = sprite;
        entry.species.text = CheckForNickname();
    }
}
/*
Mega Blastoise
Giga Charizard
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
