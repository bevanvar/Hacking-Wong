using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalLevelManager : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        float score = TimeManager.timeKeeper * 10 + Player_Movement.currentHealth/Player_Movement.maxHealth*1500 + 350;
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", score);
        }
        text.color = new Color(0, 255, 0);
        text.text = "Completed in " + TimeManager.timeScore+" seconds\nScore: "+score;
    }
}
