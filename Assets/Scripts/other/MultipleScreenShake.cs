using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MultipleScreenShake : Singleton<MultipleScreenShake>
{
    private CinemachineImpulseSource source1;
    private CinemachineImpulseSource source2;

    protected override void Awake() {
        base.Awake();
        
        // Assuming you have two CinemachineImpulseSource components on the same GameObject.
        CinemachineImpulseSource[] sources = GetComponents<CinemachineImpulseSource>();
        
        if (sources.Length >= 2) {
            source1 = sources[0];
            source2 = sources[1];
        } else {
            Debug.LogError("ScreenShakeManager requires at least two CinemachineImpulseSource components.");
        }
    }

    public void ShakeScreenSignal1() {
        if (source1 != null) {
            source1.GenerateImpulse();
        } else {
            Debug.LogWarning("Source1 is not assigned.");
        }
    }

    public void ShakeScreenSignal2() {
        if (source2 != null) {
            source2.GenerateImpulse();
        } else {
            Debug.LogWarning("Source2 is not assigned.");
        }
    }
}
