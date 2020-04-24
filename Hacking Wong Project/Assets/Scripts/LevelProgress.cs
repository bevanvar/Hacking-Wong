using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
//reference: https://www.youtube.com/watch?v=26d1uZ7yrfY

public class LevelProgress : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D other)
   {
        if (other.CompareTag("Player"))
        {
            //Loading level with build index
            //SceneManager.LoadScene(1);

            //Loading level with scene name
            //SceneManager.LoadScene("Level2");

            Debug.Log("Level finished");
        }
   }
}
