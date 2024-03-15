using UnityEngine;
using Photon.Pun;
using System.IO;
using System.Collections.Generic;

public class PlayerControllerManager : MonoBehaviourPunCallbacks
{
    private PhotonView view;
    private GameObject controller;

    public int playerTeam;
    private Dictionary<int, int> playerTeams = new Dictionary<int, int>();

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (view.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
        {
            playerTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
            Debug.Log ("Player's team: " + playerTeam);
        }
     
        AssignPlayerToSpawnArea(playerTeam);
    }

    private void AssignPlayerToSpawnArea(int team)
    {
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), Vector3.zero, Quaternion.identity, 0, new object[] { view.ViewID });
    }

    private void AssignTeamsToAllPlayers()
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.ContainsKey("Team"))
            {
                int team = (int)player.CustomProperties["Team"];
                playerTeams[player.ActorNumber] = team;
                Debug.Log(player.NickName + " is on team " + team);
            }
        }
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        AssignTeamsToAllPlayers();
    }
}
