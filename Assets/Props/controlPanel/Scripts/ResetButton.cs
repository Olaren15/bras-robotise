using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public GameObject target;
    public GameObject targetRotation;
    private Vector3 initialTargetPosition;
    private Vector3 initialTargetRotationPosition;

    private SequencePlayer sequencePlayer;

    private GameObject j2Base;
    private GameObject j2Arm;
    private GameObject j3Housing;
    private GameObject j4Arm;
    private GameObject j5Wrist;
    private GameObject j6;

    private void Start()
    {
        initialTargetPosition = target.transform.position;
        initialTargetRotationPosition = targetRotation.transform.position;

        sequencePlayer = FindObjectOfType<SequencePlayer>();

        //for an absolute reset
        j2Base = GameObject.Find("j2base-rotation");
        j2Arm = GameObject.Find("j2arm-rotation");
        j3Housing = GameObject.Find("j3housing-rotation");
        j4Arm = GameObject.Find("j4arm-rotation");
        j5Wrist = GameObject.Find("j5wrist-rotation");
        j6 = GameObject.Find("j6");
    }

    public void ResetRobotPosition()
    {
        if (!sequencePlayer.playing)
        {
            target.transform.position = initialTargetPosition;
            targetRotation.transform.position = initialTargetRotationPosition;
            targetRotation.transform.rotation = Quaternion.identity;

            j2Base.transform.rotation = Quaternion.identity;
            j2Arm.transform.rotation = Quaternion.identity;
            j3Housing.transform.rotation = Quaternion.identity;
            j4Arm.transform.rotation = Quaternion.identity;
            j5Wrist.transform.rotation = Quaternion.identity;
            j6.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}