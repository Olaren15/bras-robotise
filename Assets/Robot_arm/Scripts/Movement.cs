using UnityEngine;

[ExecuteInEditMode]
public class Movement : MonoBehaviour
{
    public const float J2BaseMinRotation = -170f;
    public const float J2BaseMaxRotation = 170f;
    public const float J2ArmMinRotation = -110f;
    public const float J2ArmMaxRotation = 130f;
    public const float J3HousingMinRotation = -122f;
    public const float J3HousingMaxRotation = 75f;
    public const float J4ArmMinRotation = -190f;
    public const float J4ArmMaxRotation = 190f;
    public const float J5WristMinRotation = -120f;
    public const float J5WristMaxRotation = 120f;
    public const float J6MinRotation = -360f;
    public const float J6MaxRotation = 360f;

    private float j2BaseRotation;

    public float J2BaseRotation
    {
        get => j2BaseRotation;
        set => j2BaseRotation = Mathf.Clamp(value, J2BaseMinRotation, J2BaseMaxRotation);
    }

    private float j2ArmRotation;

    public float J2ArmRotation
    {
        get => j2ArmRotation;
        set => j2ArmRotation = Mathf.Clamp(value, J2ArmMinRotation, J2ArmMaxRotation);
    }

    private float j3HousingRotation;

    public float J3HousingRotation
    {
        get => j3HousingRotation;
        set => j3HousingRotation = Mathf.Clamp(value, J3HousingMinRotation, J3HousingMaxRotation);
    }

    private float j4ArmRotation;

    public float J4ArmRotation
    {
        get => j4ArmRotation;
        set => j4ArmRotation = Mathf.Clamp(value, J4ArmMinRotation, J4ArmMaxRotation);
    }

    private float j5WristRotation;

    public float J5WristRotation
    {
        get => j5WristRotation;
        set => j5WristRotation = Mathf.Clamp(value, J5WristMinRotation, J5WristMaxRotation);
    }

    private float j6Rotation;

    public float J6Rotation
    {
        get => j6Rotation;
        set => j6Rotation = Mathf.Clamp(value, J6MinRotation, J6MaxRotation);
    }

    private GameObject j2Base;
    private GameObject j2Arm;
    private GameObject j3Housing;
    private GameObject j4Arm;
    private GameObject j5Wrist;
    private GameObject j6;

    private void Start()
    {
        j2Base = GameObject.Find("j2base-rotation");
        j2Arm = GameObject.Find("j2arm-rotation");
        j3Housing = GameObject.Find("j3housing-rotation");
        j4Arm = GameObject.Find("j4arm-rotation");
        j5Wrist = GameObject.Find("j5wrist-rotation");
        j6 = GameObject.Find("j6");
    }

    private void Update()
    {
        j2Base.transform.rotation = Quaternion.Euler(0, j2BaseRotation, 0);
        j2Arm.transform.rotation = j2Base.transform.rotation * Quaternion.Euler(0, 0, j2ArmRotation);
        j3Housing.transform.rotation = j2Arm.transform.rotation * Quaternion.Euler(0, 0, j3HousingRotation);
        j4Arm.transform.rotation = j3Housing.transform.rotation * Quaternion.Euler(j4ArmRotation, 0, 0);
        j5Wrist.transform.rotation = j4Arm.transform.rotation * Quaternion.Euler(0, 0, j5WristRotation);
        j6.transform.rotation = j5Wrist.transform.rotation * Quaternion.Euler(j6Rotation, 0, 90);
    }
}