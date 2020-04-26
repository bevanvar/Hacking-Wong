using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    public float timeForTransition;
    private SpawnManager script;
   void OnTriggerEnter2D(Collider2D other)
   {
        if (other.CompareTag("Player"))
        {
            script = GameObject.Find("GameManager").GetComponent<SpawnManager>();
            script.OnArrowTouch();
        }
   }
}
