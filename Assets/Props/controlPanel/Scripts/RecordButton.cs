using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordButton : MonoBehaviour
{
    private bool active;
    private SequenceRecorder sequenceRecorder;

    void Start()
    {
        sequenceRecorder = FindObjectOfType<SequenceRecorder>();

    }

    public void ToggleRecord()
    {
        if (active)
        {
            sequenceRecorder.StopRecording();
        }
        else
        {
            sequenceRecorder.StartRecording();
        }
        active = !active;

    }
}
