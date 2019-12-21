using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleParent : MonoBehaviour
{
    public Toggle[] children;

    public void ToggleChildren() {
        Toggle parent = this.gameObject.GetComponent<Toggle>();

        foreach (Toggle child in children) {
            if (!parent.isOn) {
                child.isOn = false;
                child.interactable = false;
            } else {
                child.interactable = true;
            }
        }
    }
}
