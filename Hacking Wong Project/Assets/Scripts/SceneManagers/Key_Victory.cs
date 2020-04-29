using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Victory : MonoBehaviour
{
    public float timeForTransition;
    private BossBattle script;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            script = GameObject.Find("GameManager").GetComponent<BossBattle>();
            script.OnKeyTouch();
        }
    }
}
