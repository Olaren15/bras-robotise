using UnityEngine;
using Valve.VR.InteractionSystem;

public class VRControlsManager : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject[] teleportGameObjects;
    private SnapTurn snapTurn;
    private Teleport teleport;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        teleportGameObjects = GameObject.FindGameObjectsWithTag("Teleportation");
        snapTurn = FindObjectOfType<SnapTurn>();
        teleport = FindObjectOfType<Teleport>();
    }

    public void SetMovementTeleportation()
    {
        SetTeleportObjectsActive(true);
        SetContinuousMovementActive(false);
        SetLeftHandSnapTurnActive(true);
        SetLeftHandTeleportActive(true);
    }

    public void SetMovementContinuous()
    {
        SetTeleportObjectsActive(false);
        SetContinuousMovementActive(true);
        SetLeftHandSnapTurnActive(false);
        SetLeftHandTeleportActive(false);
    }

    public void SetMovementHybrid()
    {
        SetTeleportObjectsActive(true);
        SetContinuousMovementActive(true);
        SetLeftHandSnapTurnActive(false);
        SetLeftHandTeleportActive(false);
    }

    private void SetTeleportObjectsActive(bool active)
    {
        foreach (GameObject teleportationGameObject in teleportGameObjects)
        {
            teleportationGameObject.SetActive(active);
        }
    }

    private void SetContinuousMovementActive(bool active)
    {
        playerController.EnableMovement = active;
    }

    private void SetLeftHandSnapTurnActive(bool active)
    {
        snapTurn.enableLeftHand = active;
    }

    private void SetLeftHandTeleportActive(bool active)
    {
        teleport.EnableTeleportOnLeftHand = active;
    }
}