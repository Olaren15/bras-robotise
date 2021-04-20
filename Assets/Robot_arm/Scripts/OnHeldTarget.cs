using System;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class OnHeldTarget : MonoBehaviour
{
    public SteamVR_Action_Boolean steamVRActionBoolean;
    private Hand[] hands;
    private SequenceRecorder sequenceRecorder;

    private void Start()
    {
        hands = FindObjectsOfType<Hand>();
        sequenceRecorder = FindObjectOfType<SequenceRecorder>();
    }

    public void OnHeldUpdate()
    {
        foreach (var hand in hands)
        {
            if (steamVRActionBoolean.GetStateDown(hand.handType))
            {
                sequenceRecorder.SaveKeyPoint(transform);
                
            }

        }
    }
}