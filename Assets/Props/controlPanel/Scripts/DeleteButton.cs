using UnityEngine;

namespace Props.controlPanel.Scripts
{
    public class DeleteButton : MonoBehaviour
    {
        private SequencePlayer sequencePlayer;
        private SequenceRecorder sequenceRecorder;
        private SequenceList sequenceList;

        private void Start()
        {
            sequenceRecorder = FindObjectOfType<SequenceRecorder>();
            sequencePlayer = FindObjectOfType<SequencePlayer>();
            sequenceList = FindObjectOfType<SequenceList>();
        }

        public void Delete()
        {
            if (!sequencePlayer.playing && !sequenceRecorder.recording)
            {
                sequenceList.DeleteSelectedSequence();
            }
        }
    }
}