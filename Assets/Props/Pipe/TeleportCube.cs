using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCube : MonoBehaviour
{
    public GameObject spawner;
    private void OnTriggerEnter(Collider other)
    {
        GameObject cube = other.gameObject;
        Destroy(cube);
        Instantiate(cube, spawner.transform.position, Quaternion.identity);



    }
}
