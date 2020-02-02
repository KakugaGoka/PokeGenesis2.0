using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenController : MonoBehaviour {
    
    private Dropdown
        habitatDropdown;

    private Texture2D
        map;

    private Color
        roomColor = Color.white,
        wallColor = Color.black,
        lineColor = Color.gray;

    public MapGenerator 
        mapGen;

    // Start is called before the first frame update
    void Start()
    {
        habitatDropdown = GameObject.Find("Habitat Dropdown").GetComponent<Dropdown>();

        List<Dropdown.OptionData> habitatOptions = new List<Dropdown.OptionData>();
        habitatOptions.Add(new Dropdown.OptionData("Any Habitat"));
        foreach (string habitat in PokedexManager.habitats) {
            habitatOptions.Add(new Dropdown.OptionData(habitat));
        }
        habitatDropdown.AddOptions(habitatOptions);

        mapGen = gameObject.GetComponent<MapGenerator>();
    }

    public void CreateMap() {
        mapGen.GenerateMap();
        PushMapToView(mapGen.mapTexture);
    }

    void PushMapToView(Texture2D texture) {
        //while (!roomsLinked)
        //    yield return new WaitForSeconds(0.1f);
        Sprite newSprite = Sprite.Create(texture,
                                 new Rect(0, 0, texture.width, texture.height),
                                 new Vector2(0.5f, 0.5f));

        GameObject content = new GameObject();

        content.AddComponent<RectTransform>();
        RectTransform rect = content.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(mapGen.width, mapGen.height);

        content.AddComponent<Image>();
        Image image = content.GetComponent<Image>();
        image.sprite = newSprite;
        GameObject view = GameObject.Find("Lists Content");

        foreach (var child in view.GetComponentsInChildren<Image>()) {
            if (child.gameObject != view.gameObject) {
                Destroy(child.gameObject);
            }
        }

        content.transform.SetParent(view.transform);
        rect.localScale = Vector3.one;
    }
}
