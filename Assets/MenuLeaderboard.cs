using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLeaderboard : MonoBehaviour
{
   public void ReturnPressed(){
      Debug.Log("ReturnPressed");
      SceneManager.LoadScene("MainMenuScene");
      Debug.Log("Main Menu is loaded");
   }
}

