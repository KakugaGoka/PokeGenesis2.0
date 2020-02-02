using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForageController : MonoBehaviour {
    public GameObject
        contentView,
        itemView,
        groupView;

    public int capturedJSONCount = 0;

    private bool
        readyToUpdate;

    private UnityEngine.UI.InputField
        nameField,
        checkField;

    private Slider
        countSlider,
        minTierSlider,
        maxTierSlider;

    private Toggle
        groupToggle;

    private Dropdown
        checkDropdown,
        groupDropdown;

    private GameObject
        itemsPanel,
        searchPanel,
        groupPanel,
        itemsTab,
        searchTab,
        groupTab,
        panelParent;

    private List<Group>
        groupList = new List<Group>();

    void Start() {
        nameField = GameObject.Find("Name Field").GetComponent<UnityEngine.UI.InputField>();
        checkField = GameObject.Find("Check Field").GetComponent<UnityEngine.UI.InputField>();

        checkDropdown = GameObject.Find("Check Type Dropdown").GetComponent<Dropdown>();
        List<Dropdown.OptionData> checkOptions = new List<Dropdown.OptionData>();
        checkOptions.Add(new Dropdown.OptionData("Perception"));
        checkOptions.Add(new Dropdown.OptionData("Intuition"));
        checkOptions.Add(new Dropdown.OptionData("Survival"));
        checkOptions.Add(new Dropdown.OptionData("Tech Education"));
        checkDropdown.ClearOptions();
        checkDropdown.AddOptions(checkOptions);

        countSlider = GameObject.Find("Count Slider").GetComponent<Slider>();
        minTierSlider = GameObject.Find("Min Tier Slider").GetComponent<Slider>();
        maxTierSlider = GameObject.Find("Max Tier Slider").GetComponent<Slider>();

        int maxTier = PokedexManager.items.OrderBy(x => x.tier).ToArray()[PokedexManager.items.Length - 1].tier;

        minTierSlider.maxValue = maxTier;
        maxTierSlider.maxValue = maxTier;

        searchPanel = GameObject.Find("Search Panel");
        itemsPanel = GameObject.Find("Items Panel");
        groupPanel = GameObject.Find("Group Panel");
        panelParent = GameObject.Find("Panels");

        searchTab = GameObject.Find("Search Tab");
        itemsTab = GameObject.Find("Items Tab");
        groupTab = GameObject.Find("Group Tab");

        List<string> groups = new List<string>();
        foreach (Item item in PokedexManager.items) {
            if (!groups.Contains(item.group)) {
                groups.Add(item.group);
            }
        }
        groups = groups.OrderBy(x => x).ToList();

        foreach (string group in groups) {
            GameObject newGroup = Instantiate(PokedexManager.manager.groupPrefab) as GameObject;
            newGroup.transform.parent = groupView.transform;
            newGroup.transform.localScale = Vector3.one;
            Group controller = newGroup.GetComponent<Group>();
            controller.title = group;
            controller.allowed = true;
            controller.SetFields();
            groupList.Add(controller);
        }

#if DEBUG
        // Verify that all items have a description, group, and tier.
        foreach (Item item in PokedexManager.items) {
            if (item.desc == null) {
                Debug.LogError(item.name + " deos not have a description");
            }
            if (item.group == null) {
                Debug.LogError(item.name + " deos not have a group");
            }
            if (item.tier < 1) {
                Debug.LogError(item.name + " deos not have a tier");
            }
        }
#endif
        // Load temp pokemon if they exist. 
        var myFiles = Directory.GetFiles(PokedexManager.dataPath + "/ItemLists/", "*.json", SearchOption.TopDirectoryOnly);
        capturedJSONCount = myFiles.Length;

        foreach (var file in myFiles) {
            ItemList list = ItemList.FromJson(file);
            CreateItemListEntry(list);
        }

        readyToUpdate = true;
    }

    void Update() {
        if (readyToUpdate) {
            if (PokedexManager.currentItemList == null) {
                if (contentView.transform.childCount > 0) {
                    PokedexManager.currentItemList = contentView.transform.GetChild(0).gameObject;
                    OnSelected();
                } else {
                    ClearFields();
                }
            }
        }
    }

    public void OnSelected() {
        PokedexManager.currentItemList.GetComponent<ItemListEntry>().SetFields();
        ItemList itemList = PokedexManager.currentItemList.GetComponent<ItemListEntry>().itemList;

        nameField.text = itemList.name;
        checkField.text = itemList.check.ToString();
        checkDropdown.value = (int)itemList.type;

        for (int i = 0; i < itemView.transform.childCount; i++) {
            Destroy(itemView.transform.GetChild(i).gameObject);
        }
        StopAllCoroutines();
        StartCoroutine(CreateItemEntries(itemList));
    }

    public void ClearFields() {
        StopAllCoroutines();
        nameField.text = "";
        checkField.text = "";

        for (int i = 0; i < itemView.transform.childCount; i++) {
            Destroy(itemView.transform.GetChild(i).gameObject);
        }
    }

    public void SetItemListInfo() {
        ItemList itemList = PokedexManager.currentItemList.GetComponent<ItemListEntry>().itemList;

        try {
            itemList.check = int.Parse(checkField.text);
        } catch {
            PokedexManager.manager.CreateWarningDialog("The Check field needs to be a whole integer. Please check your entry and try again.");
        }
        itemList.type = (CheckType)checkDropdown.value;

        if (nameField.text != itemList.name) {
            itemList.name = nameField.text;
            File.Delete(Path.Combine(PokedexManager.dataPath, itemList.savePath));
            itemList.savePath = "ItemLists/" + itemList.name + ".json";
            itemList.ToJson(itemList.savePath);
        } else {
            itemList.ToJson(itemList.savePath, true);
        }
        OnSelected();
    }

    private IEnumerator<GameObject> CreateItemEntries(ItemList itemList) {
        foreach (Item item in itemList.Items) {
            GameObject newItem = Instantiate(PokedexManager.manager.itemPrefab) as GameObject;
            newItem.transform.parent = itemView.transform;
            newItem.transform.localScale = Vector3.one;
            ItemEntry controller = newItem.GetComponent<ItemEntry>();
            controller.parent = PokedexManager.currentItemList.GetComponent<ItemListEntry>();
            foreach (var fullItem in PokedexManager.items) {
                if (fullItem.name == item.name) {
                    controller.item = fullItem;
                    controller.SetFields();
                    break;
                }
            }
            yield return newItem;
        }
    }

    public void DeleteCurrentItemList() {
        PokedexManager.manager.CreateConfirmationDialog("Are you sure you wish to delete the item list \"" + nameField.text + "\"?", ConfirmationType.listDelete);
    }

    public void CreateNewListEntry(bool append = false) {
        ItemList itemList = new ItemList();
        List<Item> toBeItemList = new List<Item>();
        List<Item> itemsThatCanBeFound = new List<Item>();
        if (append && PokedexManager.currentItemList != null) {
            itemList = PokedexManager.currentItemList.GetComponent<ItemListEntry>().itemList;
            foreach (Item item in itemList.Items) {
                toBeItemList.Add(item);
            }
        } else {
            itemList.savePath = "ItemLists/" + itemList.name + ".json";
        }

        int min = (int)minTierSlider.value;
        int max = (int)maxTierSlider.value;

        foreach (Item item in PokedexManager.items) {
            if (item.tier < min || item.tier > max) {
                continue;
            }
            Group currentGroup = groupList.FirstOrDefault(x => x.title == item.group);
            if (currentGroup.title == item.group) {
                if (currentGroup.allowed == false) {
                    continue;
                }
            }
            itemsThatCanBeFound.Add(item);
        }
        if (itemsThatCanBeFound.Count() < 1) {
            PokedexManager.manager.CreateWarningDialog("No items fit these parameters.");
            return;
        }

        int tracker = max;
        List<int> values = new List<int>();
        for (int i = min; i <= max; i++) {
            for (int x = 0; x < tracker; x++) {
                values.Add(i);
            }
            tracker--;
        }
        for (int i = 0; i < countSlider.value; i++) {
            int chosenTier = Random.Range(0, values.Count());
            List<Item> itemsToChooseFrom = new List<Item>();
            itemsToChooseFrom = itemsThatCanBeFound.Where(x => x.tier == values[chosenTier]).ToList<Item>();
            if (itemsToChooseFrom.Count() < 1) {
                i--;
                continue;
            }
            int chosenItem = Random.Range(0, itemsToChooseFrom.Count());
            toBeItemList.Add(itemsToChooseFrom[chosenItem]);
        }
        itemList.Items = toBeItemList.ToArray();
        if (!append) {
            CreateItemListEntry(itemList);
            itemList.ToJson(itemList.savePath);
        } else {
            itemList.ToJson(itemList.savePath, true);
            OnSelected();
        }
    }

    public void CreateItemListEntry(ItemList itemList) {
        GameObject controller = Instantiate(PokedexManager.manager.itemListPrefab);
        controller.transform.SetParent(contentView.transform);
        controller.transform.localScale = Vector3.one;
        ItemListEntry listEntry = controller.GetComponent<ItemListEntry>();
        listEntry.itemList = itemList;
        listEntry.SetFields();
    }

    public void UpdateCountSliderNumber(bool bySlider) {
        UnityEngine.UI.InputField sliderLabel = GameObject.Find("Count Field").GetComponent<UnityEngine.UI.InputField>();
        if (bySlider) {
            sliderLabel.text = countSlider.value.ToString();
        } else {
            sliderLabel.text = Mathf.Clamp(int.Parse(sliderLabel.text), (int)countSlider.minValue, (int)countSlider.maxValue).ToString();
            countSlider.value = float.Parse(sliderLabel.text);
        }
    }

    public void UpdateMinLevelSliderNumber(bool bySlider) {
        UnityEngine.UI.InputField sliderLabel = GameObject.Find("Min Tier Field").GetComponent<UnityEngine.UI.InputField>();
        if (bySlider) {
            sliderLabel.text = minTierSlider.value.ToString();
        } else {
            sliderLabel.text = Mathf.Clamp(int.Parse(sliderLabel.text), (int)minTierSlider.minValue, (int)minTierSlider.maxValue).ToString();
            minTierSlider.value = float.Parse(sliderLabel.text);
        }
        if (minTierSlider.value > maxTierSlider.value) {
            maxTierSlider.value = minTierSlider.value;
        }
    }

    public void UpdateMaxLevelSliderNumber(bool bySlider) {
        UnityEngine.UI.InputField sliderLabel = GameObject.Find("Max Tier Field").GetComponent<UnityEngine.UI.InputField>();
        if (bySlider) {
            sliderLabel.text = maxTierSlider.value.ToString();
        } else {
            sliderLabel.text = Mathf.Clamp(int.Parse(sliderLabel.text), (int)maxTierSlider.minValue, (int)maxTierSlider.maxValue).ToString();
            maxTierSlider.value = float.Parse(sliderLabel.text);
        }
        if (maxTierSlider.value < minTierSlider.value) {
            minTierSlider.value = maxTierSlider.value;
        }
    }

    public void ShowItems() {
        itemsPanel.transform.SetSiblingIndex(panelParent.transform.childCount);
        itemsTab.GetComponent<Image>().color = PokedexManager.frontGrey;
        searchTab.GetComponent<Image>().color = PokedexManager.backGrey;
        groupTab.GetComponent<Image>().color = PokedexManager.backGrey;
    }

    public void ShowSearch() {
        searchPanel.transform.SetSiblingIndex(panelParent.transform.childCount);
        itemsTab.GetComponent<Image>().color = PokedexManager.backGrey;
        searchTab.GetComponent<Image>().color = PokedexManager.frontGrey;
        groupTab.GetComponent<Image>().color = PokedexManager.backGrey;
    }

    public void ShowGroups() {
        groupPanel.transform.SetSiblingIndex(panelParent.transform.childCount);
        itemsTab.GetComponent<Image>().color = PokedexManager.backGrey;
        searchTab.GetComponent<Image>().color = PokedexManager.backGrey;
        groupTab.GetComponent<Image>().color = PokedexManager.frontGrey;
    }
}
