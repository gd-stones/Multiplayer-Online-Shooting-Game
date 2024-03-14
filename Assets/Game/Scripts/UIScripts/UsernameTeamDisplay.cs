using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class UsernameTeamDisplay : MonoBehaviour
{
    public Text usernameText;
    public PhotonView view; 

    private void Start()
    {
        if (view.IsMine)
        {
            // dont want to display username
            gameObject.SetActive(false);
        }
        
        usernameText.text = view.Owner.NickName;
    }
}
