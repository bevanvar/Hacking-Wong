using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    private SpawnManager script;
   void OnTriggerEnter2D(Collider2D other)
   {
        if (other.CompareTag("Player"))
        {
            if (SceneManager.sceneCountInBuildSettings - 2 == SceneManager.GetActiveScene().buildIndex)
            {
                TimeManager.timeKeeper = 0;
                TimeManager.timeScore = 0;
                Player_Movement.currentHealth = Player_Movement.maxHealth;
                SceneManager.LoadScene(0);
                return;
            }
            else
            {
                script = GameObject.Find("GameManager").GetComponent<SpawnManager>();
                script.OnArrowTouch();
            }

        }
   }
}
