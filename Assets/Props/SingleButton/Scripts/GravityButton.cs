using System.Collections.Generic;
using UnityEngine;

public class GravityButton : MonoBehaviour
{
    private bool active = true;

    private readonly List<Light> lights = new List<Light>();

    private void Start()
    {
        foreach (GameObject lightGameObject in GameObject.FindGameObjectsWithTag("Light"))
        {
            lights.Add(lightGameObject.GetComponent<Light>());
        }
    }

    public void ToggleGravity()
    {
        active = !active;
        Physics.gravity = active ? new Vector3(0.0f, -9.81f, 0.0f) : Vector3.zero;

        foreach (Light light1 in lights)
        {
            light1.color = active ? Color.white : Color.red;
        }
    }
}