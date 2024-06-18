using System.Collections.Generic;
using UnityEngine;

public class FinalGate : MonoBehaviour
{
    [SerializeField] private List<LinkedBuzzer> buzzers; // Array of LinkedBuzzer references
    [SerializeField] private GameObject gate; // Reference to the gate GameObject

    public int activeBuzzerCount; // Track the number of activated buzzers

    private void Start()
    {
        if (buzzers == null || buzzers.Count == 0)
        {
            Debug.LogError("No LinkedBuzzer game objects assigned!");
            return;
        }

        if (gate == null)
        {
            Debug.LogError("Gate GameObject not assigned!");
            return;
        }

        // Initialize activeBuzzerCount
        activeBuzzerCount = 0;
    }

    private void Update()
    {
        // Check if all buzzers are active
        if (activeBuzzerCount == buzzers.Count)
        {
            Destroy(gate);
            Debug.Log("Gate destroyed!");
        }
    }

    // Handle individual buzzer activation through event or custom method call
    /*public void OnBuzzerActivated(LinkedBuzzer activatedBuzzer)
    {
        if (activatedBuzzer.buzzerActive)
        {
            activeBuzzerCount++;
            Debug.Log("activeBuzzerCount increased!");
        }
        else
        {
            activeBuzzerCount--; // Decrement for deactivation (optional for flexibility)
        }
    }
    */
}
