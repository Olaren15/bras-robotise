using UnityEngine;

public class basket : MonoBehaviour
{
    public GameObject confetti;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(confetti, GameObject.Find("ConfettiSpawner").transform.position, Quaternion.Euler(90, 0, 0));
    }
}