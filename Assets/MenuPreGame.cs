using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPreGame : MonoBehaviour 
{
   public void ReturnPressed()
   {
      Debug.Log("MenuPreGame: ReturnPressed");
      SceneManager.LoadScene("MainMenuScene");
      Debug.Log("Main Menu is loaded");
   }

   public void StartGamePressed()
   {
      Debug.Log("MenuPreGame: StartGamePressed");
      SceneManager.LoadScene("GameScene");
      Debug.Log("GameScene is loaded");
   }
}