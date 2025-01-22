using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuPreGame : MonoBehaviour
{
    [SerializeField] private Text invalidText;
    private string username = "";
    public void StartGamePressed()
    {
        //check if username entered is of acceptable length before allowing user to enter game
        if (username.Length != 3)
        {
            invalidText.text = "Invalid username - must be 3 characters";
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
        Debug.Log("Username:" + username);
    }
}