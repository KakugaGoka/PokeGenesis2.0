using System.IO;
using UnityEngine;
using UnityEngine.Networking;


public class Client : MonoBehaviour {

    public static Client client;
    int port = 9999;
    public string ip = "127.0.0.1";

    // The id we use to identify our messages and register the handler
    short messageID = 1000;

    // The network client
    NetworkClient myClient;

    public bool startClient = false;
    public bool clientStarted = false;

    public void Start() {
        if (client == null) {
            client = this;
        } else {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Update() {
        if (startClient && !clientStarted) {
            CreateClient();
            clientStarted = true;
            startClient = false;
        }
    }

    void CreateClient() {
        var config = new ConnectionConfig();

        // Config the Channels we will use
        config.AddChannel(QosType.ReliableFragmented);
        config.AddChannel(QosType.UnreliableFragmented);

        // Create the client ant attach the configuration
        myClient = new NetworkClient();
        myClient.Configure(config, 1);

        // Register the handlers for the different network messages
        RegisterHandlers();

        Debug.Log("Connecting to Server...");
        // Connect to the server
        myClient.Connect(ip, port);
    }

    // Register the handlers for the different message types
    void RegisterHandlers() {

        // Unity have different Messages types defined in MsgType
        myClient.RegisterHandler(messageID, OnMessageReceived);
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(MsgType.Disconnect, OnDisconnected);
    }

    void OnConnected(NetworkMessage message) {
        // Do stuff when connected to the server
        Debug.Log("Sending Pokemon...");

        NetworkPokemon messageContainer = new NetworkPokemon();
        messageContainer.message = JsonUtility.ToJson(PokedexManager.currentPokemon, true);

        // Say hi to the server when connected
        myClient.Send(messageID, messageContainer);
    }

    void OnDisconnected(NetworkMessage message) {
        // Do stuff when disconnected to the server
        clientStarted = false;
        myClient = null;
    }

    // Message received from the server
    void OnMessageReceived(NetworkMessage netMessage) {
        // You can send any object that inherence from MessageBase
        // The client and server can be on different projects, as long as the MyNetworkMessage or the class you are using have the same implementation on both projects
        // The first thing we do is deserialize the message to our custom type
        var objectMessage = netMessage.ReadMessage<NetworkPokemon>();
        if (objectMessage.message == "Pokemon Recieved!") {
            File.Delete(Path.Combine(Application.streamingAssetsPath, PokedexManager.currentPokemon.savePath));
            myClient.Disconnect();
            clientStarted = false;
            startClient = false;
        }
    }
}