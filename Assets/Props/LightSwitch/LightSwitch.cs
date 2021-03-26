using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] lights;
    private GameObject[] lightCubes;

    

    private void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("Light");
        lightCubes = GameObject.FindGameObjectsWithTag("LightCube");

    }

    public void TurnOffLights()
    {
        foreach (var light in lights)
        {
            light.SetActive(false);
        }
        foreach (var lightCube in lightCubes)
        {
            lightCube.SetActive(false);
        }
    }

    public void TurnOnLights()
    {
        foreach (var light in lights)
        {
            light.SetActive(true);
        }
        foreach (var lightCube in lightCubes)
        {
            lightCube.SetActive(true);
        }
    }
}
