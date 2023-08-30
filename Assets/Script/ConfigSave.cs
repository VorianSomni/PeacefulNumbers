using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigSave
{
    public int language;
    public bool sound;
    public bool music;
    public bool timeOn;
    public string dateTime;
    public int TimesAdWasPlayedToday;
    public bool NoAdsPurchased;

    public ConfigSave(int language, bool sound, bool music, bool timeOn, string dateTime, int timesAdWasPlayedToday, bool NoAdsPurchased)
    {
        this.language = language;
        this.sound = sound;
        this.music = music;
        this.timeOn = timeOn;
        this.dateTime = dateTime;
        this.TimesAdWasPlayedToday = timesAdWasPlayedToday;
        this.NoAdsPurchased = NoAdsPurchased;
    }

}
