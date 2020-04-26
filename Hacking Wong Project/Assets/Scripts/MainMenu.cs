using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public Animator anim;
    public void PlayGame(){
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
