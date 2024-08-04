using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    bool gameEnded;
    public float restartDelay = 1f;
    public GameObject levelUI;

    public void CompleteLevel() 
    {
        Debug.Log("You Won!");
        levelUI.SetActive(true);
    }
    public void EndGame()
    {
        if (gameEnded == false) {
            gameEnded = true;
            Debug.Log("Game Over");
            //Restart Game
           Invoke("RestartLevel", restartDelay);
        }
        
    }
    public void RestartLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    
    }
    public void NextLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RestartGame() {
        SceneManager.LoadScene(0);
    }

}
