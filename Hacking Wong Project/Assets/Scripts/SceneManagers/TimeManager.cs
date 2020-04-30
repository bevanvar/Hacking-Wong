using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static int timeKeeper = 0;
    public static int timeScore = 0;
    public Text text;
    public int timePerLevel;
    // Start is called before the first frame update
    void Start()
    {
        timeKeeper += timePerLevel;
        InvokeRepeating("DeductTime", 2.5f, 1f);
    }

    void DeductTime()
    {
        timeKeeper--;
        timeScore++;
        if (timeKeeper <= 0)
        {
            SceneManager.LoadScene("DeathScene");        
        }
        if (timeKeeper <= 25)
        {
            text.color = new Color(255, 0, 0);
        }
        text.text = timeKeeper.ToString();
    }
}
