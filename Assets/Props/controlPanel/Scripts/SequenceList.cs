using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SequenceList : MonoBehaviour
{
    private SequenceRecorder sequenceRecorder;
    private SequencePlayer sequencePlayer;

    public GameObject keyPointPrefab;

    private List<Sequence> sequences = new List<Sequence>();
    private int index;

    private List<Sequence> shownSequences = new List<Sequence>();
    private int shownIndex;

    public Sequence currentSequence => sequences.Count > index ? sequences[index] : null;

    public List<GameObject> panels;
    private ChangeTooling changeTooling;


    private void Start()
    {
        changeTooling = FindObjectOfType<ChangeTooling>();

        sequencePlayer = FindObjectOfType<SequencePlayer>();
        sequenceRecorder = FindObjectOfType<SequenceRecorder>();

        if (File.Exists(Application.persistentDataPath + "/sequences.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/sequences.dat", FileMode.Open);
            sequences = binaryFormatter.Deserialize(file) as List<Sequence>;
            file.Close();
        }

        for (int i = 0; i < sequences.Count && i < 6; i++)
        {
            shownSequences.Add(sequences[i]);
        }

        UpdateUi();
    }

    private void UpdateUi()
    {
        for (int i = 0; i < shownSequences.Count && i < panels.Count; i++)
        {
            panels[i].SetActive(true);

            panels[i].GetComponent<Image>().color = i == shownIndex ? new Color(0.5f, 1.0f, 0.0f) : Color.white;
            panels[i].GetComponentInChildren<Text>().text = $"Sequence {shownSequences[i].id}";
        }

        if (shownSequences.Count < panels.Count)
        {
            for (int i = shownSequences.Count; i < panels.Count; i++)
            {
                panels[i].SetActive(false);
            }
        }


        //remove present keypoints
        foreach (var keypoint in GameObject.FindGameObjectsWithTag("Keypoint"))
        {
            Destroy(keypoint);
        }

        if (currentSequence != null)
        {
            changeTooling.ChangeTool(currentSequence.toolingId);

            InstantiateKeyPoints();
        }
    }

    public void AddSequence(Sequence sequence)
    {
        if (sequences.Count == 0)
        {
            sequence.id = 1;
        }
        else
        {
            sequence.id = sequences[sequences.Count - 1].id + 1;
        }

        sequences.Add(sequence);
        index = sequences.Count - 1;

        SaveSequences();

        if (sequences.Count < 6)
        {
            shownSequences.Add(currentSequence);

            if (shownSequences.Count > 0)
            {
                shownIndex = shownSequences.Count - 1;
            }
            else
            {
                shownIndex = 0;
            }
        }
        else
        {
            shownSequences.Clear();
            for (int i = sequences.Count - 6; i < sequences.Count; i++)
            {
                shownSequences.Add(sequences[i]);
            }

            shownIndex = 5;
        }

        UpdateUi();
    }

    public void DeleteSelectedSequence()
    {
        if (sequences.Count > 0)
        {
            sequences.RemoveAt(index);
            shownSequences.RemoveAt(shownIndex);

            //overwrite sequences
            SaveSequences();

            if (shownSequences.Count == 5)
            {
                int nextSequenceIndex = index + 5 - shownIndex;
                int previousSequenceIndex = index - shownIndex - 1;

                //Ajoute une séquence par en bas
                if (sequences.Count > nextSequenceIndex)
                {
                    shownSequences.Add(sequences[nextSequenceIndex]);
                }
                //Si il n'y a pas de séquence plus bas, on ajoute une séquence par en haut
                else if (previousSequenceIndex >= 0)
                {
                    shownSequences.Insert(0, sequences[previousSequenceIndex]);
                    if (index > 0)
                    {
                        index--;
                    }
                }
                //si il n'y a pas de séquence en bas ET en haut
                else
                {
                    //si on est sur la dernière séquence, on remonte
                    if (index == sequences.Count)
                    {
                        index--;
                        shownIndex--;
                    }
                }
            }
            else if (shownIndex > 0)
            {
                //si on est sur la dernière séquence, on remonte
                if (index == sequences.Count)
                {
                    index--;
                    shownIndex--;
                }
            }
        }

        UpdateUi();
    }

    public void SelectUp()
    {
        if (!sequencePlayer.playing && !sequenceRecorder.recording)
        {
            if (index > 0)
            {
                index--;
                ScrollUp();
                UpdateUi();
            }
        }
    }

    public void SelectDown()
    {
        if (!sequencePlayer.playing && !sequenceRecorder.recording)
        {
            if (index < sequences.Count - 1)
            {
                index++;
                ScrollDown();
                UpdateUi();
            }
        }
    }

    private void ScrollUp()
    {
        if (shownIndex == 0 && shownSequences.Count == 6)
        {
            shownSequences.RemoveAt(shownSequences.Count - 1);
            shownSequences.Insert(0, sequences[index]);
        }
        else
        {
            shownIndex--;
        }
    }

    private void ScrollDown()
    {
        if (shownIndex == 5)
        {
            shownSequences.RemoveAt(0);
            shownSequences.Insert(shownSequences.Count, sequences[index]);
        }
        else
        {
            shownIndex++;
        }
    }


    private void InstantiateKeyPoints()
    {
        //add keypoint for the new current sequence
        for (var i = 0; i < currentSequence.keyPoints.Count; i++)
        {
            var currentKeypoint = currentSequence.keyPoints[i];

            GameObject keypoint = Instantiate(keyPointPrefab, currentKeypoint.TargetPosition, Quaternion.identity);
            keypoint.transform.Find("TargetRotation").transform.position = currentKeypoint.TargetRotationPosition;
            keypoint.transform.Find("Canvas").Find("Text").GetComponent<Text>().text = (i + 1).ToString();
        }
    }

    private void SaveSequences()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/sequences.dat", FileMode.Create);
        binaryFormatter.Serialize(file, sequences);
        file.Close();
    }
}