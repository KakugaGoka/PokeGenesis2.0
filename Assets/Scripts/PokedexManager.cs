using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PokedexManager : MonoBehaviour {
    static public PokedexManager manager;
    static public Pokemon[] pokedex;
    static public PokemonType[] types;

    // Start is called before the first frame update
    void Awake() {
        if (manager == null) {
            manager = this;
        } else {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        // Load Pokemon From JSON
        TextAsset pokemonString = Resources.Load<TextAsset>("JSON/Pokemon");
        pokedex = JsonHelper.FromJson<Pokemon>(pokemonString.text);
        pokedex = pokedex.OrderBy(x => x.number).ToArray<Pokemon>();

        // Load in Types from JSON
        TextAsset jsonString = Resources.Load<TextAsset>("JSON/PokemonTypes");
        types = JsonHelper.FromJson<PokemonType>(jsonString.text);
    }

    public void ChangeScene(int sceneID) {
        SceneManager.LoadScene(sceneID);
    }
}
