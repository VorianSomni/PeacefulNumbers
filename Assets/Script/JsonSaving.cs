using System.IO;
using UnityEngine;
using System;

public class JsonSaving : MonoBehaviour
{
    public GameSave gameSave;
    public StatisticsSave statisticsSave;
    public ConfigSave configSave;

    public Statistics statistics;

    public SudokuGame sudokugame;
    private string GameSaveDataPath = "";
    private string StatisticsSaveDataPath = "";
    private string ConfigSaveDataPath = "";


    private void Awake()
    {
        SetPaths();
    }

    

    public void SetPaths()
    {
        GameSaveDataPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "GameInfosData.json";
        StatisticsSaveDataPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "StatisticsSaveDataPath.json";
        ConfigSaveDataPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "ConfigSaveDataPath.json";
    }


    // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

    private void CreateGameSave(int gameDifficulty, string gamePlayed, string gameFill, string solution, int timeinsecs, bool timeDeactivated)
    {
        gameSave = new GameSave(gameDifficulty, gamePlayed, gameFill, solution, timeinsecs, timeDeactivated);
    }

    private void CreateStatisticsSave(int n_GamesStarted, int n_GamesFinished, int n_GamesAbandoned, int n_PersonalWorst, int n_PersonalBest, int e_GamesStarted, int e_GamesFinished, int e_GamesAbandoned, int e_PersonalWorst, int e_PersonalBest, int m_gamesStarted, int m_gamesFinished, int m_gamesAbandoned, int m_personalWorst, int m_personalBest, int h_GamesStarted, int h_GamesFinished, int h_GamesAbandoned,  int h_PersonalWorst, int h_PersonalBest, int s_gamesStarted, int s_gamesFinished, int s_gamesAbandoned,   int s_personalWorst, int s_personalBest, int t_gamesStarted, int t_gamesFinished, int t_gamesAbandoned, int t_personalWorst, int t_personalBest)
    {
        statisticsSave = new StatisticsSave(n_GamesStarted, n_GamesFinished, n_GamesAbandoned,  n_PersonalWorst, n_PersonalBest, e_GamesStarted, e_GamesFinished, e_GamesAbandoned, e_PersonalWorst, e_PersonalBest, m_gamesStarted, m_gamesFinished, m_gamesAbandoned, m_personalWorst, m_personalBest, h_GamesStarted, h_GamesFinished, h_GamesAbandoned, h_PersonalWorst, h_PersonalBest, s_gamesStarted, s_gamesFinished, s_gamesAbandoned, s_personalWorst, s_personalBest, t_gamesStarted, t_gamesFinished, t_gamesAbandoned,  t_personalWorst, t_personalBest);
    }

    private void CreateConfigSave(int language, bool sound, bool music, bool timeOn, string dateTime, int timesAdWasPlayedToday, bool NoAdsPurchased)
    {
        configSave = new ConfigSave(language, sound, music, timeOn, dateTime, timesAdWasPlayedToday, NoAdsPurchased);
    }

    // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=



    // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public void GameSave(int gameDifficulty, string gamePlayed, string gameFill, string solution, int timeinsecs, bool timeDeactivated)
    {
        CreateGameSave(gameDifficulty, gamePlayed, gameFill, solution, timeinsecs, timeDeactivated);
        string savePath = GameSaveDataPath;
        
        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(gameSave);
        print(json);
        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
        writer.Close();
    }

    public void StatisticSave(int n_GamesStarted, int n_GamesFinished, int n_GamesAbandoned, int n_PersonalWorst, int n_PersonalBest, int e_GamesStarted, int e_GamesFinished, int e_GamesAbandoned, int e_PersonalWorst, int e_PersonalBest, int m_gamesStarted, int m_gamesFinished, int m_gamesAbandoned, int m_personalWorst, int m_personalBest, int h_GamesStarted, int h_GamesFinished, int h_GamesAbandoned, int h_PersonalWorst, int h_PersonalBest, int s_gamesStarted, int s_gamesFinished, int s_gamesAbandoned, int s_personalWorst, int s_personalBest, int t_gamesStarted, int t_gamesFinished, int t_gamesAbandoned, int t_personalWorst, int t_personalBest)
    {
        CreateStatisticsSave(n_GamesStarted, n_GamesFinished, n_GamesAbandoned, n_PersonalWorst, n_PersonalBest, e_GamesStarted, e_GamesFinished, e_GamesAbandoned, e_PersonalWorst, e_PersonalBest, m_gamesStarted, m_gamesFinished, m_gamesAbandoned, m_personalWorst, m_personalBest, h_GamesStarted, h_GamesFinished, h_GamesAbandoned,  h_PersonalWorst, h_PersonalBest, s_gamesStarted, s_gamesFinished, s_gamesAbandoned, s_personalWorst, s_personalBest, t_gamesStarted, t_gamesFinished, t_gamesAbandoned,  t_personalWorst, t_personalBest);
        string savePath = StatisticsSaveDataPath;

        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(statisticsSave);
        print(json);
        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
        writer.Close();
    }

    public void ConfigSave(int language, bool sound, bool music, bool timeOn, string dateTime, int timesAdWasPlayedToday, bool NoAdsPurchased)
    {
        CreateConfigSave(language, sound, music, timeOn, dateTime, timesAdWasPlayedToday, NoAdsPurchased);
        string savePath = ConfigSaveDataPath;

        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(configSave);
        print(json);
        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
        writer.Close();
    }
    // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=



    // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public void GameLoad()
    {
        if (!File.Exists(GameSaveDataPath))
        {
            var myfile = File.Create(GameSaveDataPath);
            myfile.Close();
        }

        using StreamReader reader = new StreamReader(GameSaveDataPath);
        string json = reader.ReadToEnd();
        print(json);
        gameSave = JsonUtility.FromJson<GameSave>(json);
        reader.Close();
    }

    public void StatisticLoad()
    {
        if (!File.Exists(StatisticsSaveDataPath))
        {
            var myfile = File.Create(StatisticsSaveDataPath);
            myfile.Close();
        }

        using StreamReader reader1 = new StreamReader(StatisticsSaveDataPath);
        string json1 = reader1.ReadToEnd();
        reader1.Close();

        if (json1 == "")
        {
            statistics.SaveStatistics();
        }
        else
        {
            statisticsSave = JsonUtility.FromJson<StatisticsSave>(json1);
            return;
        }

        using StreamReader reader2 = new StreamReader(StatisticsSaveDataPath);
        string json2 = reader2.ReadToEnd();
        reader2.Close();
        statisticsSave = JsonUtility.FromJson<StatisticsSave>(json2);
        
    }

    public void ConfigLoad()
    {
        if (!File.Exists(ConfigSaveDataPath))
        {
            var myfile = File.Create(ConfigSaveDataPath);
            myfile.Close();
        }

        using StreamReader reader = new StreamReader(ConfigSaveDataPath);
        string json = reader.ReadToEnd();
        print(json);
        configSave = JsonUtility.FromJson<ConfigSave>(json);
        reader.Close();
    }

    // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
}
