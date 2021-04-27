using System;
using System.Collections.Generic;
using Robot_arm.Scripts;

[Serializable]
public class Sequence
{
    public List<KeyPoint> keyPoints { get; set; }
    public int id { get; set; }

    public Sequence(List<KeyPoint> keyPoints)
    {
        this.keyPoints = keyPoints;
    }
}