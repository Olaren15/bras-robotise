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
        sequence = sequenceToPlay;

        if (sequence.Count >= 1)
        {
            playing = true;
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
                float distance =
                    Vector3.Distance(target.transform.position, sequence[nextSequenceIndex].TargetPosition);
                float distanceRotation = Vector3.Distance(targetRotation.transform.position,
                    sequence[nextSequenceIndex].TargetRotationPosition);

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
                    sequence[nextSequenceIndex].TargetPosition, speed * ratio * Time.deltaTime);

                targetRotation.transform.position = Vector3.MoveTowards(targetRotation.transform.position,
                    sequence[nextSequenceIndex].TargetRotationPosition, speed * ratioRotation * Time.deltaTime);
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