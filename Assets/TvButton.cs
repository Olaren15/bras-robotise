using UnityEngine;

public class TvButton : MonoBehaviour
{
    private bool active = true;

    public Material buttonOffMaterial;
    public Material buttonOnMaterial;
    private MeshRenderer meshRenderer;

    public Material tvOffMaterial;
    public Material tvOnMaterial;
    public MeshRenderer tvRenderer;
    

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ToggleTv()
    {
        active = !active;
        meshRenderer.material = active ? buttonOnMaterial : buttonOffMaterial;
        tvRenderer.material = active ? tvOnMaterial : tvOffMaterial;
    } 
}
