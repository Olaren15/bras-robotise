
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class FlashLightToggle : MonoBehaviour
{
    public SteamVR_Action_Boolean steamVRActionBoolean;
    private Hand[] hands;
    private GameObject lightObject;
    private GameObject flashLight;
    public Material unlitMat;
    public Material litMat;
    private MeshRenderer meshRenderer;
    
    private bool lit;

    void Start()
    {
        hands = FindObjectsOfType<Hand>();
        lightObject = GameObject.Find("LightObject").gameObject;
        flashLight = lightObject.transform.Find("Light").gameObject;
        meshRenderer = lightObject.GetComponent<MeshRenderer>();
        flashLight.SetActive(false);
    }

    public void OnHeldUpdate()
    {
        foreach (var hand in hands)
        {
            if (steamVRActionBoolean.GetStateDown(hand.handType))
            {
                if (lit)
                {
                    flashLight.SetActive(false);
                    meshRenderer.material = unlitMat;
                }
                else
                {
                    flashLight.SetActive(true);
                    meshRenderer.material = litMat;

                }

                lit = !lit;
            }
        }
    }
}
