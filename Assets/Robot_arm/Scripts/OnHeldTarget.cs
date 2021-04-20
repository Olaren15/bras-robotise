using System;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class OnHeldTarget : MonoBehaviour
{
    public SteamVR_Action_Boolean steamVRActionBoolean;
    public TargetRangeDetector range;
    public TargetRangeDetector rotationRange;

    private Hand[] hands;
    private SequenceRecorder sequenceRecorder;
    public Transform targetTransform;
    public Transform targetRotationTransform;

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
                if (range.isInRange && rotationRange.isInRange)
                {
                    sequenceRecorder.SaveKeyPoint(targetTransform, targetRotationTransform);
                }
            }
        }
    }
}