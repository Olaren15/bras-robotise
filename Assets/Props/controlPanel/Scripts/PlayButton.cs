using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private SequenceList sequenceList;
    private SequencePlayer sequencePlayer;
    private SequenceRecorder sequenceRecorder;

    private void Start()
    {
        sequenceList = FindObjectOfType<SequenceList>();
        sequencePlayer = FindObjectOfType<SequencePlayer>();
        sequenceRecorder = FindObjectOfType<SequenceRecorder>();
    }

    public void Play()
    {
        if (sequenceList.currentSequence != null && !sequenceRecorder.recording && !sequencePlayer.playing)
        {
            sequencePlayer.Play(sequenceList.currentSequence);
        }
    }
}