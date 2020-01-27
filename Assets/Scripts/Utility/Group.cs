using UnityEngine;
using UnityEngine.UI;

public class Group : MonoBehaviour {
    public string title;
    public bool allowed;
    public Toggle toggle;
    public Text text;

    public Group(string _title, bool _allowed) {
        title = _title;
        allowed = _allowed;
    }

    public void SetFields() {
        text.text = title;
        toggle.isOn = allowed;
    }

    public void OnSelected() {
        allowed = toggle.isOn;
    }
}
