using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public Animator anim;
    public AudioClip intro;
    private float timer;
    private AudioSource bgmusic;

    public void PlayGame()
    {
        bgmusic = GameObject.Find("Music").GetComponent<AudioSource>();
        bgmusic.volume = 0.5f;
        AudioSource.PlayClipAtPoint(intro, Camera.main.transform.position);
        Invoke("Proceed", 1.5f);
    }

    public void Proceed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadLevel(int level)
    {
        anim.SetTrigger("SceneEnd");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(level);
    }

    public void QuitGame(){
      Debug.Log("QUIT!");
      Application.Quit();
    }
}
