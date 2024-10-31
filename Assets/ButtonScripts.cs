using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonScripts : MonoBehaviour 
{
    public void LeaderboardPressed()
    {
      Debug.Log("LeaderboardPressed");
      SceneManager.LoadScene("LeaderboardScene");
      Debug.Log("LeaderBoard scene loaded");
   }

   public void TutorialPressed()
    {
      Debug.Log("TutorialPressed");
      SceneManager.LoadScene("TutorialScene");
      Debug.Log("Tutorial scene loaded");
   }
  
   public void LobbyPressed()
    {
      Debug.Log("LobbyPressed");
      SceneManager.LoadScene("PreGameScene");
      Debug.Log("PreGameScene loaded");
   }   

   public void QuitPressed()
    {
      Debug.Log("QuitPressed");
      Application.Quit();
      Debug.Log("Game exited");
   }
    public void ReturnPressed()
    {
        Debug.Log("MenuTutorial: ReturnPressed");
        SceneManager.LoadScene("MainMenuScene");
        Debug.Log("Main Menu is loaded");
    }

    public void PausePressed()
    {
        Debug.Log("Pause was pressed");
    }

    public void RestartGame()
    {
        // Loads the pre game scene when button is clicked
        Debug.Log("Restarted game");
        Debug.Log("Pre Game Scene is loaded");
        SceneManager.LoadScene("PreGameScene");
    }
}