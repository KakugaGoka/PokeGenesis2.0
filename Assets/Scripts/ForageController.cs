using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForageController : MonoBehaviour {
    public GameObject
        contentView,
        itemView;

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
        itemsTab,
        searchTab,
        panelParent;

    private Dictionary<string, bool>
        groupDictionary = new Dictionary<string, bool>();

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
        Debug.Log("Max Tier is: " + maxTier.ToString());

        minTierSlider.maxValue = maxTier;
        maxTierSlider.maxValue = maxTier;

        searchPanel = GameObject.Find("Search Panel");
        itemsPanel = GameObject.Find("Items Panel");
        panelParent = GameObject.Find("Panels");

        searchTab = GameObject.Find("Search Tab");
        itemsTab = GameObject.Find("Items Tab");

        groupToggle = GameObject.Find("Group Toggle").GetComponent<Toggle>();

        groupDropdown = GameObject.Find("Allowed Groups Dropdown").GetComponent<Dropdown>();
        List<string> groups = new List<string>();
        foreach (Item item in PokedexManager.items) {
            if (!groups.Contains(item.group)) {
                groups.Add(item.group);
            }
        }
        groups = groups.OrderBy(x => x).ToList();
        foreach (string group in groups) {
            groupDictionary.Add(group, true);
        }
        List<Dropdown.OptionData> groupOptions = new List<Dropdown.OptionData>();
        foreach (string group in groupDictionary.Keys) {
            groupOptions.Add(new Dropdown.OptionData(group));
        }
        groupDropdown.ClearOptions();
        groupDropdown.AddOptions(groupOptions);

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

    public void SetGroupToggle() {
        groupDictionary[groupDropdown.options[groupDropdown.value].text] = groupToggle.isOn;
    }

    public void GetGroupToggle() {
        groupToggle.isOn = groupDictionary[groupDropdown.options[groupDropdown.value].text];
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
            if (groupDictionary.Keys.Contains(item.group)) {
                if (groupDictionary[item.group] == false) {
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

    public void UpdateEncounterSliderNumber() {
        Text sliderLabel = GameObject.Find("Count Slider Label").GetComponent<Text>();
        sliderLabel.text = "Count: " + countSlider.value.ToString();
    }

    public void UpdateMinLevelSliderNumber() {
        Text sliderLabel = GameObject.Find("Min Tier Slider Label").GetComponent<Text>();
        sliderLabel.text = "Min Lvl: " + minTierSlider.value.ToString();
        if (minTierSlider.value > maxTierSlider.value) {
            maxTierSlider.value = minTierSlider.value;
        }
    }

    public void UpdateMaxLevelSliderNumber() {
        Text sliderLabel = GameObject.Find("Max Tier Slider Label").GetComponent<Text>();
        sliderLabel.text = "Max Lvl: " + maxTierSlider.value.ToString();
        if (maxTierSlider.value < minTierSlider.value) {
            minTierSlider.value = maxTierSlider.value;
        }
    }

    public void ShowItems() {
        itemsPanel.transform.SetSiblingIndex(panelParent.transform.childCount);
        itemsTab.GetComponent<Image>().color = PokedexManager.frontGrey;
        searchTab.GetComponent<Image>().color = PokedexManager.backGrey;
    }

    public void ShowSearch() {
        searchPanel.transform.SetSiblingIndex(panelParent.transform.childCount);
        itemsTab.GetComponent<Image>().color = PokedexManager.backGrey;
        searchTab.GetComponent<Image>().color = PokedexManager.frontGrey;
    }
}
