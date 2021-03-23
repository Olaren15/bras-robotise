using UnityEngine;

public class PositionLocalConstraints : MonoBehaviour
{
    public bool freezeX;
    public bool freezeY;
    public bool freezeZ;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        float x = freezeX ? initialPosition.x : transform.localPosition.x;
        float y = freezeY ? initialPosition.y : transform.localPosition.y;
        float z = freezeZ ? initialPosition.z : transform.localPosition.z;
        
        transform.localPosition = new Vector3(x, y, z);
        
        transform.localRotation = initialRotation;
    }
}