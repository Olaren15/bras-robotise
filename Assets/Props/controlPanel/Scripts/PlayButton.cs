using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private SequenceList sequenceList;
    private SequencePlayer sequencePlayer;
    private SequenceRecorder sequenceRecorder;
    private ResetButton reset;

    private void Start()
    {
        sequenceList = FindObjectOfType<SequenceList>();
        sequencePlayer = FindObjectOfType<SequencePlayer>();
        sequenceRecorder = FindObjectOfType<SequenceRecorder>();
        reset = FindObjectOfType<ResetButton>();
    }

    public void Play()
    {
        if (sequenceList.currentSequence != null && !sequenceRecorder.recording && !sequencePlayer.playing)
        {
            reset.ResetRobotPosition();
            sequencePlayer.Play(sequenceList.currentSequence.keyPoints);
        }
    }
}