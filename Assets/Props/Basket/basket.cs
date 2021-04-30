using System;
using UnityEngine;

public class basket : MonoBehaviour
{
    public GameObject confetti;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(confetti, GameObject.Find("ConfettiSpawner").transform.position, Quaternion.Euler(90, 0, 0));
        audioSource.Play();
    }
}