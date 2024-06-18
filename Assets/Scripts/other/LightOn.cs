using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;  // Make sure you have the Universal RP installed

public class LightTrigger : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D light2D;  // Reference to the Light2D component
    public GameObject Player;  // Reference to the player object

    private void Start()
    {
        // Ensure the light is off at the start
        if (light2D != null)
        {
            light2D.enabled = false;
        }
        else
        {
            Debug.LogError("Light2D component not set.");
        }
    }

    // This method is called when another collider enters the trigger collider attached to the object this script is attached to
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the collider
        if (other.gameObject == Player)
        {
            // Turn on the light
            if (light2D != null)
            {
                light2D.enabled = true;
            }
        }
    }
}
