using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class DialogController : MonoBehaviour
{
    public Text messageBox;
    public ConfirmationType confirmationType;

    public void Close() {
        if (this.gameObject.name.Contains("Sending")) {
            Client.client.myClient.Disconnect();
        }
        Destroy(this.gameObject);
    }

    public void Confirm() {
        switch (confirmationType) {
            case ConfirmationType.trade:
                Client.client.startClient = true;
                break;
            case ConfirmationType.delete:
                PokedexManager.manager.DeleteCurrentPokemonAndEntry();
                break;
            case ConfirmationType.capture:
                PokedexManager.manager.CaptureCurrentSelected();
                break;
            case ConfirmationType.release:
                PokedexManager.manager.ReleaseCurrentSelected();
                break;
            case ConfirmationType.quit:
                Application.Quit();
                break;
            case ConfirmationType.backup:
                if (File.Exists(Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json.bak"))) {
                    File.Delete(Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json.bak"));
                }
                File.Copy(Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json"), Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json.bak"));
                break;
            case ConfirmationType.restore:
                File.Delete(Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json"));
                File.Copy(Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json.bak"), Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json"));
                break;
            case ConfirmationType.merge:
                string pokedexString = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json"));
                string backupString = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json.bak"));

                JObject pokedexJSON = JObject.Parse(pokedexString);
                JObject backupJSON = JObject.Parse(backupString);

                pokedexJSON.Merge(backupJSON, new JsonMergeSettings {
                    MergeArrayHandling = MergeArrayHandling.Merge
                });

                string data = pokedexJSON.ToString();
                File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "JSON/Pokemon.json"), data);
                PokedexManager.manager.CreateWarningDialog("Pokedex successfully merged with the backup pokedex data!");
                break;

        }
        Destroy(this.gameObject);
    }
}

public enum ConfirmationType {
    trade = 0,
    delete, 
    capture,
    release,
    quit,
    backup,
    restore,
    merge
}
