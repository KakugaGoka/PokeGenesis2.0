using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveEntry : MonoBehaviour
{
    public Tooltip
        tooltip;

    public Image 
        typeBanner;

    public Text 
        typeText,
        moveText,
        classText,
        dbText,
        acText,
        freqText;

    [HideInInspector]
    public Move move;

    public void SetFields(string title = "") {
        if (move != null) {
            typeBanner.color = move.type.GetColor();
            typeText.text = move.type.typeName;
            moveText.text = title == "" ? move.name : title;
            classText.text = move.damageClass;
            int db = move.db;
            if (PokedexManager.currentPokemon.type.Contains(move.type.typeName)) {
                db++;
            }
            dbText.text = "DB " + db.ToString();
            acText.text = "AC " + move.ac.ToString();
            freqText.text = move.freq;
        }
    }

    public void OnSelected() {
        tooltip.ShowMoveToolTipFromPrefab(move);
    }
}
