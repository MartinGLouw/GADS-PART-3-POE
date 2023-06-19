using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light flickerLight;
    public float minIntensity = 0.25f;
    public float maxIntensity = 8f;
    public float flickerSpeed = 1f;

    void Update()
    {
        if (Random.value < flickerSpeed)
        {
            flickerLight.intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}