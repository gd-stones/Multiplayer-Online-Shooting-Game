using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerControllerManager : MonoBehaviour
{
    private PhotonView view;
    private GameObject controller;

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
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), Vector3.zero, Quaternion.identity, 0, new object[] { view.ViewID });
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }
}
