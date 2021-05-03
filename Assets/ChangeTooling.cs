using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTooling : MonoBehaviour
{
    public GameObject suctionTool;
    public GameObject plasmaTorch;
    private int indice = 0;
    
    public void CycleThroughToolings()
    {
        if (indice == 2)
        {
            indice = 0;
        }
        else
        {
            indice++;
        }
        
        if (indice == 1)
        {
            suctionTool.SetActive(true);
            plasmaTorch.SetActive(false);

        }
        else if (indice == 2)
        {
            suctionTool.SetActive(false);
            plasmaTorch.SetActive(true);
        }
        else if(indice == 0)
        {
            suctionTool.SetActive(false);
            plasmaTorch.SetActive(false);

        }
        
    }
}
