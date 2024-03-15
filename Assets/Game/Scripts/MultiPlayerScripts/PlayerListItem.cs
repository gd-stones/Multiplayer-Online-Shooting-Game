using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    public Text playerName;
    public Text teamText;
    private Player player;
    private int team; 

    public void SetUp(Player _player, int _team)
    {
        this.player = _player;
        this.team = _team;
        playerName.text = player.NickName;
        teamText.text = "Team " + _team;

        ExitGames.Client.Photon.Hashtable customProps = new ExitGames.Client.Photon.Hashtable();
        customProps["Team"] = _team;
        _player.SetCustomProperties(customProps);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
