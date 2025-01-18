using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private PlayerControls playerControls;
    public static bool isPaused = false;
    const string MENU_TEXT = "MainMenu";

    private void Awake()
    {
        // Initialize PlayerControls and subscribe to the Pause action
        playerControls = new PlayerControls();
        playerControls.Pause.Pause.performed += OnPause; // Link action to handler
    }

    private void OnEnable()
    {
        // Enable the Input Action Map
        playerControls.Pause.Enable();
    }

    private void OnDisable()
    {
        // Disable the Input Action Map
        playerControls.Pause.Disable();
    }

    private void Start()
    {
        pauseMenuUI = GameObject.Find("PauseMenu");
        pauseMenuUI.SetActive(false);
    }

    private void OnPause(InputAction.CallbackContext context)
    {
            Pause();
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide pause menu
        Time.timeScale = 1f; // Resume game time
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Show pause menu
        Time.timeScale = 0f; // Pause game time
        isPaused = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MENU_TEXT); // Load main menu
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application
    }
}
