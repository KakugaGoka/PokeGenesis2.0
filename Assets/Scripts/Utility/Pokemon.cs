using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        gigaImage,
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
        highAbility,
        notes;

    public int
        number,
        level,
        dynamaxLevel,
        tutorPoints,
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
        vulnerable,
        isDynamax;

    public string[]
        evolutions,
        capabilities,
        basicAbilities,
        advancedAbilities,
        highAbilities,
        currentAbilities,
        moves,
        features,
        edges;

    public Nature
        nature;

    public void ToJson(string path, bool overwrite = false) {
        string finalPath = path;
        if (!overwrite) {
            finalPath = ValidatePath(path);
        }
        savePath = finalPath;
        string data = JsonUtility.ToJson(this, true);
        File.WriteAllText(Path.Combine(PokedexManager.dataPath, savePath), data);
    }

    public static Pokemon FromJson(string path) {
        string data = File.ReadAllText(path);
        return JsonUtility.FromJson<Pokemon>(data);
    }

    private string ValidatePath(string path, int iteration = 0) {
        string newPath = path;
        if (File.Exists(Path.Combine(PokedexManager.dataPath, path))) {
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
        List<string> newCurrentAbilities = currentAbilities.ToList();
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
        currentAbilities = newCurrentAbilities.ToArray();
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

    public void ModifyBaseStatForNature(bool remove = false) {
        int value = remove ? -2 : 2;
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
        value *= -1;
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
        if (currentHealth <= 0) {
            captureRate = 0;
            return;
        }

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
            string cryLocation = Path.Combine(PokedexManager.dataPath, "Cries/" + cry + ".ogg");
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

    public void SetTutorPoints() {
        tutorPoints = 1;
        tutorPoints += level / 5;
    }

    public string CheckForNickname() {
        if (String.IsNullOrEmpty(nickname)) {
            if (mega.inMegaForm) {
                return mega.name;
            } else if (altMega.inMegaForm) {
                return altMega.name;
            } else if (isDynamax) {
                if (String.IsNullOrWhiteSpace(gigaImage)) {
                    return "Dynamax " + species;
                } else {
                    return "Gigantamax " + species;
                }
            } else {
                return species;
            }
        } else {
            return nickname;
        }
    }

    public void SetNickname(string text) {
        if (text != species &&
                text != "Dynamax " + species &&
                text != "Gigantamax " + species &&
                text != mega.name &&
                text != altMega.name &&
                !String.IsNullOrWhiteSpace(text)) {
            nickname = text;
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

    public int GetMaxHealth() {
        int health = level + ((hp + hpLevel) * 3) + 10;
        return health;
    }

    public int GetDynaMaxHealth() {
        int health = GetMaxHealth();
        health = (int)(health * Mathf.Clamp(1.5f + (dynamaxLevel * 0.05f), 1.5f, 2.0f)); 
        return health;
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
        if (isDynamax) {
            UnapplyDynamax();
        }
        mega.sprite = PokedexManager.LoadSprite("Icons/Pokemon/" + mega.image);
        hpLevel += mega.hp;
        atkLevel += mega.atk;
        defLevel += mega.def;
        spatkLevel += mega.spatk;
        spdefLevel += mega.spdef;
        spdLevel += mega.spd;
        List<string> newCurrentAbilites = currentAbilities.ToList();
        newCurrentAbilites.Add(mega.ability);
        currentAbilities = newCurrentAbilites.ToArray();
        mega.inMegaForm = true;
        entry.sprite.sprite = mega.sprite;
        entry.species.text = CheckForNickname();
    }

    public void UnapplyMega() {
        PokedexEntry entry = PokedexManager.currentEntry.GetComponent<PokedexEntry>();
        sprite = PokedexManager.LoadSprite("Icons/Pokemon/" + image);
        hpLevel -= mega.hp;
        atkLevel -= mega.atk;
        defLevel -= mega.def;
        spatkLevel -= mega.spatk;
        spdefLevel -= mega.spdef;
        spdLevel -= mega.spd;
        List<string> newCurrentAbilites = currentAbilities.ToList();
        newCurrentAbilites.Remove(mega.ability);
        currentAbilities = newCurrentAbilites.ToArray();
        mega.inMegaForm = false;
        entry.sprite.sprite = sprite;
        entry.species.text = CheckForNickname();
    }

    public void ApplyAltMega() {
        PokedexEntry entry = PokedexManager.currentEntry.GetComponent<PokedexEntry>();
        if (mega.inMegaForm) {
            UnapplyMega();
        }
        if (isDynamax) {
            UnapplyDynamax();
        }
        altMega.sprite = PokedexManager.LoadSprite("Icons/Pokemon/" + altMega.image);
        hpLevel += altMega.hp;
        atkLevel += altMega.atk;
        defLevel += altMega.def;
        spatkLevel += altMega.spatk;
        spdefLevel += altMega.spdef;
        spdLevel += altMega.spd;
        List<string> newCurrentAbilites = currentAbilities.ToList();
        newCurrentAbilites.Add(altMega.ability);
        currentAbilities = newCurrentAbilites.ToArray();
        altMega.inMegaForm = true;
        entry.sprite.sprite = altMega.sprite;
        entry.species.text = CheckForNickname();
    }

    public void UnapplyAltMega() {
        PokedexEntry entry = PokedexManager.currentEntry.GetComponent<PokedexEntry>();
        sprite = PokedexManager.LoadSprite("Icons/Pokemon/" + image);
        hpLevel -= altMega.hp;
        atkLevel -= altMega.atk;
        defLevel -= altMega.def;
        spatkLevel -= altMega.spatk;
        spdefLevel -= altMega.spdef;
        spdLevel -= altMega.spd;
        List<string> newCurrentAbilites = currentAbilities.ToList();
        newCurrentAbilites.Remove(altMega.ability);
        currentAbilities = newCurrentAbilites.ToArray();
        altMega.inMegaForm = false;
        entry.sprite.sprite = sprite;
        entry.species.text = CheckForNickname();
    }

    public void ApplyDynamax() {
        PokedexEntry entry = PokedexManager.currentEntry.GetComponent<PokedexEntry>();
        if (mega.inMegaForm) {
            UnapplyMega();
        }
        if (altMega.inMegaForm) {
            UnapplyAltMega();
        }
        if (!String.IsNullOrWhiteSpace(gigaImage)) {
            sprite = PokedexManager.LoadSprite("Icons/Pokemon/" + gigaImage);
        }
        float hpMultiplier = (float)currentHealth / (float)maxHealth;
        maxHealth = GetDynaMaxHealth();
        currentHealth = Mathf.RoundToInt(maxHealth * hpMultiplier);
        isDynamax = true;
        entry.sprite.sprite = sprite;
        entry.species.text = CheckForNickname();
        entry.dynaBack.SetActive(true);
        entry.dynaFront.SetActive(true);
    }

    public void UnapplyDynamax() {
        PokedexEntry entry = PokedexManager.currentEntry.GetComponent<PokedexEntry>();
        sprite = PokedexManager.LoadSprite("Icons/Pokemon/" + image);
        float hpMultiplier = (float)currentHealth / (float)maxHealth;
        maxHealth = GetMaxHealth();
        currentHealth = Mathf.RoundToInt(maxHealth * hpMultiplier);
        isDynamax = false;
        entry.sprite.sprite = sprite;
        entry.species.text = CheckForNickname();
        entry.dynaBack.SetActive(false);
        entry.dynaFront.SetActive(false);
    }

    public string[] SkillsToArray() {
        string athlBase = "Athl " + athleticsDie + "d6";
        if (athleticsBonus != 0) { athlBase += "+" + athleticsBonus; }
        string acroBase = "Acro " + acrobaticsDie + "d6";
        if (acrobaticsBonus != 0) { acroBase += "+" + acrobaticsBonus; }
        string combatBase = "Combat " + combatDie + "d6";
        if (combatBonus != 0) { combatBase += "+" + combatBonus; }
        string focusBase = "Focus " + focusDie + "d6";
        if (focusBonus != 0) { focusBase += "+" + focusBonus; }
        string percepBase = "Percep " + perceptionDie + "d6";
        if (perceptionBonus != 0) { percepBase += "+" + perceptionBonus; }
        string stealthBase = "Stealth " + stealthDie + "d6";
        if (stealthBonus != 0) { stealthBase += "+" + stealthBonus; }
        string techEduBase = "Edu: Tech " + techEduDie + "d6";
        if (techEduBonus != 0) { techEduBase += "+" + techEduBonus; }
        return new string[] {
            athlBase,
            acroBase,
            combatBase,
            focusBase,
            percepBase,
            stealthBase,
            techEduBase
        };
    }

    public string[] AbilitiesToArray() {
        List<string> infoList = new List<string>();
        foreach (var bAb in basicAbilities) {
            infoList.Add("Basic Ability: " + bAb);
        }
        foreach (var aAb in advancedAbilities) {
            infoList.Add("Adv Ability: " + aAb);
        }
        foreach (var hAb in highAbilities) {
            infoList.Add("High Ability: " + hAb);
        }
        return infoList.ToArray();
    }

    public string[] HabitatsToArray() {
        List<string> habitatList = habitat.Split(',').ToList();
        for (int i = 0; i < habitatList.Count(); i++) {
            habitatList[i] = habitatList[i].Trim();
        }
        return habitatList.ToArray();
    }

    public void ExportToRoll20JSON() {
        string jsonLocation = Path.Combine(PokedexManager.dataPath, "Roll20_Captured");
        if (!Directory.Exists(jsonLocation)) {
            Directory.CreateDirectory(jsonLocation);
        }
        string typeOne;
        string typeTwo;
        if (type.Contains("/")) {
            string[] typesSplit = type.Split('/');
            typeOne = typesSplit[0];
            typeTwo = typesSplit[1];
        } else {
            typeOne = type;
            typeTwo = "None";
        }

        string jsonHeight;
        string jsonWeight;
        string[] hSplit = size.Split('(');
        string[] wSplit = weight.Split('(');
        jsonHeight = hSplit[1].Remove(hSplit[1].Length - 1);
        jsonWeight = wSplit[1].Remove(wSplit[1].Length - 1);

        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        using (JsonWriter writer = new JsonTextWriter(sw)) {
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("CharType");
            writer.WriteValue(0);
            writer.WritePropertyName("nickname");
            writer.WriteValue(nickname);
            writer.WritePropertyName("species");
            writer.WriteValue(species);
            writer.WritePropertyName("type1");
            writer.WriteValue(typeOne);
            writer.WritePropertyName("type2");
            writer.WriteValue(typeTwo);
            writer.WritePropertyName("Level");
            writer.WriteValue(level);
            writer.WritePropertyName("HeldItem");
            writer.WriteValue(heldItem.name);
            writer.WritePropertyName("Gender");
            writer.WriteValue(gender);
            writer.WritePropertyName("Nature");
            writer.WriteValue(nature.name);
            writer.WritePropertyName("Height");
            writer.WriteValue(jsonHeight);
            writer.WritePropertyName("WeightClass");
            writer.WriteValue(int.Parse(jsonWeight));
            writer.WritePropertyName("base_HP");
            writer.WriteValue(hp);
            writer.WritePropertyName("base_ATK");
            writer.WriteValue(atk);
            writer.WritePropertyName("base_DEF");
            writer.WriteValue(def);
            writer.WritePropertyName("base_SPATK");
            writer.WriteValue(spatk);
            writer.WritePropertyName("base_SPDEF");
            writer.WriteValue(spdef);
            writer.WritePropertyName("base_SPEED");
            writer.WriteValue(spd);
            writer.WritePropertyName("HP");
            writer.WriteValue(hpLevel);
            writer.WritePropertyName("ATK");
            writer.WriteValue(atkLevel);
            writer.WritePropertyName("DEF");
            writer.WriteValue(defLevel);
            writer.WritePropertyName("SPATK");
            writer.WriteValue(spatkLevel);
            writer.WritePropertyName("SPDEF");
            writer.WriteValue(spdefLevel);
            writer.WritePropertyName("SPEED");
            writer.WriteValue(spdLevel);
            writer.WritePropertyName("Capabilities");
            writer.WriteStartObject();
            string startNaturewalk = "Error";
            foreach (var cap in capabilities) {
                if (cap.Contains("Naturewalk")) {
                    StringBuilder build = new StringBuilder();
                    List<string> capWalks = new List<string>();
                    string[] start1 = cap.Split(')');
                    string[] start2 = start1[0].Split('(');
                    string[] start3 = start2[1].Split(',');
                    foreach (string w in start3) {
                        build.Clear();
                        build.Append("Naturewalk (");
                        build.Append(w.Trim());
                        build.Append(")");
                        capWalks.Add(build.ToString());
                    }
                    startNaturewalk = cap;
                    foreach (string v in capWalks) {
                        writer.WritePropertyName(v);
                        writer.WriteValue(true);
                    }
                }
            }
            foreach (string cap in capabilities) {
                if (!cap.Contains(startNaturewalk)) {
                    if (cap.Contains("Overland") || cap.Contains("Swim") || cap.Contains("Power") || cap.Contains("Levitate") || cap.Contains("Burrow") || cap.Contains("Sky")) {
                        string[] capSplit = cap.Split(' ');
                        writer.WritePropertyName(capSplit[0]);
                        writer.WriteValue(Convert.ToInt32(capSplit[1]));
                    } else if (cap.Contains("Jump")) {
                        string[] capSplit = cap.Split(' ');
                        string[] highLow = capSplit[1].Split('/');
                        writer.WritePropertyName("HJ");
                        writer.WriteValue(int.Parse(highLow[0]));
                        writer.WritePropertyName("LJ");
                        writer.WriteValue(int.Parse(highLow[1]));
                    } else {
                        writer.WritePropertyName(cap);
                        writer.WriteValue(true);
                    }
                }
            }
            writer.WriteEndObject();
            writer.WritePropertyName("Athletics");
            writer.WriteValue(athleticsDie);
            writer.WritePropertyName("Acrobatics");
            writer.WriteValue(acrobaticsDie);
            writer.WritePropertyName("Combat");
            writer.WriteValue(combatDie);
            writer.WritePropertyName("Stealth");
            writer.WriteValue(stealthDie);
            writer.WritePropertyName("Perception");
            writer.WriteValue(perceptionDie);
            writer.WritePropertyName("Focus");
            writer.WriteValue(focusDie);
            writer.WritePropertyName("TechnologyEducation");
            writer.WriteValue(techEduDie);
            writer.WritePropertyName("Athletics_bonus");
            writer.WriteValue(athleticsBonus);
            writer.WritePropertyName("Acrobatics_bonus");
            writer.WriteValue(acrobaticsBonus);
            writer.WritePropertyName("Combat_bonus");
            writer.WriteValue(combatBonus);
            writer.WritePropertyName("Stealth_bonus");
            writer.WriteValue(stealthBonus);
            writer.WritePropertyName("Perception_bonus");
            writer.WriteValue(perceptionBonus);
            writer.WritePropertyName("Focus_bonus");
            writer.WriteValue(focusBonus);
            writer.WritePropertyName("TechnologyEducation_bonus");
            writer.WriteValue(techEduBonus);
            writer.WritePropertyName("TutorPoints");
            writer.WriteValue(tutorPoints);
            int placement = 1;
            foreach (Move moveBase in knownMoveList) {
                foreach (Move move in PokedexManager.moves) {
                    if (move.name == moveBase.name) {

                        writer.WritePropertyName("Move" + placement);
                        writer.WriteStartObject();
                        writer.WritePropertyName("Name");
                        writer.WriteValue(move.name);
                        if (move.typeName != null) {
                            writer.WritePropertyName("Type");
                            writer.WriteValue(move.typeName);
                        }
                        if (move.freq != null) {
                            writer.WritePropertyName("Freq");
                            writer.WriteValue(move.freq);
                        }
                        if (move.ac != 0) {
                            writer.WritePropertyName("AC");
                            writer.WriteValue(move.ac);
                        }
                        if (move.db != 0) {
                            writer.WritePropertyName("DB");
                            writer.WriteValue(move.db);
                        }
                        if (move.damageClass != null) {
                            writer.WritePropertyName("DType");
                            writer.WriteValue(move.damageClass);
                        }
                        if (move.range != null) {
                            writer.WritePropertyName("Range");
                            writer.WriteValue(move.range);
                        }
                        if (move.effects != null) {
                            writer.WritePropertyName("Effects");
                            writer.WriteValue(move.effects);
                        }
                        if (move.contestType != null) {
                            writer.WritePropertyName("Contest Type");
                            writer.WriteValue(move.contestType);
                        }
                        if (move.contestEffect != null) {
                            writer.WritePropertyName("Contest Effect");
                            writer.WriteValue(move.contestEffect);
                        }
                        writer.WriteEndObject();
                        placement++;
                        break;
                    }
                }
            }
            placement = 1;
            foreach (string abil in currentAbilities) {
                foreach (var ability in PokedexManager.abilities) {
                    if (ability.name == abil) {
                        writer.WritePropertyName("Ability" + placement);
                        writer.WriteStartObject();
                        writer.WritePropertyName("Name");
                        writer.WriteValue(ability.name);
                        if (ability.freq != null) {
                            writer.WritePropertyName("Freq");
                            writer.WriteValue(ability.freq);
                        }
                        if (ability.target != null) {
                            writer.WritePropertyName("Target");
                            writer.WriteValue(ability.target);
                        }
                        if (ability.trigger != null) {
                            writer.WritePropertyName("Trigger");
                            writer.WriteValue(ability.trigger);
                        }
                        if (ability.effect != null) {
                            writer.WritePropertyName("Info");
                            writer.WriteValue(ability.effect);
                        }
                        writer.WriteEndObject();
                        placement++;
                        break;
                    }
                }
            }
        }
        File.WriteAllText(Path.Combine(PokedexManager.dataPath, "Roll20_" + savePath), sb.ToString());
    }
}