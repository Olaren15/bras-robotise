using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SuctionTooling : MonoBehaviour
{
    private const float MaxRaycastDistance = 0.05f;

    private GameObject grabbedObject;
    private Transform originalParent;

    private Hand[] hands;

    private void Start()
    {
        hands = FindObjectsOfType<Hand>();
    }

    public void Activate(bool active)
    {
        if (active)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward * -1),
                out RaycastHit hit, MaxRaycastDistance))
            {
                grabbedObject = hit.rigidbody.gameObject;

                if (grabbedObject.GetComponent<Throwable>() && !grabbedObject.CompareTag("Target"))
                {
                    foreach (Hand hand in hands)
                    {
                        // detach object from hand if held
                        hand.DetachObject(grabbedObject);
                    }

                    grabbedObject.AddComponent<IgnoreHovering>();
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

                    foreach (Transform child in grabbedObject.transform)
                    {
                        child.gameObject.AddComponent<IgnoreHovering>();
                    }

                    grabbedObject.transform.position += transform.TransformDirection(Vector3.forward) * hit.distance;

                    originalParent = grabbedObject.transform.parent;
                    grabbedObject.transform.parent = transform;
                }
            }
        }
        else
        {
            if (grabbedObject)
            {
                Destroy(grabbedObject.GetComponent<IgnoreHovering>());
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;

                foreach (Transform child in grabbedObject.transform)
                {
                    Destroy(child.gameObject.GetComponent<IgnoreHovering>());
                }

                grabbedObject.transform.parent = originalParent;
            }
        }
    }
}