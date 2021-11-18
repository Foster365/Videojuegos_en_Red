using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;

using TMPro;

public class PhotonChatController : MonoBehaviour, IChatClientListener
{

    [SerializeField] TextMeshProUGUI chatLineContent;
    [SerializeField] TMP_InputField messageInputField;

    [SerializeField] ScrollRect chatScroll;
    float limitScrollAutomation = .2f;

    ChatClient playerChatClient; //Me sirve para mandar mensajes, actualizar el estado de mi chat, enviar mensajes privados
    //[SerializeField] string playerUserId;

    string[] channels;
    string[] chats;

    int currentChat;
    int maxChatLines = 3;

    Dictionary<string, int> chatsDictionary = new Dictionary<string, int>();

    public Action OnSelect = delegate { };
    public Action OnDeselect = delegate { };

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

        if(chatLineContent.textInfo.lineCount >= maxChatLines)
        {
            StartCoroutine(WaitToDeleteLine());
        }

        //Compruebo si tengo que hacer scroll o no cuando se actualiza el chat.
        if (chatScroll.verticalNormalizedPosition < limitScrollAutomation)
        {
            StartCoroutine(WaitToScroll());
        }

    }

    IEnumerator WaitToScroll()
    {
        yield return new WaitForEndOfFrame();
        chatScroll.verticalNormalizedPosition = 0;
    }

    IEnumerator WaitToDeleteLine()
    {
        yield return new WaitForEndOfFrame();

        if (chatLineContent.textInfo.lineCount > maxChatLines + 1)
        {
            for (int i = 0; i < chatLineContent.textInfo.lineCount - maxChatLines; i++)
            {

                var index = chats[currentChat].IndexOf("\n"); //Obtengo el índice del primer salto del línea, para poder cortar el string, diciéndole en qué índice quiero que empiece
                chats[currentChat] = chats[currentChat].Substring(index + 1);

                chatLineContent.text = chats[currentChat];
            }
        }

    }

    public void SendPlayerMessage()
    {

        if (string.IsNullOrEmpty(messageInputField.text) || string.IsNullOrWhiteSpace(messageInputField.text)) return;
        Debug.Log("Send Player Message: Input Field is not null or white, and it doesn't have any empty space");

        //Private Message
        string[] messageWords = messageInputField.text.Split(' ');
        if (messageWords[0] == "/w" && messageWords.Length > 2)
        {
            playerChatClient.SendPrivateMessage(messageWords[1], string.Join(" ", messageWords, 2, messageWords.Length - 2));
        }
        else playerChatClient.PublishMessage(channels[currentChat], messageInputField.text);
        //
        messageInputField.text = ""; //Cuando termino de mandar el mensaje borro la línea
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(messageInputField .gameObject);

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

            string color;

            if (senders[i] == PhotonNetwork.NickName) color = "<color=yellow>";
            else color = "<color=orange>";
                int indexChat = chatsDictionary[channelName];
            chats[indexChat] += color + senders[i] + ": " + "</color>" + messages[i] + "\n";

        }
        UpdateChatUI();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        for (int i = 0; i < chats.Length; i++)
        {
            chats[i] += "<color=green>" + sender + ": " + "</color>" + message + "\n";
        }
        UpdateChatUI();
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
            chats[0] += "<color=red>"+ PhotonNetwork.NickName + " hooped into channel: " + channels[i] + "! :)" + "</color>" + "\n";
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

    public void SelectChat()
    {
        OnSelect();
    }

    public void DeselectChat()
    {
        OnDeselect();
    }

}