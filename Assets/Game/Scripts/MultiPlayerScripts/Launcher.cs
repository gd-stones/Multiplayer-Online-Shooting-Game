using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    [SerializeField] private InputField roomNameInputField;
    [SerializeField] private Text roomNameText;
    [SerializeField] private Text errorText;
    [SerializeField] private Transform roomListContent;
    [SerializeField] private GameObject roomListItemPrefab;
    [SerializeField] private Transform playerListContent;
    [SerializeField] private GameObject playerListItemPrefab;
    public GameObject startButton;

    private int nextTeamNumber = 1; 

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Debug.Log("connecting to master...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connect to master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("UserNameMenu");
        Debug.Log("joined lobby");
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInputField.text))
        {
            PhotonNetwork.CreateRoom(roomNameInputField.text);
            MenuManager.Instance.OpenMenu("LoadingMenu");
        }
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("RoomMenu");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Count(); i++)
        {
            int teamNumber = GetNextTeamNumber();
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i], teamNumber);
        }

        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string errorMessage)
    {
        errorText.text = "Room Creation Failed: " + errorMessage;
        MenuManager.Instance.OpenMenu("ErrorMenu");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("LoadingMenu");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("LoadingMenu");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("TitleMenu");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
            {
                continue;
            }

            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        int teamNumber = GetNextTeamNumber();
        GameObject playerItem =  Instantiate(playerListItemPrefab, playerListContent);
        playerItem.GetComponent<PlayerListItem>().SetUp(newPlayer, teamNumber);
    }

    private int GetNextTeamNumber()
    {
        int teamNumber = nextTeamNumber;
        nextTeamNumber = 3 - nextTeamNumber;
        return teamNumber;
    }
}
