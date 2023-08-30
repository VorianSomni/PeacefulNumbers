using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PenNumbers : MonoBehaviour
{
    public TextMeshProUGUI[] numberTexts;

    public void ShowHidePenNumber(int number)
    {
        if(numberTexts[number - 1].IsActive())
        {
            numberTexts[number - 1].gameObject.SetActive(false);
        }
        else
        {
            numberTexts[number - 1].gameObject.SetActive(true);
        }
    }
}
