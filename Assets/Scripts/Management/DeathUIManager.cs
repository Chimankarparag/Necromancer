using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathUIManager : MonoBehaviour
{
    public GameObject deathUI;
    public static bool isActivated = false;
    const string MENU_TEXT = "MainMenu";

    void Start(){
        deathUI=GameObject.Find("DeathUI");
        deathUI.SetActive(false);
    }

    public void GotKilled(){
        deathUI.SetActive(true);

    }


    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(MENU_TEXT);
        deathUI.SetActive(false);

        // Make sure to use UnityEngine.SceneManagement if using SceneManager
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
