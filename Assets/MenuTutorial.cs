using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuTutorial : MonoBehaviour 
{   
   public void ReturnPressed(){
      Debug.Log("MenuTutorial: ReturnPressed");
      SceneManager.LoadScene("MainMenuScene");
      Debug.Log("Main Menu is loaded");
   }
}