using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item {
    public Sprite
        sprite;

    public string
        name,
        desc,
        image,
        group;

    public int
        tier;
}

public class ItemEntry : MonoBehaviour {
    public Tooltip
        tooltip;

    public Image
        image;

    public Text
        nameField,
        groupField;

    public Item
        item;

    public ItemListEntry
        parent;

    public void SetFields() {
        if (item != null) {
            nameField.text = item.name;
            groupField.text = item.group;
            image.sprite = PokedexManager.LoadSprite("Icons/Items/" + item.image);
        }
    }

    public void OnSelected() {
        tooltip.ShowItemToolTipFromPrefab(item);
    }

    public void DestroySelf() {
        PokedexManager.currentItem = this.gameObject;
        PokedexManager.manager.CreateConfirmationDialog("Are you sure you wish to delete " + item.name + " from this list?", ConfirmationType.itemDelete);
    }
}
