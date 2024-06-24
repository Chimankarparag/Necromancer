using UnityEngine;

public class CaveEntrance : MonoBehaviour
{
    [SerializeField] private Light2DController lightController;
    [SerializeField] private bool isOn=true;
    [SerializeField] private float reduceLightAreaMultiplier = 0.5f;
    [SerializeField] private float restoreLightAreaMultiplier = 1.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player){
            if (isOn){
                lightController.SetLightAreaMultiplier(reduceLightAreaMultiplier);
            }else{
                lightController.SetLightAreaMultiplier(restoreLightAreaMultiplier);
            }
        }

    }

}
