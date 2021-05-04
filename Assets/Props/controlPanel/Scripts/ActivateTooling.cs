using UnityEngine;

public class ActivateTooling : MonoBehaviour
{
    private bool active;

    public TorchTooling torchTooling;
    public SuctionTooling suctionTooling;

    public Material defaultMaterial;
    public Material pressedMaterial;

    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ToggleTooling()
    {
        active = !active;
        meshRenderer.material = active ? pressedMaterial : defaultMaterial;

        if (torchTooling.enabled)
        {
            torchTooling.Activate(active);
        }
        else if (suctionTooling.enabled)
        {
            suctionTooling.Activate(active);
        }
    }
}