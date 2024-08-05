using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinUIManager : MonoBehaviour
{
    public GameObject WinUI;
    public static bool isActivated = false;
    const string MENU_TEXT = "MainMenu";

    void Start(){
        WinUI=GameObject.Find("WinUI");
        WinUI.SetActive(false);
    }

    public void GameWin(){
        WinUI.SetActive(true);

    }


    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(MENU_TEXT);
        WinUI.SetActive(false);

        // Make sure to use UnityEngine.SceneManagement if using SceneManager
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
