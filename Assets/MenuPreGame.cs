using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPreGame : MonoBehaviour 
{
   private string username = "";
   public void ReturnPressed()
   {
      Debug.Log("MenuPreGame: ReturnPressed");
      SceneManager.LoadScene("MainMenuScene");
      Debug.Log("Main Menu is loaded");
   }

   public void StartGamePressed()
   {
      //check if username entered is of acceptable length before allowing user to enter game
      if (username.Length<3 || username.Length>8)
      {
         Debug.Log("username length not met requirements");
      }
      
      else
      {
         //username is accepted, game is loaded
         Debug.Log("Username length is accepted");
         SceneManager.LoadScene("GameScene");
         Debug.Log("GameScene is loaded");
      }
        Debug.Log("nothing");
   }

   public void GetUsername(string input)
   {
        Stats.Initialise();
        username = input;
        Stats.Record.Name = username;
        Debug.Log("Username:"+username);
   }
}