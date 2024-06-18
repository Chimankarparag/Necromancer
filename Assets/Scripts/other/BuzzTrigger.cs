using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BuzzerTrigger : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D light2D;
    public Sprite GreenLight;
    [SerializeField] GameObject objecttodestroy;
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
     private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.GetComponent<DamageSource>()){
             if (light2D != null)
            {
                light2D.enabled = true;
            }
            this.gameObject.GetComponent<SpriteRenderer>().sprite = GreenLight;
            Destroy(objecttodestroy);
            

        }
    }
}
