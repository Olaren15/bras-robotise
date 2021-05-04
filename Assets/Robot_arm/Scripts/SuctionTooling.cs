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
                if (hit.rigidbody.gameObject.GetComponent<Throwable>() &&
                    !hit.rigidbody.gameObject.CompareTag("Target"))
                {
                    grabbedObject = hit.rigidbody.gameObject;

                    foreach (Hand hand in hands)
                    {
                        // detach object from hand if held
                        hand.DetachObject(grabbedObject);
                    }

                    EnableIgnoreHovering(true, grabbedObject);
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

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
                EnableIgnoreHovering(false, grabbedObject);
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;

                grabbedObject.transform.parent = originalParent;
                grabbedObject = null;
            }
        }
    }

    private void EnableIgnoreHovering(bool enable, GameObject gameObject1)
    {
        if (enable)
        {
            gameObject1.AddComponent<IgnoreHovering>();
        }
        else
        {
            Destroy(gameObject1.GetComponent<IgnoreHovering>());
        }

        foreach (Transform transform1 in gameObject1.transform)
        {
            EnableIgnoreHovering(enable, transform1.gameObject);
        }
    }
}