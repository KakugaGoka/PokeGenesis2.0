using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleParent : MonoBehaviour
{
    public Toggle[] children;

    public void ToggleChildren(bool setting) {
        Toggle parent = this.gameObject.GetComponent<Toggle>();

        foreach (Toggle child in children) {
            if (parent.isOn == setting) {
                child.isOn = setting;
                child.interactable = false;
            } else {
                child.interactable = true;
            }
        }
    }
}
