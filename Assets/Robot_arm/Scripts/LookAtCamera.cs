using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, Vector3.up);
    }
}