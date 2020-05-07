using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathClips : MonoBehaviour
{
    public AudioSource bgmusic;
    public AudioSource ChimClip;

    // Start is called before the first frame update
    void Start()
    {
        bgmusic.volume = 0.25f;
        bgmusic.Play();
        ChimClip.Play();
        Invoke("Change", 3.8f);
    }

    void Change()
    {
        bgmusic.volume = 1f;
    }

}
