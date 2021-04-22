using System.Collections.Generic;
using Robot_arm.Scripts;
using UnityEngine;

public class SequencePlayer : MonoBehaviour
{
    public bool playing { get; private set; }

    private List<KeyPoint> sequence;
    private int nextSequenceIndex;

    public GameObject target;
    public GameObject targetRotation;

    public float speed { get; set; } = 0.3f;

    public void Play(List<KeyPoint> sequenceToPlay)
    {
        playing = true;
        sequence = sequenceToPlay;

        if (sequence.Count >= 1)
        {
            target.transform.position = sequence[0].TargetPosition;
            targetRotation.transform.position = sequence[0].TargetRotationPosition;
            targetRotation.transform.rotation = Quaternion.identity;
            targetRotation.transform.parent = target.transform.parent;

            if (sequence.Count >= 2)
            {
                nextSequenceIndex = 1;
            }
        }
    }

    private void Update()
    {
        if (playing)
        {
            if (target.transform.position != sequence[nextSequenceIndex].TargetPosition
                || targetRotation.transform.position != sequence[nextSequenceIndex].TargetRotationPosition)
            {
                target.transform.position = Vector3.MoveTowards(target.transform.position,
                    sequence[nextSequenceIndex].TargetPosition, speed * Time.deltaTime);

                float distance =
                    Vector3.Distance(target.transform.position, sequence[nextSequenceIndex].TargetPosition);
                float distanceRotation = Vector3.Distance(targetRotation.transform.position,
                    sequence[nextSequenceIndex].TargetRotationPosition);

                float ratio = distanceRotation / distance;

                targetRotation.transform.position = Vector3.MoveTowards(targetRotation.transform.position,
                    sequence[nextSequenceIndex].TargetRotationPosition, speed * ratio * Time.deltaTime);
            }
            else if (sequence.Count > nextSequenceIndex + 1)
            {
                nextSequenceIndex++;
            }
            else
            {
                playing = false;
                nextSequenceIndex = 0;
                targetRotation.transform.parent = target.transform;
            }
        }
    }
}