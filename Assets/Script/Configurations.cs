using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Configurations : MonoBehaviour
{
    public SudokuGame sudokuGame;
    Coroutine TimerRoutine = null;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI version;
    public int timeInSeconds = 0;
    public bool TimerOn = false; // Serve para as configurações de tempo
    
    public int language;
    public bool sound;
    public bool music;
    public bool time;

    private void Start()
    {
        sudokuGame = gameObject.GetComponent<SudokuGame>();
        version.text = "Ver. " + Application.version;
    }


    public void StartTimer()
    {
        if (sudokuGame.DidYouDeactivateTimeInGame == false && TimerOn == true)
        {
           TimerRoutine = StartCoroutine(GameTimer());
        }
    }

    public void StopTimer()
    {
        if(TimerRoutine != null)
        {
            StopCoroutine(TimerRoutine);
        }
        
    }

    public void ResetTimer()
    {
        timeInSeconds = 0;
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timerString;
    }

    public void DeactivateTimer()
    {
        TimerOn = !TimerOn;
        if (sudokuGame.isplaying && TimerOn == false)
        {
            timeInSeconds = 0;
            int minutes = Mathf.FloorToInt(timeInSeconds / 60);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60);
            string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = timerString;

            sudokuGame.DidYouDeactivateTimeInGame = true;
            time = false;
        }
        else if (sudokuGame.isplaying == false && TimerOn == false)
        {
            sudokuGame.DidYouDeactivateTimeInGame = false;
            time = true;
        }
        sudokuGame.SaveConfig();
    }

    IEnumerator GameTimer()
    {
        yield return new WaitForSeconds(1);
        while (TimerOn)
        {
            yield return new WaitForSeconds(1);
            timeInSeconds += 1;
            int minutes = Mathf.FloorToInt(timeInSeconds / 60);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60);
            string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = timerString;
        }
    }

    

}
