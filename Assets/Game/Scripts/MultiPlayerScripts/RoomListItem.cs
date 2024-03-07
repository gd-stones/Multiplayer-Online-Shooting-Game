using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] private Text roomNameText;
    private RoomInfo info;

    public void SetUp(RoomInfo info)
    {
        this.info = info;
        roomNameText.text = info.Name;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info);
    }
}
