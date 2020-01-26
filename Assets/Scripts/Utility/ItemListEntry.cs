using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemList {
    public string
        name = "Item List",
        savePath;

    public int
        check = 9;

    public CheckType
        type;

    public Item[]
        Items;

    public void ToJson(string path, bool overwrite = false) {
        string finalPath = path;
        if (!overwrite) {
            finalPath = PokedexManager.ValidatePath(path);
        }
        savePath = finalPath;
        string data = JsonUtility.ToJson(this, true);
        File.WriteAllText(Path.Combine(PokedexManager.dataPath, savePath), data);
    }

    public static ItemList FromJson(string path) {
        string data = File.ReadAllText(path);
        ItemList list = new ItemList();
        list = JsonUtility.FromJson<ItemList>(data);
        list.Items = JsonHelper.FromJson<Item>(data);
        return list;
    }
}

public class ItemListEntry : MonoBehaviour {
    public ItemList
        itemList;

    public Image
        image;

    public Text
        nameField,
        checkField;

    public void SetFields() {
        if (itemList != null) {
            switch (itemList.type) {
                case CheckType.perception:
                    image.sprite = Resources.Load<Sprite>("Icons/Items/Wise_Glasses");
                    break;
                case CheckType.survival:
                    image.sprite = Resources.Load<Sprite>("Icons/Items/Explorer_Kit");
                    break;
                case CheckType.intuition:
                    image.sprite = Resources.Load<Sprite>("Icons/Items/Old_Sea_Map");
                    break;
                case CheckType.techEdu:
                    image.sprite = Resources.Load<Sprite>("Icons/Items/Poke_Radar");
                    break;
            }
            nameField.text = itemList.name;
            checkField.text = itemList.check.ToString() + "+";
        }
    }

    public void OnSelected() {
        PokedexManager.currentItemList = this.gameObject;
        ForageController forage = GameObject.Find("Scroll View").GetComponent<ForageController>();
        forage.OnSelected();
    }
}

[Serializable]
public enum CheckType {
    perception,
    intuition,
    survival,
    techEdu
}
