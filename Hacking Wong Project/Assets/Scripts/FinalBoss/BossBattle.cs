using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBattle : MonoBehaviour
{

    public float transitionTime = 2f;
    public Animator screenTransitionAnim;
    public GameObject keyObj;
    GameObject boss;

    public void Start()
    {
        boss = GameObject.Find("Final Boss");
    }
    public void BossIsDead()
    {
        Instantiate(keyObj, boss.transform.position, Quaternion.identity);
    }
    public void OnKeyTouch()
    {
        StartCoroutine(LoadLevelNext(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevelNext(int level)
    {
        screenTransitionAnim.SetTrigger("SceneEnd");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(level);
    }
}
