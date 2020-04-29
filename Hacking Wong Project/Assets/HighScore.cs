using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class HighScore : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
        float score = PlayerPrefs.GetFloat("HighScore", 0);
        text.text = "High score: " + score;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        text.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        text.enabled = false;
    }

}
