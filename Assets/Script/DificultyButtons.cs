using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DificultyButtons : MonoBehaviour
{
    public PlayButton playButtonMenuPrincipal;
    public PlayButton playButtonMenuSecundario;
    public TextMeshProUGUI[] difflabels;
    public TextMeshProUGUI[] difflabels2;
    public byte number;

    private void Start()
    {
        number = 0;
    }

    public void LeftButton()
    {
        if (number > 0)
        {
            number--;
        }
        playButtonMenuPrincipal.dif = number;
        playButtonMenuSecundario.dif = number;
        DeactivateAllDifLabels();
        difflabels[number].gameObject.SetActive(true);
        difflabels2[number].gameObject.SetActive(true);
    }

    public void RightButton()
    {
        if (number < difflabels.Length-1)
        {
            number++;
        }
        playButtonMenuPrincipal.dif = number;
        playButtonMenuSecundario.dif = number;
        DeactivateAllDifLabels();
        difflabels[number].gameObject.SetActive(true);
        difflabels2[number].gameObject.SetActive(true);
    }

    void DeactivateAllDifLabels()
    {
        for (int i = 0; i < difflabels.Length; i++)
        {
            difflabels[i].gameObject.SetActive(false);
            difflabels2[i].gameObject.SetActive(false);
        }
        
    }

}
