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
                Animator anim = GameObject.Find("SceneTransition").GetComponent<Animator>();
                anim.SetTrigger("SceneEnd");
                TimeManager.timeKeeper = 0;
                TimeManager.timeScore = 0;
                Player_Movement.currentHealth = Player_Movement.maxHealth;
                StartCoroutine(LoadLevel());
                return;
            }
            else
            {
                script = GameObject.Find("GameManager").GetComponent<SpawnManager>();
                script.OnArrowTouch();
            }

        }
   }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
}
