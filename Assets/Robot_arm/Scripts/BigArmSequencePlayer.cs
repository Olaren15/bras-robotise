using System.Collections;
using System.Collections.Generic;
using Robot_arm.Scripts;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BigArmSequencePlayer : MonoBehaviour
{
    public bool playing { get; private set; }

    private Sequence sequence;
    private int nextSequenceIndex;
    private bool active;

    public GameObject target;
    public GameObject targetRotation;

    public int speed = 3;
    public SuctionTooling suctionTooling;

    private Sequence hardCodedSequence;
    public List<GameObject> keyPoints;
    private void Start()
    {
        List<KeyPoint> listKeypoints = new List<KeyPoint>();
        bool activate = false;
        foreach (var keyPoint in keyPoints)
        {
            if (keyPoint.name == "keyPointBigArmActivate" || keyPoint.name == "keyPointBigArmDeactivate")
            {
                activate = !activate;
            }
            listKeypoints.Add(new KeyPoint(keyPoint.transform.position, keyPoint.transform.Find("TargetRotation").transform.position, activate));
        }
        hardCodedSequence = new Sequence(listKeypoints);
        Play(hardCodedSequence);

    }

    public void Play(Sequence sequenceToPlay)
    {
        sequence = sequenceToPlay;

        if (sequence.keyPoints.Count >= 1)
        {
            playing = true;
            active = false;
            target.transform.position = sequence.keyPoints[0].TargetPosition;
            targetRotation.transform.position = sequence.keyPoints[0].TargetRotationPosition;
            targetRotation.transform.rotation = Quaternion.identity;
            targetRotation.transform.parent = target.transform.parent;

            if (sequence.keyPoints.Count >= 2)
            {
                nextSequenceIndex = 1;
            }
        }
    }

    private void Update()
    {
        if (playing)
        {
            if (target.transform.position != sequence.keyPoints[nextSequenceIndex].TargetPosition
                || targetRotation.transform.position != sequence.keyPoints[nextSequenceIndex].TargetRotationPosition)
            {
                float distance =
                    Vector3.Distance(target.transform.position, sequence.keyPoints[nextSequenceIndex].TargetPosition);
                float distanceRotation = Vector3.Distance(targetRotation.transform.position,
                    sequence.keyPoints[nextSequenceIndex].TargetRotationPosition);

                float ratio;
                float ratioRotation;
                if (distance > distanceRotation)
                {
                    ratio = 1.0f;
                    ratioRotation = distanceRotation / distance;
                }
                else
                {
                    ratio = distance / distanceRotation;
                    ratioRotation = 1.0f;
                }

                target.transform.position = Vector3.MoveTowards(target.transform.position,
                    sequence.keyPoints[nextSequenceIndex].TargetPosition, speed / 10.0f * ratio * Time.deltaTime);

                targetRotation.transform.position = Vector3.MoveTowards(targetRotation.transform.position,
                    sequence.keyPoints[nextSequenceIndex].TargetRotationPosition, speed / 10.0f * ratioRotation * Time.deltaTime);
            }
            else if (sequence.keyPoints.Count > nextSequenceIndex + 1)
            {
                if (active != sequence.keyPoints[nextSequenceIndex].toolActivated)
                {
                    active = !active;
                    suctionTooling.Activate(sequence.keyPoints[nextSequenceIndex].toolActivated);
                }
                nextSequenceIndex++;

            }
            else
            {
                if (active != sequence.keyPoints[nextSequenceIndex].toolActivated)
                {
                    active = !active;
                    suctionTooling.Activate(sequence.keyPoints[nextSequenceIndex].toolActivated);
                }
                nextSequenceIndex = 0;

            }
        }
    }
}
