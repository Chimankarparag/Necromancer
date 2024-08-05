using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool isPaused = false;
    const string MENU_TEXT = "MainMenu";

    void Start(){
        pauseMenuUI=GameObject.Find("PauseMenu");
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // Check if the player presses the pause button (Escape key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide pause menu
        Time.timeScale = 1f; // Resume game time
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true); // Show pause menu
        Time.timeScale = 0f; // Pause game time
        isPaused = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(MENU_TEXT);
        // Make sure to use UnityEngine.SceneManagement if using SceneManager
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
