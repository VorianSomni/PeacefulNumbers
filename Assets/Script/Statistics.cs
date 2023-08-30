using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Statistics : MonoBehaviour
{
    public JsonSaving jsonSaving;
    public SudokuGame sudokuGame;
    public int dificultyNumber = 0;
    public TextMeshProUGUI DificultyLabel;
    public TextMeshProUGUI[] StatsLabels;
    public TextMeshProUGUI[] Quantities;
    public StatisticsSave rawStats;

    // Novice
    int N_GamesStarted = 0;
    int N_GamesFinished = 0;
    int N_GamesAbandoned = 0;
    int N_PersonalWorst = 0;
    int N_PersonalBest = 0;

    // Easy
    int E_GamesStarted = 0;
    int E_GamesFinished = 0;
    int E_GamesAbandoned = 0;
    int E_PersonalWorst = 0;
    int E_PersonalBest = 0;

    // Medium
    int M_GamesStarted = 0;
    int M_GamesFinished = 0;
    int M_GamesAbandoned = 0;
    int M_PersonalWorst = 0;
    int M_PersonalBest = 0;

    // Hard
    int H_GamesStarted = 0;
    int H_GamesFinished = 0;
    int H_GamesAbandoned = 0;
    int H_PersonalWorst = 0;
    int H_PersonalBest = 0;

    // Specialist or Expert
    int S_GamesStarted = 0;
    int S_GamesFinished = 0;
    int S_GamesAbandoned = 0;
    int S_PersonalWorst = 0;
    int S_PersonalBest = 0;

    // Terror or Evil
    int T_GamesStarted = 0;
    int T_GamesFinished = 0;
    int T_GamesAbandoned = 0;
    int T_PersonalWorst = 0;
    int T_PersonalBest = 0;

    private void Start()
    {
        dificultyNumber = 0;
        SetStats();
    }

    public void SetStats()
    {
        switch (dificultyNumber)
        {
            case 0:
                UpdateNoviceStats();
                break;
            case 1:
                UpdateEasyStats();
                break;
            case 2:
                UpdateMediumStats();
                break;
            case 3:
                UpdateHardStats();
                break;
            case 4:
                UpdateExpertStats();
                break;
            case 5:
                UpdateEvilStats();
                break;
        }
    }

    public void NextStats()
    {
        if(dificultyNumber < 5)
        {
            dificultyNumber++;
        }
        SetStats();
    }

    public void PreviousStats()
    {
        if (dificultyNumber > 0)
        {
            dificultyNumber--;
        }
        SetStats();
    }

    private void UpdateAll()
    {
        UpdateEasyStats();
        UpdateMediumStats();
        UpdateHardStats();
        UpdateExpertStats();
        UpdateEvilStats();
        UpdateNoviceStats();
    }

    private void UpdateNoviceStats()
    {
        if (sudokuGame.lang == 1)
            DificultyLabel.text = "Novice";
        else
            DificultyLabel.text = "Novato";

        Quantities[0].text = N_GamesStarted.ToString();
        Quantities[1].text = N_GamesFinished.ToString();
        Quantities[2].text = N_GamesAbandoned.ToString();

        Quantities[4].text = ConvertTime(N_PersonalWorst);
        Quantities[5].text = ConvertTime(N_PersonalBest);
    }

    private void UpdateEasyStats()
    {
        if (sudokuGame.lang == 1)
            DificultyLabel.text = "Easy";
        else
            DificultyLabel.text = "Fácil";

        Quantities[0].text = E_GamesStarted.ToString();
        Quantities[1].text = E_GamesFinished.ToString();
        Quantities[2].text = E_GamesAbandoned.ToString();
        Quantities[4].text = ConvertTime(E_PersonalWorst);
        Quantities[5].text = ConvertTime(E_PersonalBest);
    }

    private void UpdateMediumStats()
    {
        if (sudokuGame.lang == 1)
            DificultyLabel.text = "Medium";
        else
            DificultyLabel.text = "Médio";

        Quantities[0].text = M_GamesStarted.ToString();
        Quantities[1].text = M_GamesFinished.ToString();
        Quantities[2].text = M_GamesAbandoned.ToString();

        Quantities[4].text = ConvertTime(M_PersonalWorst);
        Quantities[5].text = ConvertTime(M_PersonalBest);
    }

    private void UpdateHardStats()
    {
        if (sudokuGame.lang == 1)
            DificultyLabel.text = "Hard";
        else
            DificultyLabel.text = "Difícil";

        Quantities[0].text = H_GamesStarted.ToString();
        Quantities[1].text = H_GamesFinished.ToString();
        Quantities[2].text = H_GamesAbandoned.ToString();

        Quantities[4].text = ConvertTime(H_PersonalWorst);
        Quantities[5].text = ConvertTime(H_PersonalBest);
    }

    private void UpdateExpertStats()
    {
        if (sudokuGame.lang == 1)
            DificultyLabel.text = "Expert";
        else
            DificultyLabel.text = "Especialista";

        Quantities[0].text = S_GamesStarted.ToString();
        Quantities[1].text = S_GamesFinished.ToString();
        Quantities[2].text = S_GamesAbandoned.ToString();

        Quantities[4].text = ConvertTime(S_PersonalWorst);
        Quantities[5].text = ConvertTime(S_PersonalBest);
    }

    private void UpdateEvilStats()
    {
        if (sudokuGame.lang == 1)
            DificultyLabel.text = "Evil";
        else
            DificultyLabel.text = "Terror";

        Quantities[0].text = T_GamesStarted.ToString();
        Quantities[1].text = T_GamesFinished.ToString();
        Quantities[2].text = T_GamesAbandoned.ToString();

        Quantities[4].text = ConvertTime(T_PersonalWorst);
        Quantities[5].text = ConvertTime(T_PersonalBest);
    }


    string ConvertTime(int Time)
    {
        int minutes = Mathf.FloorToInt(Time / 60);
        int seconds = Mathf.FloorToInt(Time % 60);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        return timerString;
    }

    public void IncreaseGameStatistics(int dificulty, int gamesStarted = 0, int gamesFinished = 0, int gamesAbandoned = 0)
    {
        if (dificulty == 0)
        {
            N_GamesStarted += gamesStarted;
            N_GamesFinished += gamesFinished;
            N_GamesAbandoned += gamesAbandoned;
            SaveStatistics();
            return;
        }

        if (dificulty == 1)
        {
            E_GamesStarted += gamesStarted;
            E_GamesFinished += gamesFinished;
            E_GamesAbandoned += gamesAbandoned;
            SaveStatistics();
            return;
        }

        if (dificulty == 2)
        {
            M_GamesStarted += gamesStarted;
            M_GamesFinished += gamesFinished;
            M_GamesAbandoned += gamesAbandoned;
            SaveStatistics();
            return;
        }

        if (dificulty == 3)
        {
            H_GamesStarted += gamesStarted;
            H_GamesFinished += gamesFinished;
            H_GamesAbandoned += gamesAbandoned;
            SaveStatistics();
            return;
        }

        if (dificulty == 4)
        {
            S_GamesStarted += gamesStarted;
            S_GamesFinished += gamesFinished;
            S_GamesAbandoned += gamesAbandoned;
            SaveStatistics();
            return;
        }

        if (dificulty == 5)
        {
            T_GamesStarted += gamesStarted;
            T_GamesFinished += gamesFinished;
            T_GamesAbandoned += gamesAbandoned;
            SaveStatistics();
            return;
        }
    }
    
    public void CompareTimes(int dificulty, int gameSeconds)
    {
        if(gameSeconds == 0)
        {
            return;
        }


        if (dificulty == 0)
        {
            if (N_PersonalBest == 0)
            {
                N_PersonalBest = gameSeconds;
                N_PersonalWorst = gameSeconds;
            }
            else
            {
                if (gameSeconds < N_PersonalBest)
                {
                    N_PersonalBest = gameSeconds;
                }

                if (gameSeconds > N_PersonalWorst)
                {
                    N_PersonalWorst = gameSeconds;
                }
            }
            return;
        }

        if (dificulty == 1)
        {
            if (E_PersonalBest == 0)
            {
                E_PersonalBest = gameSeconds;
                E_PersonalWorst = gameSeconds;
            }
            else
            {
                if (gameSeconds < E_PersonalBest)
                {
                    E_PersonalBest = gameSeconds;
                }

                if (gameSeconds > E_PersonalWorst)
                {
                    E_PersonalWorst = gameSeconds;
                }
            }
            return;
        }

        if (dificulty == 2)
        {
            if (M_PersonalBest == 0)
            {
                M_PersonalBest = gameSeconds;
                M_PersonalWorst = gameSeconds;
            }
            else
            {
                if (gameSeconds < M_PersonalBest)
                {
                    M_PersonalBest = gameSeconds;
                }

                if (gameSeconds > M_PersonalWorst)
                {
                    M_PersonalWorst = gameSeconds;
                }
            }
            return;
        }

        if (dificulty == 3)
        {
            if(H_PersonalBest == 0)
            {
                H_PersonalBest = gameSeconds;
                H_PersonalWorst = gameSeconds;
            }
            else
            {
                if (gameSeconds < H_PersonalBest)
                {
                    H_PersonalBest = gameSeconds;
                }

                if (gameSeconds > H_PersonalWorst)
                {
                    H_PersonalWorst = gameSeconds;
                }
            }
            
            return;
        }

        if (dificulty == 4)
        {
            if (S_PersonalBest == 0)
            {
                S_PersonalBest = gameSeconds;
                S_PersonalWorst = gameSeconds;
            }
            else
            {
                if (gameSeconds < S_PersonalBest)
                {
                    S_PersonalBest = gameSeconds;
                }

                if (gameSeconds > S_PersonalWorst)
                {
                    S_PersonalWorst = gameSeconds;
                }
            }
            return;
        }

        if (dificulty == 5)
        {
            if(T_PersonalBest == 0)
            {
                T_PersonalBest = gameSeconds;
                T_PersonalWorst = gameSeconds;
            }
            else
            {
                if (gameSeconds < T_PersonalBest)
                {
                    T_PersonalBest = gameSeconds;
                }

                if (gameSeconds > T_PersonalWorst)
                {
                    T_PersonalWorst = gameSeconds;
                }
            }
            
            return;
        }

    }


    public void SaveStatistics()
    {
        jsonSaving.StatisticSave(N_GamesStarted, N_GamesFinished, N_GamesAbandoned, N_PersonalWorst, N_PersonalBest, E_GamesStarted, E_GamesFinished, E_GamesAbandoned, E_PersonalWorst, E_PersonalBest, M_GamesStarted, M_GamesFinished, M_GamesAbandoned, M_PersonalWorst, M_PersonalBest, H_GamesStarted, H_GamesFinished, H_GamesAbandoned, H_PersonalWorst, H_PersonalBest, S_GamesStarted, S_GamesFinished, S_GamesAbandoned, S_PersonalWorst, S_PersonalBest, T_GamesStarted, T_GamesFinished, T_GamesAbandoned, T_PersonalWorst, T_PersonalBest);
    }

    public void LoadStatistics()
    {

        jsonSaving.StatisticLoad();
        rawStats = jsonSaving.statisticsSave;

        // Novice
        N_GamesStarted = rawStats.N_GamesStarted;
        N_GamesFinished = rawStats.N_GamesFinished;
        N_GamesAbandoned = rawStats.N_GamesAbandoned;
        N_PersonalWorst = rawStats.N_PersonalWorst;
        N_PersonalBest = rawStats.N_PersonalBest;

        // Easy
        E_GamesStarted = rawStats.E_GamesStarted;
        E_GamesFinished = rawStats.E_GamesFinished;
        E_GamesAbandoned = rawStats.E_GamesAbandoned;
        E_PersonalWorst = rawStats.E_PersonalWorst;
        E_PersonalBest = rawStats.E_PersonalBest;

        // Medium
        M_GamesStarted = rawStats.M_GamesStarted;
        M_GamesFinished = rawStats.M_GamesFinished;
        M_GamesAbandoned = rawStats.M_GamesAbandoned;
        M_PersonalWorst = rawStats.M_PersonalWorst;
        M_PersonalBest = rawStats.M_PersonalBest;

        // Hard
        H_GamesStarted = rawStats.H_GamesStarted;
        H_GamesFinished = rawStats.H_GamesFinished;
        H_GamesAbandoned = rawStats.H_GamesAbandoned;
        H_PersonalWorst = rawStats.H_PersonalWorst;
        H_PersonalBest = rawStats.H_PersonalBest;

        // Specialist or Expert
        S_GamesStarted = rawStats.S_GamesStarted;
        S_GamesFinished = rawStats.S_GamesFinished;
        S_GamesAbandoned = rawStats.S_GamesAbandoned;
        S_PersonalWorst = rawStats.S_PersonalWorst;
        S_PersonalBest = rawStats.S_PersonalBest;

        // Terror or Evil
        T_GamesStarted = rawStats.T_GamesStarted;
        T_GamesFinished = rawStats.T_GamesFinished;
        T_GamesAbandoned = rawStats.T_GamesAbandoned;
        T_PersonalWorst = rawStats.T_PersonalWorst;
        T_PersonalBest = rawStats.T_PersonalBest;

        UpdateAll();
    }


    public void ResetStatistics()
    {
        // Novice
        N_GamesStarted = 0;
        N_GamesFinished = 0;
        N_GamesAbandoned = 0;
        N_PersonalWorst = 0;
        N_PersonalBest = 0;

        // Easy
        E_GamesStarted = 0;
        E_GamesFinished = 0;
        E_GamesAbandoned = 0;
        E_PersonalWorst = 0;
        E_PersonalBest = 0;

        // Medium
        M_GamesStarted = 0;
        M_GamesFinished = 0;
        M_GamesAbandoned = 0;
        M_PersonalWorst = 0;
        M_PersonalBest = 0;

        // Hard
        H_GamesStarted = 0;
        H_GamesFinished = 0;
        H_GamesAbandoned = 0;
        H_PersonalWorst = 0;
        H_PersonalBest = 0;

        // Specialist or Expert
        S_GamesStarted = 0;
        S_GamesFinished = 0;
        S_GamesAbandoned = 0;
        S_PersonalWorst = 0;
        S_PersonalBest = 0;

        // Terror or Evil
        T_GamesStarted = 0;
        T_GamesFinished = 0;
        T_GamesAbandoned = 0;
        T_PersonalWorst = 0;
        T_PersonalBest = 0;

        UpdateAll();
        SaveStatistics();
    }
  
}
