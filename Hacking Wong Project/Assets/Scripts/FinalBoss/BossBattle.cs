using Pathfinding;
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
    public GameObject explosion;

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

    public void InstantiateExplosions()
    {
        StartCoroutine(Exp());
    }

    IEnumerator Exp()
    {
        for(int i=0; i<20; i++)
        {
            float xPos = 4 - (Random.value * 8);
            float yPos = 2.5f - (Random.value * 5);
            Destroy(Instantiate(explosion, new Vector3(xPos, yPos, -1), Quaternion.identity), 0.45f);
            yield return new WaitForSeconds(0.15f);
        }
    }
}
