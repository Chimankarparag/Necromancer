using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidGateScript : MonoBehaviour
{
    [SerializeField] private GameObject gate; // The gate GameObject
    [SerializeField] private GameObject guard; // The guard GameObject
    [SerializeField] private Collider2D activationCollider; // The collider to trigger gate activation

    private Transform playerTransform;

    private void Start()
    {
        if (gate == null || guard == null || activationCollider == null)
        {
            Debug.LogError("Gate, Guard or Activation Collider is not assigned.");
            return;
        }

        activationCollider.enabled = false; // Ensure the collider is initially disabled
        playerTransform = PlayerController.Instance.transform; // Get the player transform from the PlayerController
        StartCoroutine(CheckGuardStatus());
    }

    private IEnumerator CheckGuardStatus()
    {
        while (true)
        {
            if (guard == null)
            {
                DeactivateGate();
                break;
            }
            yield return new WaitForSeconds(0.5f); // Check guard status every 0.5 seconds
        }
    }

    private void DeactivateGate()
    {
        gate.SetActive(false);
        activationCollider.enabled = true; // Enable the collider for reactivation
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            ActivateGate();
        }
    }

    private void ActivateGate()
    {
        gate.SetActive(true);
        activationCollider.enabled = false; // Disable the collider to prevent multiple activations
    }
}
