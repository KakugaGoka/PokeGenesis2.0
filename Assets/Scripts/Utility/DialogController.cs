using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public Text messageBox;
    public Camera cameraForDialog;
    public ConfirmationType confirmationType;


    void Start() {
        cameraForDialog = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void Close() {
        Destroy(this.gameObject);
    }

    public void Confirm() {
        switch (confirmationType) {
            case ConfirmationType.trade:
                Client.client.startClient = true;
                Destroy(this.gameObject);
                return;
            case ConfirmationType.delete:
                PokedexManager.manager.DeleteCurrentPokemonAndEntry();
                Destroy(this.gameObject);
                return;
            case ConfirmationType.capture:
                PokedexManager.manager.CaptureCurrentSelected();
                Destroy(this.gameObject);
                return;
            case ConfirmationType.release:
                PokedexManager.manager.ReleaseCurrentSelected();
                Destroy(this.gameObject);
                return;
        }
    }
}

public enum ConfirmationType {
    trade = 0,
    delete, 
    capture,
    release
}
