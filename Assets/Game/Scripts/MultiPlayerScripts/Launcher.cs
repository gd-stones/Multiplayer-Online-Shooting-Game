using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
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
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("TitleMenu");
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
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
}
