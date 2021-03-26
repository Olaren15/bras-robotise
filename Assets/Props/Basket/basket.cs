using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basket : MonoBehaviour
{
    public ParticleSystem confetti;
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(confetti, GameObject.Find("ConfettiSpawner").transform.position, Quaternion.Euler(90, 0,0));
    }
}
