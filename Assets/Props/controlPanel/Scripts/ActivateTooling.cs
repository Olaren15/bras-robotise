using UnityEngine;

public class ActivateTooling : MonoBehaviour
{
    private bool active;

    public TorchTooling torchTooling;
    public SuctionTooling suctionTooling;

    public Material defaultMaterial;
    public Material pressedMaterial;

    private MeshRenderer meshRenderer;
    private SequencePlayer sequencePlayer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sequencePlayer = FindObjectOfType<SequencePlayer>();

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
}