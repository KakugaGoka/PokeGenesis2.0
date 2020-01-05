using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputField : UnityEngine.UI.InputField
{
    private bool justSelected = false;

    public override void OnSelect(BaseEventData eventData) {
        base.OnSelect(eventData);
        justSelected = true;
    }

    protected override void LateUpdate() {
        base.LateUpdate();
        if (justSelected) {
            MoveTextEnd(false);
            justSelected = false;
        }
    }
}
