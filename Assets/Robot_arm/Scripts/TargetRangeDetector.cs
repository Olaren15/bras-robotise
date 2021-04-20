using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TargetRangeDetector : MonoBehaviour
{
    public GameObject target;
    public bool isInRange = true;
    public SteamVR_Action_Vibration haptics;
    public float vibrationDuration = 1;
    private float timeUntilNextVibration;

    public float vibrationFrequency = 150;

    private Hand[] hands;
    private SphereCollider sphereCollider;

    public Material outOfRangeMaterial;
    private Material originalMaterial;

    private MeshRenderer rangeRenderer;

    private void Start()
    {
        hands = FindObjectsOfType<Hand>();
        sphereCollider = GetComponent<SphereCollider>();
        originalMaterial = target.GetComponent<MeshRenderer>().material;
        rangeRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (!isInRange)
        {
            if (timeUntilNextVibration <= 0)
            {
                foreach (var hand in hands)
                {
                    if (hand.currentAttachedObject == target)
                    {
                        float distance = Vector3.Distance(target.transform.position, transform.position);
                        distance -= sphereCollider.radius * transform.localScale.x;
                        haptics.Execute(0, vibrationDuration, vibrationFrequency, distance, hand.handType);
                        timeUntilNextVibration = vibrationDuration * 1.2f;
                    }
                }
            }
            else
            {
                timeUntilNextVibration -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            isInRange = false;
            target.GetComponent<MeshRenderer>().material = outOfRangeMaterial;
            rangeRenderer.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            isInRange = true;
            target.GetComponent<MeshRenderer>().material = originalMaterial;
            rangeRenderer.enabled = false;
        }
    }
}