using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LinkedBuzzer : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D light2D;
    public Sprite GreenLight;
    [SerializeField] GameObject objectToDestroy;

    public bool buzzerActive; // New public variable to store buzzer state
    private FinalGate finalGate;
    // public Action<LinkedBuzzer> OnBuzzerActivated { get; internal set; }

    private void Start()
    {
        finalGate = objectToDestroy.GetComponent<FinalGate>();
        // Ensure the light is off at the start
        if (light2D != null)
        {
            light2D.enabled = false;
        }
        else
        {
            Debug.LogError("Light2D component not set.");
        }

        // Initialize buzzerActive to false initially
        buzzerActive = false;

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.GetComponent<DamageSource>())&&(!buzzerActive))
        {
            if (light2D != null)
            {
                light2D.enabled = true;
            }

            this.gameObject.GetComponent<SpriteRenderer>().sprite = GreenLight;
            

            // Set buzzerActive to true upon triggering
            buzzerActive = true;
            
            Debug.Log("BuzzerActive");
            finalGate.activeBuzzerCount++; 
            Debug.Log(finalGate.activeBuzzerCount);
            
        }
    }
 
}
