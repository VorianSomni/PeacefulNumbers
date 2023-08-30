using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BG_script : MonoBehaviour
{
    public SpriteRenderer[] Backgrounds;
    byte ActualBG = 0;
    bool firstTime = true;
    byte time = 60;
    

    public void StartBackground()
    {
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            LeanTween.alpha(Backgrounds[i].gameObject, 0, 0.001f);
        }

        StartCoroutine(ChooseNextBG());

    }

    IEnumerator Tween(byte actual, byte next)
    {
        if (firstTime)
        {
            LeanTween.alpha(Backgrounds[next].gameObject, 1, 2.5f);
            firstTime = false;
        }
        else
        {
            LeanTween.alpha(Backgrounds[next].gameObject, 1, time);
            LeanTween.alpha(Backgrounds[actual].gameObject, 0, time);
        }
        
        while (Backgrounds[next].gameObject.LeanIsTweening())
        {
            yield return new WaitForSeconds(3);
        }
        ActualBG = next;
        StartCoroutine(ChooseNextBG());
    }

    IEnumerator ChooseNextBG()
    {
        byte NextBG = 0;
        while (true)
        {
            NextBG = (byte)Random.Range(0, Backgrounds.Length);
            if (NextBG != ActualBG)
            {
                
                break;
            }
        }

        StartCoroutine(Tween(ActualBG, NextBG));
        yield return null;
    }

   
}