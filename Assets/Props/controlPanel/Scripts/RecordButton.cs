using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordButton : MonoBehaviour
{
    private bool active;
    private SequenceRecorder sequenceRecorder;
    private SequencePlayer sequencePlayer;
    private SequenceList sequenceList;

    private void Start()
    {
        sequenceRecorder = FindObjectOfType<SequenceRecorder>();
        sequencePlayer = FindObjectOfType<SequencePlayer>();
        sequenceList = FindObjectOfType<SequenceList>();
    }

    public void ToggleRecord()
    {
        if (active)
        {
            sequenceList.currentSequence = sequenceRecorder.StopRecording();
        }
        else
        {
            if (!sequencePlayer.playing)
            {
                sequenceRecorder.StartRecording();
            }
        }

        active = !active;
    }
}