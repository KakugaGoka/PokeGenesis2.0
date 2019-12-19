using System.IO;
using System.Linq;
using UnityEngine;

public class PokedexManager : MonoBehaviour
{
    static public Pokemon[] pokedex;

    // Start is called before the first frame update
    void Awake()
    {
        // Load Pokemon From JSON
        TextAsset pokemonString = Resources.Load<TextAsset>("JSON/Pokemon");
        pokedex = JsonHelper.FromJson<Pokemon>(pokemonString.text);
        pokedex = pokedex.OrderBy(x => x.number).ToArray<Pokemon>();

        // Load in Types from JSON
        TextAsset jsonString = Resources.Load<TextAsset>("JSON/PokemonTypes");
        PokemonType[] pokemonTypes = JsonHelper.FromJson<PokemonType>(jsonString.text);
        Debug.Log(pokemonTypes.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
