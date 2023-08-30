using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsSave
{
    public int N_GamesStarted;
    public int N_GamesFinished ;
    public int N_GamesAbandoned ;
    
    public int N_PersonalWorst;
    public int N_PersonalBest;

    // Easy
    public int E_GamesStarted;
    public int E_GamesFinished;
    public int E_GamesAbandoned;
    public int E_PersonalWorst;
    public int E_PersonalBest;

    // Medium
    public int M_GamesStarted;
    public int M_GamesFinished;
    public int M_GamesAbandoned;
    public int M_PersonalWorst;
    public int M_PersonalBest;

    // Hard
    public int H_GamesStarted;
    public int H_GamesFinished ;
    public int H_GamesAbandoned;
    public int H_PersonalWorst;
    public int H_PersonalBest ;

    // Specialist or Expert
    public int S_GamesStarted;
    public int S_GamesFinished ;
    public int S_GamesAbandoned ;
    public int S_PersonalWorst ;
    public int S_PersonalBest ;

    // Terror or Evil
    public int T_GamesStarted;
    public int T_GamesFinished;
    public int T_GamesAbandoned;
    public int T_PersonalWorst;
    public int T_PersonalBest;

    public StatisticsSave(int n_GamesStarted, int n_GamesFinished, int n_GamesAbandoned, int n_PersonalWorst, int n_PersonalBest, int e_GamesStarted, int e_GamesFinished, int e_GamesAbandoned, int e_PersonalWorst, int e_PersonalBest, int m_gamesStarted, int m_gamesFinished, int m_gamesAbandoned, int m_personalWorst, int m_personalBest, int h_GamesStarted, int h_GamesFinished, int h_GamesAbandoned, int h_PersonalWorst, int h_PersonalBest, int s_gamesStarted, int s_gamesFinished, int s_gamesAbandoned, int s_personalWorst, int s_personalBest, int t_gamesStarted, int t_gamesFinished, int t_gamesAbandoned, int t_personalWorst, int t_personalBest)
    {
        N_GamesStarted = n_GamesStarted;
        N_GamesFinished = n_GamesFinished;
        N_GamesAbandoned = n_GamesAbandoned;
        N_PersonalWorst = n_PersonalWorst;
        N_PersonalBest = n_PersonalBest;
        E_GamesStarted = e_GamesStarted;
        E_GamesFinished = e_GamesFinished;
        E_GamesAbandoned = e_GamesAbandoned;
        E_PersonalWorst = e_PersonalWorst;
        E_PersonalBest = e_PersonalBest;
        M_GamesStarted = m_gamesStarted;
        M_GamesFinished = m_gamesFinished;
        M_GamesAbandoned = m_gamesAbandoned;
        M_PersonalWorst = m_personalWorst;
        M_PersonalBest = m_personalBest;
        H_GamesStarted = h_GamesStarted;
        H_GamesFinished = h_GamesFinished;
        H_GamesAbandoned = h_GamesAbandoned;
        H_PersonalWorst = h_PersonalWorst;
        H_PersonalBest = h_PersonalBest;
        S_GamesStarted = s_gamesStarted;
        S_GamesFinished = s_gamesFinished;
        S_GamesAbandoned = s_gamesAbandoned;
        S_PersonalWorst = s_personalWorst;
        S_PersonalBest = s_personalBest;
        T_GamesStarted = t_gamesStarted;
        T_GamesFinished = t_gamesFinished;
        T_GamesAbandoned = t_gamesAbandoned;
        T_PersonalWorst = t_personalWorst;
        T_PersonalBest = t_personalBest;
    }
}
