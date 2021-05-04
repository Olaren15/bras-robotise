using UnityEngine;
using Valve.VR.InteractionSystem;

public class SequencePlayer : MonoBehaviour
{
    public bool playing { get; private set; }

    private Sequence sequence;
    private int nextSequenceIndex;

    public GameObject target;
    public GameObject targetRotation;

    public int speed = 3;
    private ActivateTooling activateTooling;
    private ChangeTooling changeTooling;
    
    private void Start()
    {
        activateTooling = FindObjectOfType<ActivateTooling>();
        changeTooling = FindObjectOfType<ChangeTooling>();
    }

    public void Play(Sequence sequenceToPlay)
    {
        sequence = sequenceToPlay;

        if (sequence.keyPoints.Count >= 1)
        {
            playing = true;
            target.transform.position = sequence.keyPoints[0].TargetPosition;
            targetRotation.transform.position = sequence.keyPoints[0].TargetRotationPosition;
            targetRotation.transform.rotation = Quaternion.identity;
            targetRotation.transform.parent = target.transform.parent;
            
            changeTooling.ChangeTool(sequence.toolingId);
            
            if (activateTooling.active != sequence.keyPoints[0].toolActivated)
            {
                activateTooling.ActivateTool(sequence.keyPoints[0].toolActivated);
            }
            
            target.AddComponent<IgnoreHovering>();
            targetRotation.AddComponent<IgnoreHovering>();

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
                if (activateTooling.active != sequence.keyPoints[nextSequenceIndex].toolActivated)
                {
                    activateTooling.ActivateTool(sequence.keyPoints[nextSequenceIndex].toolActivated);
                }
                nextSequenceIndex++;

            }
            else
            {
                if (activateTooling.active != sequence.keyPoints[nextSequenceIndex].toolActivated)
                {
                    activateTooling.ActivateTool(sequence.keyPoints[nextSequenceIndex].toolActivated);
                }
                playing = false;
                nextSequenceIndex = 0;
                targetRotation.transform.parent = target.transform;

                Destroy(target.GetComponent<IgnoreHovering>());
                Destroy(targetRotation.GetComponent<IgnoreHovering>());
            }
        }
    }
}