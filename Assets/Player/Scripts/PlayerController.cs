using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 2;
    private CharacterController characterController;
    public Transform vrCamera;
    private float playerHeight;

    public bool EnableMovement;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        playerHeight = vrCamera.position.y - transform.position.y;
        characterController.center = new Vector3(vrCamera.localPosition.x, playerHeight / 2, vrCamera.localPosition.z);
        characterController.height = playerHeight;

        if (EnableMovement)
        {
            Vector3 direction =
                Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) -
                                     new Vector3(0, 9.81f, 0) * Time.deltaTime);
        }
    }
}