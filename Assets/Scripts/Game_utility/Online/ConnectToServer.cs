using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;
using System.Linq;
using static JsonFunctions;
public class ConnectToServer : MonoBehaviourPunCallbacks 
{
    public static ConnectToServer Instance;

    [SerializeField] TMP_InputField roomNameInputField; 
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject PlayerListItemPrefab;
    [SerializeField] GameObject startGameButton;

    [SerializeField] TMP_Text scenesSelectButton;
    public static List<string> scenes = new List<string>();
    public static int ListCicleActual = 0;
    private int ListCicleMax = 0;

    [SerializeField] TMP_InputField playerNameInputField; 

    

    void Awake()
    {
        Instance = this;
        //para cambiar el escenario.
        scenes.Add("Prueba Sebaster");
        scenes.Add("Nivel 1");
        ListCicleMax = scenes.Count;
        scenesSelectButton.text = scenes[ListCicleActual];
    }

    // Start is called before the first frame update
    void Start()
    {
        
        PhotonNetwork.ConnectUsingSettings();

    }
    public override void OnConnectedToMaster()
    {
        NickNameAsign();
        PhotonNetwork.JoinLobby(); 
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
        MenuManager.Instance.CloseMenu("loading");
        /*PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
        //SceneManager.LoadScene("Lobby"); // <- delete this*/
    }

    public void CreateRoom()
    {
        NickNameAsign();

        RoomOptions roomOptions = 
        new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 8,
        };
        ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();
        _myCustomProperties.Add("Scene", scenes[ListCicleActual]);

        roomOptions.CustomRoomProperties = _myCustomProperties;




        if(string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
        MenuManager.Instance.OpenMenu("loading");
        

    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);

    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(scenes[ListCicleActual]);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (var i = 0; i < roomList.Count; i++)
        {
            if(roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void OnRightArrowClick()
    {
        ListCicleActual++;
        if(ListCicleActual >= ListCicleMax)
        {
            ListCicleActual = 0;
        }
        scenesSelectButton.text = scenes[ListCicleActual];
    }
    public void OnLeftArrowClick()
    {
        ListCicleActual--;
        if(ListCicleActual < 0)
        {
            ListCicleActual = ListCicleMax-1;
        }
        scenesSelectButton.text = scenes[ListCicleActual];
    }

    public void NickNameAsign()
    {
        string archivoPlayer = "Assets/Scripts/Game_utility/JSON/playerData.json";
        PhotonNetwork.NickName = CargarValorJSON<PlayerData, string>(archivoPlayer, datos => datos.Name);
    }

}
