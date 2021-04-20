using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Robot_arm.Scripts;
using UnityEngine;

public class SequenceRecorder : MonoBehaviour
{
    private bool recording;
    public GameObject keyPointPrefab;
    private List<KeyPoint> sequence = new List<KeyPoint>();

    void Start()
    {
    }

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

    public void SaveKeyPoint(Transform targetTransform)
    {
        if (recording)
        {
            GameObject keyPoint = Instantiate(keyPointPrefab, targetTransform.position, Quaternion.identity);
            Vector3 targetRotationPosition = targetTransform.transform.Find("TargetRotation").transform.position;
            keyPoint.transform.Find("TargetRotation").transform.position = targetRotationPosition;
            AddKeyPointToSequence(targetTransform.position, targetRotationPosition);
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
    }
}