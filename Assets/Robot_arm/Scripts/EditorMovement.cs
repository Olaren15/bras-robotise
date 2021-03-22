using UnityEngine;

[ExecuteInEditMode]
public class EditorMovement : MonoBehaviour
{
    [Range(Movement.J2BaseMinRotation, Movement.J2BaseMaxRotation)]
    public float j2BaseRotation;

    [Range(Movement.J2ArmMinRotation, Movement.J2ArmMaxRotation)]
    public float j2ArmRotation;

    [Range(Movement.J3HousingMinRotation, Movement.J3HousingMaxRotation)]
    public float j3HousingRotation;

    [Range(Movement.J4ArmMinRotation, Movement.J4ArmMaxRotation)]
    public float j4ArmRotation;

    [Range(Movement.J5WristMinRotation, Movement.J5WristMaxRotation)]
    public float j5WristRotation;

    [Range(Movement.J6MinRotation, Movement.J6MaxRotation)]
    public float j6Rotation;

    private Movement movement;

    private void Start()
    {
        movement = GetComponent<Movement>();
    }
    
    private void Update()
    {
        movement.J2BaseRotation = j2BaseRotation;
        movement.J2ArmRotation = j2ArmRotation;
        movement.J3HousingRotation = j3HousingRotation;
        movement.J4ArmRotation = j4ArmRotation;
        movement.J5WristRotation = j5WristRotation;
        movement.J6Rotation = j6Rotation;
    }
}