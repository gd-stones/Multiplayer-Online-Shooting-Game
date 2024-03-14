using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerUsernameManager : MonoBehaviour
{
    [SerializeField] private InputField usernameInput;
    [SerializeField] private Text errorMessageText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            usernameInput.text = PlayerPrefs.GetString("username");
            PhotonNetwork.NickName = PlayerPrefs.GetString("username");
        }
    }

    public void PlayerUsernameInputValueChanged()
    {
        string username = usernameInput.text;

        if (!string.IsNullOrEmpty(username) && username.Length <20)
        {
            PhotonNetwork.NickName = username;
            PlayerPrefs.SetString("username", username);
            errorMessageText.text = "";
            MenuManager.Instance.OpenMenu("TitleMenu");
        }
        else
        {
            errorMessageText.text = "Username must be less than 20 characters";
        }
    }
}
