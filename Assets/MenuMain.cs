using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuMain : MonoBehaviour 
{
    public void LeaderboardPressed(){
      Debug.Log("LeaderboardPressed");
      SceneManager.LoadScene("LeaderboardScene");
      Debug.Log("LeaderBoard scene loaded");
   }

   public void TutorialPressed(){
      Debug.Log("TutorialPressed");
      SceneManager.LoadScene("TutorialScene");
      Debug.Log("Tutorial scene loaded");
   }
  
   public void LobbyPressed(){
      Debug.Log("LobbyPressed");
      SceneManager.LoadScene("PreGameScene");
      Debug.Log("PreGameScene loaded");
   }   
}