using System;
using System.Collections.Generic;
using System.Text;

public class RawData
{
    public List<int> GameScore { get; set; }
    public List<int> HeartsGained { get; set; }
    public List<int> PowerupsUsed { get; set; }
    public List<int> HeartsLost { get; set; }
    public List<int> FlyingTaps { get; set; }
    public StatisticsResult CalculatedGameScore { get; set; }
    public StatisticsResult CalculatedHeartsGained { get; set; }
    public StatisticsResult CalculatedPowerupsUsed { get; set; }
    public StatisticsResult CalculatedHeartsLost { get; set; }
    public StatisticsResult CalculatedFlyingTaps { get; set; }
}
