using UnityEngine;

public class RecordButton : MonoBehaviour
{
    private bool active;
    private SequenceRecorder sequenceRecorder;
    private SequencePlayer sequencePlayer;
    private SequenceList sequenceList;
    private MeshRenderer meshRenderer;

    public Material defaultMaterial;
    public Material pressedMaterial;

    private void Start()
    {
        sequenceRecorder = FindObjectOfType<SequenceRecorder>();
        sequencePlayer = FindObjectOfType<SequencePlayer>();
        sequenceList = FindObjectOfType<SequenceList>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ToggleRecord()
    {
        if (active)
        {
            sequenceList.AddSequence(sequenceRecorder.StopRecording());
            active = !active;
        }
        else if (!sequencePlayer.playing)
        {
            sequenceRecorder.StartRecording();
            active = !active;
        }

        meshRenderer.material = active ? pressedMaterial : defaultMaterial;
    }
}