using System;

using System.Collections.Generic;
//using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
//using UnityEditor.U2D.Aseprite;
using UnityEngine.UIElements;
using Unity.Burst.Intrinsics;
using System.Linq;


public static class Stats
{
    private static string _filePath = @"c:\temp\cs\runrun-data.txt";

    public static GameRecord Record = new GameRecord();
    public static RawData Data = new RawData();

    public static void Initialise()
    {
        CheckForDataFile();
        RebuildStatistics();
        Record = new GameRecord();
    }

    public static void SaveToFile()
    {
        CheckForDataFile();
        var str = $"{Record.Name},{Record.HeartsGained},{Record.HeartsLost},{Record.GameScore},{Record.PowerupsUsed},{Record.FlyingTaps}";

        using (StreamWriter file = File.AppendText(_filePath))
        {
            file.WriteLine(str);
        }
    }

    private static void CheckForDataFile()
    {
        if (!File.Exists(_filePath))
        {
            using (var stream = File.Create(_filePath))
            {

                // creates an empty file - do nothing else
            }
        }
    }

    private static void RebuildStatistics()
    {
        List<List<int>> columns = new List<List<int>>();

        // open file
        using (StreamReader reader = new StreamReader(_filePath))
        {
            string line;
            bool isFirstLine = true;

            // Read the file line by line
            while ((line = reader.ReadLine()) != null)
            {
                // Split the line into columns based on the comma
                string[] values = line.Split(',');

                // Initialize columns if it's the first line (header row)
                if (isFirstLine)
                {
                    foreach (var value in values)
                    {
                        columns.Add(new List<int>());
                    }
                    isFirstLine = false;
                }

                // Process each value in the row and add to corresponding column list
                for (int i = 0; i < values.Length; i++)
                {
                    if (int.TryParse(values[i], out int number))
                    {
                        columns[i].Add(number);
                    }
                    // This is for helping to see whats wrong
                    /*
                    else
                    {                            
                        Console.WriteLine($"Warning: '{values[i]}' is not a valid integer.");
                    }
                    */
                }
            }
        }
        //calculate stats
        var data = new RawData();
        if (columns.Count > 0)
        {
            //{Record.Name},{Record.HeartsGained},{Record.HeartsLost},{Record.GameScore},{Record.PowerupsUsed},{Record.FlyingTaps}
            data.HeartsGained = columns[1];
            data.HeartsLost = columns[2];
            data.GameScore = columns[3];
            data.PowerupsUsed = columns[4];
            data.FlyingTaps = columns[5];
            data.CalculatedHeartsGained = CalculateStatistics(columns[1]);
            data.CalculatedHeartsLost = CalculateStatistics(columns[2]);
            data.CalculatedGameScore = CalculateStatistics(columns[3]);
            data.CalculatedPowerupsUsed = CalculateStatistics(columns[4]);
            data.CalculatedFlyingTaps = CalculateStatistics(columns[5]);
            //store stats
            Data = data;
        }
    }

    private static StatisticsResult CalculateStatistics(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
        {
            throw new ArgumentException("The list of numbers cannot be null or empty.");
        }

        var result = new StatisticsResult();
        var frequency = new Dictionary<int, int>();

        // Initialize variables for max, min, and sum for average calculation
        int max = int.MinValue;
        int min = int.MaxValue;
        int sum = 0;

        // Iterate through the numbers to compute max, min, sum, and frequency
        foreach (var num in numbers)
        {
            // Update max and min
            if (num > max)
            {
                max = num;
            }

            if (num < min)
            {
                min = num;
            }

            // Update sum for average calculation
            sum += num;

            // Update frequency dictionary for mode calculation
            if (frequency.ContainsKey(num))
            {
                frequency[num]++;
            }
            else
            {
                frequency[num] = 1;
            }
        }

        // Calculate average
        result.Average = (double)sum / numbers.Count;

        // Set Max and Min values
        result.Max = max;
        result.Min = min;

        // Calculate Mode
        int highestFrequency = 0;
        foreach (var entry in frequency)
        {
            if (entry.Value > highestFrequency)
            {
                highestFrequency = entry.Value;
            }
        }

        // Collect all numbers with the highest frequency (the modes)
        result.Mode = new List<int>();
        foreach (var entry in frequency)
        {
            if (entry.Value == highestFrequency)
            {
                result.Mode.Add(entry.Key);
            }
        }

        return result;
    }

    public static string Compare()
    {
        var r = Record;
        var hg = CalculatePercentile(Data.HeartsGained, r.HeartsGained);
        var hl = CalculatePercentile(Data.HeartsLost, r.HeartsLost);
        var gs = CalculatePercentile(Data.GameScore, r.GameScore);
        var pu = CalculatePercentile(Data.PowerupsUsed, r.PowerupsUsed);
        var ft = CalculatePercentile(Data.FlyingTaps, r.FlyingTaps);
        //Console.WriteLine("HeartsGained Percentile:" + hg);
        //Console.WriteLine("HeartsLost Percentile:" + hl);
        //Console.WriteLine("GameScore Percentile:" + gs);
        //Console.WriteLine("PowerupsUsed PowerupsUsed:" + pu);
        //Console.WriteLine("FlyingTaps Percentile:" + ft);
        var sb = new StringBuilder();
        sb.AppendLine(ConvertToWords("Hearts Gained", r.HeartsGained, hg, Data.CalculatedHeartsGained));
        sb.AppendLine(ConvertToWords("Hearts Lost", r.HeartsLost, hl, Data.CalculatedHeartsLost));
        sb.AppendLine(ConvertToWords("Game Score", r.GameScore, gs, Data.CalculatedGameScore));
        sb.AppendLine(ConvertToWords("Powerups Used", r.PowerupsUsed, pu, Data.CalculatedPowerupsUsed));
        sb.AppendLine(ConvertToWords("Flying Taps", r.FlyingTaps, ft, Data.CalculatedFlyingTaps));
        return sb.ToString();
    }

    private static string ConvertToWords(string metric, int score, double percentile, StatisticsResult statistics)
    {
        if (percentile == 100)
        {
            return $"{metric} [{score}]. Congrats you are the best! You beat the previous record of {statistics.Max}.";
        }
        else if (percentile > 50)
        {
            return $"{metric} [{score}]. You done better than {percentile:N2}% other players. Highest: {statistics.Max} Lowest: {statistics.Min}";
        }
        else
        {
            return $"{metric} [{score}]. You done worse than {100 - percentile:N2}% other players. Highest: {statistics.Max} Lowest: {statistics.Min}";
        }
    }

    private static double CalculatePercentile(List<int> numbers, int target)
    {
        // Ensure the list is not empty
        if (numbers == null || numbers.Count == 0)
        {
            throw new ArgumentException("The list of numbers cannot be null or empty.");
        }

        // Sort the list to ensure correct percentile calculation
        numbers = BubbleSort(numbers);

        // Count how many numbers are less than or equal to the target
        int countLessThanOrEqualToTarget = 0;
        foreach (int num in numbers)
        {
            if (num <= target)
            {
                countLessThanOrEqualToTarget++;
            }
            else
            {
                break; // Since the list is sorted, no need to check further
            }
        }

        // Calculate the percentile
        double percentile = ((double)countLessThanOrEqualToTarget / numbers.Count) * 100;

        return percentile;
    }

    public static List<int> BubbleSort(List<int> numbers)
    {

        PrintList(numbers);

        for (int i = 1; i < numbers.Count; i++)
        {
            int key = numbers[i];
            int j = i - 1;

            // Move elements of list[0..i-1], that are greater than key,
            // to one position ahead of their current position
            while (j >= 0 && numbers[j] > key)
            {
                numbers[j + 1] = numbers[j];
                j--;
            }
            numbers[j + 1] = key;
        }

        PrintList(numbers);
        return numbers;
    }

    private static void PrintList(List<int> list)
    {
        foreach (int number in list)
        {
            Debug.Log(number + " ");
        }
    }
    public static string Leaderboard()
    {
        List<List<int>> columns = new List<List<int>>();
        List<GameRecord> output = new List<GameRecord>();

        // open file
        using (StreamReader reader = new StreamReader(_filePath))
        {
            string line;
            bool isFirstLine = true;


            // Read the file line by line
            while ((line = reader.ReadLine()) != null)
            {
                // Split the line into columns based on the comma
                string[] values = line.Split(',');
                var gr = new GameRecord();

                // Initialize columns if it's the first line (header row)
                if (isFirstLine)
                {
                    foreach (var value in values)
                    {
                        columns.Add(new List<int>());
                    }
                    isFirstLine = false;
                }

                // Process each value in the row and add to corresponding column list
                if (int.TryParse(values[3], out int number))
                {
                    gr.Name = values[0];
                    gr.GameScore = number;
                    output.Add(gr);
                }
            }
            output = output.OrderByDescending(x => x.GameScore).Take(5).ToList();
        }

        var sb = new StringBuilder();
        foreach (var gr in output)
        {
            sb.AppendLine(gr.GameScore + " " + gr.Name);
        }
        return sb.ToString();
    }
    public static void ResetStats()
    {
        if (File.Exists(_filePath))
        {
            // Delete the file
            File.Delete(_filePath);
            Console.WriteLine("File deleted successfully.");
            CheckForDataFile();
        }

    }
}


