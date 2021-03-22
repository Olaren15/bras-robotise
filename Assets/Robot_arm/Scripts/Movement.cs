using UnityEngine;

[ExecuteInEditMode]
public class Movement : MonoBehaviour
{
    [Range(-360, 360)] public float j2BaseRotation;
    [Range(-100, 130)] public float j2ArmRotation;
    [Range(-100, 65)] public float j3HousingRotation;
    [Range(-180, 180)] public float j4ArmRotation;
    [Range(-90, 90)] public float j5WristRotation;
    [Range(-360, 360)] public float j6Rotation;

    private GameObject J2Base;
    private GameObject J2Arm;
    private GameObject J3Housing;
    private GameObject J4Arm;
    private GameObject J5Wrist;
    private GameObject J6;

    private void Start()
    {
        J2Base = GameObject.Find("j2base-rotation");
        J2Arm = GameObject.Find("j2arm-rotation");
        J3Housing = GameObject.Find("j3housing-rotation");
        J4Arm = GameObject.Find("j4arm");
        J5Wrist = GameObject.Find("j5wrist-rotation");
        J6 = GameObject.Find("j6");
    }

    private void Update()
    {
        J2Base.transform.rotation = Quaternion.Euler(0, j2BaseRotation, 0);
        J2Arm.transform.rotation = J2Base.transform.rotation * Quaternion.Euler(0, 0, j2ArmRotation);
        J3Housing.transform.rotation = J2Arm.transform.rotation * Quaternion.Euler(0, 0, j3HousingRotation);
        J4Arm.transform.rotation = J3Housing.transform.rotation * Quaternion.Euler(j4ArmRotation, 0, 0);
        J5Wrist.transform.rotation = J4Arm.transform.rotation * Quaternion.Euler(0, 0, j5WristRotation);
        J6.transform.rotation = J5Wrist.transform.rotation * Quaternion.Euler(j6Rotation, 0, 90);
    }
}