using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class OverShoulderInventory : MonoBehaviour
{
    private Hand[] hands;
    private List<GameObject> objectsInCollider;
    private GameObject storedItem;
    public SteamVR_Action_Vibration haptics;

    void Start()
    {
        hands = FindObjectsOfType<Hand>();
        objectsInCollider = new List<GameObject>();
    }

    void Update()
    {
        foreach (var hand in hands)
        {
            foreach (var objectInCollider in objectsInCollider)
            {
                if (hand.gameObject.GetInstanceID() == objectInCollider.GetInstanceID())
                {
                    if (hand.IsGrabEnding(hand.currentAttachedObject) && !storedItem &&
                        !hand.currentAttachedObject.CompareTag("Target"))
                    {
                        storedItem = hand.currentAttachedObject;
                        hand.DetachObject(hand.currentAttachedObject);
                        haptics.Execute(0, 0.05f, 100, 1f, hand.handType);
                        storedItem.SetActive(false);
                    }

                    if (hand.GetGrabStarting() != GrabTypes.None && storedItem)
                    {
                        storedItem.SetActive(true);
                        hand.AttachObject(storedItem, GrabTypes.Grip);
                        haptics.Execute(0, 0.05f, 100, 1f, hand.handType);
                        storedItem = null;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        objectsInCollider.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInCollider.Remove(other.gameObject);
    }
}