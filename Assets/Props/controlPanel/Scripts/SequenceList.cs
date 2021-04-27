using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SequenceList : MonoBehaviour
{
    private List<Sequence> sequences = new List<Sequence>();
    private int index;

    private List<Sequence> shownSequences = new List<Sequence>();
    private int shownIndex;

    public Sequence currentSequence => sequences[index];

    public List<GameObject> panels;


    private void Start()
    {
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
    }

    public void AddSequence(Sequence sequence)
    {
        sequence.id = sequences.Count + 1;
        sequences.Add(sequence);
        index = sequences.Count - 1;

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/sequences.dat", FileMode.Create);
        binaryFormatter.Serialize(file, sequences);
        file.Close();


        if (shownSequences.Count < 6)
        {
            shownSequences.Add(sequences[index]);
            if (shownSequences.Count > 1)
            {
                shownIndex++;
            }
        }
        else
        {
            ScrollDown();
        }

        UpdateUi();
    }

    public void SelectUp()
    {
        if (index > 0)
        {
            index--;
            ScrollUp();
            UpdateUi();
        }
    }

    public void SelectDown()
    {
        if (index < sequences.Count - 1)
        {
            index++;
            ScrollDown();
            UpdateUi();
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

    public void DeleteSelectedSequence()
    {
    }
}