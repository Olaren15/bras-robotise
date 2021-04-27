using System.Collections.Generic;
using UnityEngine;

public class ToggleShow : MonoBehaviour
{
    private GameObject[] mainScreenButtons;
    private GameObject[] settingsScreenButtons;

    public GameObject sequenceListCanvas;

    public void Start()
    {
        settingsScreenButtons = GameObject.FindGameObjectsWithTag("ButtonSettings");
        mainScreenButtons = GameObject.FindGameObjectsWithTag("ButtonMain");
        GoToMain();
    }

    public void GoToSettings()
    {
        ToggleButtons(mainScreenButtons, false);
        ToggleButtons(settingsScreenButtons, true);
        sequenceListCanvas.SetActive(false);
    }

    public void GoToMain()
    {
        ToggleButtons(settingsScreenButtons, false);
        ToggleButtons(mainScreenButtons, true);
        sequenceListCanvas.SetActive(true);
    }

    private static void ToggleButtons(IEnumerable<GameObject> buttons, bool enable)
    {
        foreach (GameObject button in buttons)
        {
            var boxColliders = button.GetComponents<BoxCollider>();
            foreach (BoxCollider boxCollider in boxColliders)
            {
                boxCollider.enabled = enable;
            }

            PositionLocalConstraints localConstraints = button.GetComponent<PositionLocalConstraints>();
            if (localConstraints)
            {
                localConstraints.freezeY = true;
            }

            button.GetComponent<MeshRenderer>().enabled = enable;
        }
    }
}