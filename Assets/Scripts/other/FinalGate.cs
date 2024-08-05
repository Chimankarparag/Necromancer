using System.Collections.Generic;
using UnityEngine;

public class FinalGate : MonoBehaviour
{
    [SerializeField] private GameObject gate; // Reference to the gate GameObject

    public int activeBuzzerCount; // Track the number of activated buzzers

    private void Start()
    {
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
        if (activeBuzzerCount == 4)
        {
            Debug.Log("Gate destroyed!");
            Destroy(gate);
            // Debug.Log("Gate destroyed!");
        }
    }

}
