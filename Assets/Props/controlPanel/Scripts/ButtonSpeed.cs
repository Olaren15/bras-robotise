using UnityEngine;
using UnityEngine.UI;

public class ButtonSpeed : MonoBehaviour
{
    public Text text;
    private SequencePlayer sequencePlayer;
    
    private void Start()
    {
        sequencePlayer = FindObjectOfType<SequencePlayer>();
    }

    public void ChangeSpeed(bool speedUp)
    {
        if (speedUp)
        {
            if (sequencePlayer.speed < 9)
            {
                sequencePlayer.speed++;
            }
        }
        else
        {
            if (sequencePlayer.speed > 1)
            {
                sequencePlayer.speed--;
            }
        }
        
        text.text = sequencePlayer.speed.ToString();
    }
}