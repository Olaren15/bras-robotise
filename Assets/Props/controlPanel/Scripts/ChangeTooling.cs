using System;
using UnityEngine;

public class ChangeTooling : MonoBehaviour
{
    public GameObject suctionTool;
    public GameObject plasmaTorch;
    public int index;

    public ActivateTooling activateTooling;
    private SequencePlayer sequencePlayer;
    private SequenceRecorder sequenceRecorder;


    private void Start()
    {
        sequencePlayer = FindObjectOfType<SequencePlayer>();
        sequenceRecorder = FindObjectOfType<SequenceRecorder>();

    }

    public void CycleThroughToolings()
    {
        if (!sequencePlayer.playing && !sequenceRecorder.recording)
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
            ChangeTool(index);
            
        }
    }

    public void ChangeTool(int toolingId)
    {
        index = toolingId;
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