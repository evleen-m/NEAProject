using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverDisplay;

    [ContextMenu("Increase Score")]
    public void increaseScore()
    {
        playerScore = playerScore+1;
        scoreText.text = playerScore.ToString();

    }

    public void restartGame()
    {
        Debug.Log("Restarted game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {

        gameOverDisplay.SetActive(true);
    }
}
