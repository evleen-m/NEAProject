using System;
using System.Collections.Generic;
using System.Text;

public class GameRecord
{
    public string Name { get; set; }
    public int GameScore { get; set; }
    public int HeartsGained { get; set; }
    public int PowerupsUsed { get; set; }
    public int HeartsLost { get; set; }
    public int FlyingTaps { get; set; }

    public DateTime GameFinished { get; set; }

    public void Reset()
    {
        Name = string.Empty;
        GameScore = 0;
        HeartsGained = 0;
        PowerupsUsed = 0;
        HeartsLost = 0;
        FlyingTaps = 0;
        GameFinished = DateTime.MinValue;
    }
}