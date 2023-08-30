using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave
{
    public string gamePlayed;
    public int gameDifficulty;
    public string gameFill;
    public int timeinsecs;
    public string solution;
    public bool timeDeactivated;

    public GameSave(int gameDifficulty, string gamePlayed, string gameFill, string solution, int timeinsecs, bool timeDeactivated)
    {
        this.gamePlayed = gamePlayed;
        this.gameDifficulty = gameDifficulty;
        this.gameFill = gameFill;
        this.timeinsecs = timeinsecs;
        this.solution = solution;
        this.timeDeactivated = timeDeactivated;
    }
}
