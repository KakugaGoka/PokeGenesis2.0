using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour {

    static public Server server;
    int port = 9999;
    int maxConnections = 10;

    // The id we use to identify our messages and register the handler
    short messageID = 1000;

    public bool startServer = false;
    public bool serverStarted = false;

    public void Start() {
        if (server == null) {
            server = this;
        } else {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Update() {
        if (startServer && !serverStarted) {
            CreateServer();
            serverStarted = true;
        }
    }

    public void CreateServer() {
        // Register handlers for the types of messages we can receive
        RegisterHandlers();

        var config = new ConnectionConfig();
        // There are different types of channels you can use, check the official documentation
        config.AddChannel(QosType.ReliableFragmented);
        config.AddChannel(QosType.UnreliableFragmented);

        var ht = new HostTopology(config, maxConnections);

        if (!NetworkServer.Configure(ht)) {
            Debug.Log("No server created, error on the configuration definition");
            return;
        } else {
            // Start listening on the defined port
            if (NetworkServer.Listen(port))
                Debug.Log("Server created, listening on port: " + port);
            else
                Debug.Log("No server created, could not listen to the port: " + port);
        }
    }

    public void CloseServer() {
        NetworkServer.Shutdown();
    }

    private void RegisterHandlers() {
        // Unity have different Messages types defined in MsgType
        NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnClientDisconnected);

        // Our message use his own message type.
        NetworkServer.RegisterHandler(messageID, OnMessageReceived);
    }

    private void RegisterHandler(short t, NetworkMessageDelegate handler) {
        NetworkServer.RegisterHandler(t, handler);
    }

    void OnClientConnected(NetworkMessage netMessage) {
        // Do stuff when a client connects to this server

        // Send a thank you message to the client that just connected
        NetworkPokemon messageContainer = new NetworkPokemon();
        messageContainer.message = JsonUtility.ToJson(PokedexManager.currentPokemon, true);

        // This sends a message to a specific client, using the connectionId
        NetworkServer.SendToClient(netMessage.conn.connectionId, messageID, messageContainer);
    }

    void OnClientDisconnected(NetworkMessage netMessage) {
        // Do stuff when a client dissconnects
    }

    void OnMessageReceived(NetworkMessage netMessage) {
        // You can send any object that inherence from MessageBase
        // The client and server can be on different projects, as long as the MyNetworkMessage or the class you are using have the same implementation on both projects
        // The first thing we do is deserialize the message to our custom type
        var objectMessage = netMessage.ReadMessage<NetworkPokemon>();
        Pokemon pokemon = JsonUtility.FromJson<Pokemon>(objectMessage.message);
        pokemon.ToJson(Path.Combine(Application.streamingAssetsPath, pokemon.savePath));
        Debug.Log("Message received: " + pokemon.species);

    }
}