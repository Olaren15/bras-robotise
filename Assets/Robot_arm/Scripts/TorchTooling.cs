using UnityEngine;

public class TorchTooling : MonoBehaviour
{
    public GameObject particleEffect;

    public void Activate(bool active)
    {
        particleEffect.SetActive(active);
    }
}