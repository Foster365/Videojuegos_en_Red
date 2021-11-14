using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;

using TMPro;

public class PhotonChatController : MonoBehaviour, IChatClientListener
{

    [SerializeField] TextMeshProUGUI chatLineContent;
    [SerializeField] TMP_InputField messageInputField;

    ChatClient playerChatClient; //Me sirve para mandar mensajes, actualizar el estado de mi chat, enviar mensajes privados
    //[SerializeField] string playerUserId;

    string[] channels;
    string[] chats;

    int currentChat;

    Dictionary<string, int> chatsDictionary = new Dictionary<string, int>();

    private void Start()
    {

        if (!PhotonNetwork.IsConnected) return; // Esto es meramente porque necesito que todos estén conectados, pero Photon Chat es independiente de Realtime y demás, así
                                                //que tranquilamente puedo prescindir de este chequeo

        channels = new string[] { "World", PhotonNetwork.CurrentRoom.Name }; //Cueva del marcianeke es PhotonNetwork.CurrentRoom.Name, está dando null
        chats = new string[2];

        chatsDictionary["World"] = 0;
        chatsDictionary[PhotonNetwork.CurrentRoom.Name] = 1;

        //GetChannels();

        Debug.Log("La cueva del marcianeke ha sido abierta, enemigos del heredero temed");
        playerChatClient = new ChatClient(this); // Va a tomar como referencia esta clase para reproducir las funciones de la inter
        playerChatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion,
        new AuthenticationValues(PhotonNetwork.LocalPlayer.NickName)); // Los nicknames se pueden repetir, cuando pueda hacer una base de datos que almacene
                                                                       // los nicknames y chequee que no se repitan

    }

    void GetChannels()
    {
        Debug.Log("IS WORKING!");

        int dictIndex = 0;
        for (var i = 0; i < channels.Length; i++)
        {

            chatsDictionary[channels[i]] = dictIndex;
            dictIndex++;

            Debug.Log("Chat Dictionary value: " + chatsDictionary[channels[i]]);
        }
    }

    private void Update()
    {
        if (playerChatClient != null) Debug.Log("Player Chat Client is not null");
        playerChatClient.Service(); //Recibe las actualizaciones en cuanto a mensajes, estados
    }

    void UpdateChatUI() //Voy a actualizar constantemente el text del content (El chat)
    {

        chatLineContent.text = chats[currentChat];

    }

    public void SendPlayerMessage()
    {

        if (string.IsNullOrEmpty(messageInputField.text) || string.IsNullOrWhiteSpace(messageInputField.text)) return;
        Debug.Log("Send Player Message: Input Field is not null or white, and it doesn't have any empty space");
        playerChatClient.PublishMessage(channels[currentChat], messageInputField.text);

    }

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log("Debug Return yet not implemented yet");
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log("On State Change not implemented yet");
    }

    public void OnConnected()
    {
        Debug.Log("Chat Connected");
        playerChatClient.Subscribe(channels); // Me suscribo a los canales
    }

    public void OnDisconnected()
    {
        Debug.Log("Chat Disconnected");
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages) //El sender y el messaje comparten index. El sender en [0] tiene el message en [0]
    {
        Debug.Log("On Get Messages is working");
        for (int i = 0; i < senders.Length; i++)
        {

            int indexChat = chatsDictionary[channelName];
            chats[indexChat] += senders[i] + ": " + messages[i] + "\n";

        }
        UpdateChatUI();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log("On Private Message not implemented yet");
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Debug.Log("On Status Update not implemented yet");
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        for (int i = 0; i < channels.Length; i++)//Dinujo las líneas que se escriban en el chat
        {
            //chatLineContent.text += channels[i] + "\n"; //Cuando escriba una nueva linea se va a dibujar debajo
            chats[0] += "color=blue>"+ PhotonNetwork.NickName + " hooped into channel: " + channels[i] + "! :)" + "</color>" + "\n";
        }

        UpdateChatUI();

    }

    public void OnUnsubscribed(string[] channels) //Tengo un array porque si me desconecto de varios canales cuando el service no se actualizó (Se actualiza cada
                                                  //cierto tiempo), cuando se actualice me va a devolver todos los canales a los que me desuscribí
    {

        Debug.Log("On Unsuscribed not implemented yet");
    }

    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log("On User Suscribed not implemented yet");
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log("On User Unsuscribed not implemented yet");
    }

}
