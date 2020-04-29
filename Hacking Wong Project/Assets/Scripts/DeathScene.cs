using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScene : MonoBehaviour
{
    public Text text;
    void Start()
    {
        text.text = "Failed in "+TimeManager.timeScore.ToString()+ " seconds";
    }
    public void GoToMainMenu()
    {
        TimeManager.timeScore = 0;
        TimeManager.timeKeeper = 0;
        Player_Movement.currentHealth = Player_Movement.maxHealth;
        SceneManager.LoadScene("Menu");
    }


    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
