using System.Collections.Generic;
using System.Linq;
using Robot_arm.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class SequenceRecorder : MonoBehaviour
{
    private bool recording;
    public GameObject keyPointPrefab;
    private List<KeyPoint> sequence = new List<KeyPoint>();
    private List<GameObject> sequenceGameObjects = new List<GameObject>();

    public void StartRecording()
    {
        SetRecording(true);
    }

    public List<KeyPoint> StopRecording()
    {
        SetRecording(false);
        List<KeyPoint> newSequence = sequence.ToList();
        sequence.Clear();
        return newSequence;
    }

    private void SetRecording(bool recording)
    {
        this.recording = recording;
    }

    public void SaveKeyPoint(Transform targetTransform, Transform targetRotationTransform)
    {
        if (recording)
        {
            GameObject keyPoint = Instantiate(keyPointPrefab, targetTransform.position, Quaternion.identity);
            keyPoint.transform.Find("TargetRotation").transform.position = targetRotationTransform.position;
            keyPoint.transform.Find("Canvas").Find("Text").GetComponent<Text>().text = (sequence.Count + 1).ToString();

            sequenceGameObjects.Add(keyPoint);
            AddKeyPointToSequence(targetTransform.position, targetRotationTransform.position);
        }
    }

    private void AddKeyPointToSequence(Vector3 targetPosition, Vector3 targetRotationPosition)
    {
        KeyPoint keyPoint = new KeyPoint(targetPosition, targetRotationPosition);
        sequence.Add(keyPoint);
    }

    public void Undo()
    {
        if (sequence.Count > 0)
        {
            RemoveLastKeyPoint();
        }
    }

    private void RemoveLastKeyPoint()
    {
        sequence.RemoveAt(sequence.Count);
        sequenceGameObjects.RemoveAt(sequenceGameObjects.Count);
    }
}