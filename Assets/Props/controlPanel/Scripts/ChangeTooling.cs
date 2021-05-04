using UnityEngine;

public class ChangeTooling : MonoBehaviour
{
    public GameObject suctionTool;
    public GameObject plasmaTorch;
    private int index;

    public ActivateTooling activateTooling;

    public void CycleThroughToolings()
    {
        activateTooling.ToggleTooling(true);

        if (index == 2)
        {
            index = 0;
        }
        else
        {
            index++;
        }

        if (index == 1)
        {
            suctionTool.SetActive(true);
            plasmaTorch.SetActive(false);
        }
        else if (index == 2)
        {
            suctionTool.SetActive(false);
            plasmaTorch.SetActive(true);
        }
        else if (index == 0)
        {
            suctionTool.SetActive(false);
            plasmaTorch.SetActive(false);
        }
    }
}