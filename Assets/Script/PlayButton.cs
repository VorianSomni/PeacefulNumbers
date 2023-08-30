using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayButton : MonoBehaviour
{
    public SudokuGame sudokuGame;
    public TextMeshProUGUI difficulty_text_game;
    public Statistics statistics;
    public GameObject TelaJogo;

    public Button continueButton;
    public GameObject DarkPanel;
    public GameObject AreYouSure;
    public GameObject AreYouSure2;
    public byte dif = 0;

    private void Awake()
    {
        dif = 0;
    }

    public void StartGameWithDificulty()
    {
        if (dif == 5)
        {
            DarkPanel.SetActive(true);
            AreYouSure2.SetActive(true);
            return;
        }
        else
        {
            if (continueButton.IsActive())
            {
                DarkPanel.SetActive(true);
                AreYouSure.SetActive(true);
            }
            else
            {
                TelaJogo.SetActive(true);
                sudokuGame.CreateGame(dif);
            }
        }
        
    }

    public void StartNewGameFromWin()
    {
        sudokuGame.CreateGame(dif);
    }



    public void YesImSure()
    {
        statistics.IncreaseGameStatistics(dif, 0, 0, 1);
        DarkPanel.SetActive(false);
        AreYouSure.SetActive(false);
        TelaJogo.SetActive(true);
        sudokuGame.CreateGame(dif);
    }

    public void YesImSure2()
    {
        if (continueButton.IsActive())
        {
            AreYouSure.SetActive(true);
        }
        else
        {
            DarkPanel.SetActive(false);
            TelaJogo.SetActive(true);
            sudokuGame.CreateGame(dif);
        }
    }

    public void NoTakeMeBack()
    {
        DarkPanel.SetActive(false);
        AreYouSure2.SetActive(false);
        AreYouSure.SetActive(false);
    }

    public void NoTakeMeBack2()
    {
        DarkPanel.SetActive(false);
        AreYouSure2.SetActive(false);
        AreYouSure.SetActive(false);
    }
}
