using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    // [SerializeField] public GameObject DontDestroy;
    private PlayerHealth playerHealth;

    private void Start(){
        playerHealth = GetComponent<PlayerHealth>();
        // playerHealth.DontDestroy.SetActive(false);
    }

   

    public void GoToScene(string sceneName){

        SceneManager.LoadScene(sceneName);
        playerHealth.DontDestroy.SetActive(true);
   


    }
    public void QuitApp(){
        Application.Quit();
        Debug.Log("Appication has quit");
    }
}
