using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ButtonScripts : MonoBehaviour 
{

    public Text text;
    public void LeaderboardPressed()
    {
      Debug.Log("LeaderboardPressed");
      SceneManager.LoadScene("LeaderboardScene"); //loads leadboard scene
      Debug.Log("LeaderBoard scene loaded");
   }

   public void TutorialPressed()
    {
      Debug.Log("TutorialPressed");
      SceneManager.LoadScene("TutorialScene"); // loads tutorial scene
      Debug.Log("Tutorial scene loaded");
   }
  
   public void LobbyPressed()
    {
      Debug.Log("LobbyPressed");
      SceneManager.LoadScene("PreGameScene"); // loads pregame scene
      Debug.Log("PreGameScene loaded");
   }   

   public void QuitPressed()
    {
      Debug.Log("QuitPressed");
      Application.Quit(); // exits the application
      Debug.Log("Game exited");
   }
    public void ReturnPressed()
    {
        Debug.Log("MenuTutorial: ReturnPressed");
        SceneManager.LoadScene("MainMenuScene"); // loads the main menu scene
        Debug.Log("Main Menu is loaded");
    }

    public void EnlargePressed()
    {
        Debug.Log("enlarge was pressed");
        text.fontSize = 73; // increases font size
    }

    public void RestartGame()
    {
        // Loads the pre game scene when button is clicked
        Debug.Log("Restarted game");
        Debug.Log("Pre Game Scene is loaded"); //
        SceneManager.LoadScene("PreGameScene");
    }
}