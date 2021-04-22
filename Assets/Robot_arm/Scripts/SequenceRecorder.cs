using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Robot_arm.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class SequenceRecorder : MonoBehaviour
{
    public bool recording { get; private set; }
    public GameObject keyPointPrefab;
    private List<KeyPoint> sequence = new List<KeyPoint>();
    private List<GameObject> sequenceGameObjects = new List<GameObject>();

    public void StartRecording()
    {
        SetRecording(true);

        foreach (GameObject gameObject in sequenceGameObjects)
        {
            Destroy(gameObject);
        }

        sequenceGameObjects.Clear();
    }

    public List<KeyPoint> StopRecording()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/sequence.dat", FileMode.Create);
        binaryFormatter.Serialize(file, sequence);
        file.Close();

        if (File.Exists(Application.persistentDataPath + "/sequence.dat"))
        {
            BinaryFormatter binaryFormatter1 = new BinaryFormatter();
            FileStream file1 = File.Open(Application.persistentDataPath + "/sequence.dat", FileMode.Open);
            var savedData = binaryFormatter1.Deserialize(file1) as List<KeyPoint>;
            file1.Close();

            for (int i = 0; i < savedData.Count; i++)
            {
                print("Target position " + i);
                print(savedData[i].TargetPosition);
                print("Target rotation position " + i);
                print(savedData[i].TargetRotationPosition);
            }
        }

        SetRecording(false);
        var newSequence = sequence.ToList();
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
        sequence.RemoveAt(sequence.Count - 1);
        Destroy(sequenceGameObjects[sequenceGameObjects.Count - 1]);
        sequenceGameObjects.RemoveAt(sequenceGameObjects.Count - 1);
    }
}