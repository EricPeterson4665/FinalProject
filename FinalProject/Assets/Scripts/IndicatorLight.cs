using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorLight : MonoBehaviour
{
    public Light pointLight;
    public Material onMaterial;
    public float lightIntensity = 10;
    
    public void ActivateLight()
    {
        var light = GetComponent<MeshRenderer>();
        light.material = onMaterial;
        pointLight.intensity = lightIntensity;
    }
    
}
