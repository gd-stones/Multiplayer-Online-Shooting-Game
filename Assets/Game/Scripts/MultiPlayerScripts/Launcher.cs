using Photon.Pun;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Debug.Log("connecting to master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connect to master");
        OnJoinedLobby();
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("TitleMenu");
        Debug.Log("joined lobby");
    }
}
