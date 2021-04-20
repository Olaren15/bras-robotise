using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public GameObject target;
    public GameObject targetRotation;
    private Vector3 initialTargetPosition;
    private Vector3 initialTargetRotationPosition;
    private Movement movement;

    
    private void Start()
    {
        initialTargetPosition = target.transform.position;
        initialTargetRotationPosition = targetRotation.transform.position;
        movement = FindObjectOfType<Movement>();
    }

    public void ResetRobotPosition()
    {
        target.transform.position = initialTargetPosition;
        targetRotation.transform.position = initialTargetRotationPosition;
        targetRotation.transform.rotation = Quaternion.identity;
        
        movement.J6Rotation = 0;
        movement.J5WristRotation = 0;
        movement.J4ArmRotation = 0;
        movement.J3HousingRotation = 0;
        movement.J2ArmRotation = 0;
        movement.J2BaseRotation = 0;


    }
}
