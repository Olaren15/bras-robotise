using UnityEngine;

public class ActivateTooling : MonoBehaviour
{
    public bool active;

    public TorchTooling torchTooling;
    public SuctionTooling suctionTooling;

    public Material defaultMaterial;
    public Material pressedMaterial;

    private MeshRenderer meshRenderer;
    private SequencePlayer sequencePlayer;
    private SequenceRecorder sequenceRecorder;

    public OnHeldTarget onHeldTarget;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sequencePlayer = FindObjectOfType<SequencePlayer>();
        sequenceRecorder = FindObjectOfType<SequenceRecorder>();

    }

    public void ToggleTooling(bool forceDeactivate = false)
    {
        if (!sequencePlayer.playing)
        {
            if (forceDeactivate)
            {
                active = false;
            }
            else
            {
                active = !active;
            }

            if (sequenceRecorder.recording && !forceDeactivate)
            {
                onHeldTarget.SaveKeyPoint();
            }
            
            ActivateTool(active);
        }
        
    }

    public void ActivateTool(bool activate)
    {
        active = activate;
        meshRenderer.material = active ? pressedMaterial : defaultMaterial;

        if (torchTooling.gameObject.activeSelf)
        {
            torchTooling.Activate(active);
        }
        else if (suctionTooling.gameObject.activeSelf)
        {
            suctionTooling.Activate(active);
        }
    }
    
}