using System;

using System.Collections.Generic;
using System.IO;
using System.Text;


    public static class Stats
    {
        public static void CreateFile(string filePath, string textToWrite)
        {
            // Try to write to the file
            try
            {
                // Write the text to the file
                File.WriteAllText(filePath, textToWrite);

                // Inform the user
                Console.WriteLine($"Text successfully written to {filePath}");
            }
            catch (Exception ex)
            {
                // Handle any errors that might occur
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    public static void Save(string filePath)
    {
        //string = "1, 1, 1, 1, "
        string ProgressString = GameProgress.ToString();
        string LifeString = GameLives.ToString();
        string PowerupString = GamePowerups.ToString();

        string textToWrite = ProgressString + "," + LifeString + "," + PowerupString;

        File.WriteAllText(filePath, textToWrite);
    }

    // Counters
    public static int GameProgress { get; set; }
    public static int GameLives { get; set; }
    public static int GamePowerups { get; set; }

    //reset method resets all counter to base values

    // Save method, takes all counters and writes to a string


    }

