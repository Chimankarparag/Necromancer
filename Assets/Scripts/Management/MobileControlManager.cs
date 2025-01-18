using UnityEngine;

public class MobileControlManager : MonoBehaviour
{
    public GameObject mobileControls; // Reference to the GameObject holding mobile UI

    void Start()
    {
        // Check the platform
        if (Application.isMobilePlatform)
        {
            // Enable mobile controls if on a mobile platform
            mobileControls.SetActive(true);
        }
        else
        {
            // Disable mobile controls if on PC, Mac, or Console
            mobileControls.SetActive(false);
        }
    }
}
