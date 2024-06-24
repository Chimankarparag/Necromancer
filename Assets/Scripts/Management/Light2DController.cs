using UnityEngine;


public class Light2DController : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.Light2D light2D;
    private float originalLightArea;
    private float currentLightAreaMultiplier = 1.0f;

    private void Start()
    {
        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        originalLightArea = light2D.pointLightOuterRadius;
    }

    public void SetLightAreaMultiplier(float multiplier)
    {
        light2D.pointLightOuterRadius = originalLightArea * multiplier;
        currentLightAreaMultiplier = multiplier;
    }

    public void ResetLightAreaMultiplier()
    {
        light2D.pointLightOuterRadius = originalLightArea;
        currentLightAreaMultiplier = 1.0f;
    }
}
