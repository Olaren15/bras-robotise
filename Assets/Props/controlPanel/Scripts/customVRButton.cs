using System;
using UnityEngine;
using UnityEngine.Events;

public class customVRButton : MonoBehaviour
{
    [System.Serializable]
    public class ButtonEvent : UnityEvent
    {
    }

    public float pressLength;
    public bool pressed;
    public ButtonEvent downEvent;


    public Material defaultMaterial;
    public Material pressedMaterial;
    private MeshRenderer meshRenderer;

    private Vector3 startPos;

    private SpringJoint springJoint;
    private PositionLocalConstraints localConstraints;

    private void Start()
    {
        startPos = transform.localPosition;
        meshRenderer = GetComponent<MeshRenderer>();
        springJoint = GetComponent<SpringJoint>();
        localConstraints = GetComponent<PositionLocalConstraints>();
    }

    private void Update()
    {
        bool originalPressed = pressed;

        float distance = Mathf.Abs(transform.localPosition.y - startPos.y);
        if (distance >= pressLength)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, startPos.y - pressLength,
                transform.localPosition.z);
        }

        if (distance >= pressLength * 0.75f)
        {
            if (!pressed)
            {
                pressed = true;
                downEvent?.Invoke();
            }
        }
        else
        {
            pressed = false;
        }

        if (originalPressed != pressed)
        {
            meshRenderer.material = pressed ? pressedMaterial : defaultMaterial;
        }

        if (transform.localPosition.y > startPos.y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, startPos.y, transform.localPosition.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        localConstraints.freezeY = false;
    }

    private void OnTriggerExit(Collider other)
    {
        localConstraints.freezeY = true;
    }
}