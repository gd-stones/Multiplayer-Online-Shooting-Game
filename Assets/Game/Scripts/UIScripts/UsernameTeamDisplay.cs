using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class UsernameTeamDisplay : MonoBehaviour
{
    public Text usernameText;
    public Text teamText;
    public PhotonView view;

    private void Start()
    {
        if (view.IsMine)
        {
            // dont want to display username
            gameObject.SetActive(false);
        }

        usernameText.text = view.Owner.NickName;

        if (view.Owner.CustomProperties.ContainsKey("Team"))
        {
            int team = (int)view.Owner.CustomProperties["Team"];
            teamText.text = "Team " + team;
        }
    }
}
