using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInfos
{
    public string gamePlayed;
    public int gameDifficulty;
    public string gameFill;
    public int timeinsecs;
    public int lang;
    public string solution;

    public SaveInfos(string gamePlayed, int gameDifficulty, string gameFill, int timeinsecs,  int lang, string solution)
    {
        this.gamePlayed = gamePlayed;
        this.gameDifficulty = gameDifficulty;
        this.gameFill = gameFill;
        this.timeinsecs = timeinsecs;
        this.lang = lang;
        this.solution = solution;
    }
}
