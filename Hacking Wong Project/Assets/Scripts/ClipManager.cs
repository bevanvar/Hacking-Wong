using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipManager : MonoBehaviour
{
    public AudioSource bgmusic;
    public AudioSource ChimClip;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        bgmusic.volume = 0.25f;
        bgmusic.Play();
        ChimClip.Play();
        Invoke("Change", waitTime);
    }

    void Change()
    {
        bgmusic.volume = 1f;
    }

}
