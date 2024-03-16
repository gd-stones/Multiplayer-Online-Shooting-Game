using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isMenuOpened = false;
    public GameObject menuUI;
    public GameObject scoreUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isMenuOpened == false)
        {
            scoreUI.SetActive(false);
            menuUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isMenuOpened = true;
            AudioListener.pause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isMenuOpened == true)
        {
            scoreUI.SetActive(true);
            menuUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isMenuOpened = false;
            AudioListener.pause = false;
        }
    }

    public void LeaveGame()
    {
        Debug.Log("Game leaved");
        Application.Quit();
    }
}
