using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//reference: https://www.youtube.com/watch?v=26d1uZ7yrfY

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject spawnFinished;
    public int xPos;
    public int yPos;
    private int enemyCount = 0;
    Transform target;
    private int waveCount = 0;
    private int deathCount = 0;
    private bool enemies = false;

    public Animator screenTransitionAnim;
    public float transitionTime = 2f;
    private void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        target = g.transform;
        StartCoroutine(enemyDrop());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(LoadLevelNext(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    IEnumerator enemyDrop()
    {
        //wait a second after level starts
        yield return new WaitForSeconds(1f);
        while(waveCount<3)
        {
            //if enemies present
            if(enemies)
            {
                yield return new WaitForSeconds(5f);
                if (deathCount == enemyCount) enemies = false;
                continue;
            }
            //if enemies absent
            else
            {
                waveCount += 1;
                //number of enemies spawned = wave number
                for (int i = 0; i < waveCount; i++)
                {
                    generateValidPos();
                    int enemyType = Random.Range(1, 4);
                    if (enemyType == 1)
                    {
                        Instantiate(enemy1, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    }
                    else if (enemyType == 2)
                    {
                        Instantiate(enemy2, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(enemy3, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    }
                    enemyCount += 1;
                    enemies = true;
                    //wait 2 seconds before spawning next enemy
                    //yield return new WaitForSeconds(2f);
                }
            }        
        }
        //check if all enemies are killed
        //instantiate arrow
        while(true)
        {
            if (!enemies) break;
            else
            {
                yield return new WaitForSeconds(2f);
            }
        }
        Instantiate(spawnFinished, new Vector3(20, 0, 0), Quaternion.identity);
    }

    //function to generate a valid spawn position
    void generateValidPos()
    {
        int flag = 0;
        xPos = Random.Range(-14, 12);
        yPos = Random.Range(-5, 6);
        Vector2 pos = new Vector2(xPos, yPos);
        float distance = Vector2.Distance(pos, target.position);
        if (distance >= 12f)
        {
            if (xPos >= -14 && xPos <= -1 && yPos >= 0 && yPos <= 5) flag = 1;
            if (xPos >= -4 && xPos <= 11 && yPos >= -5 && yPos <= -1) flag = 1;
        }
        if (flag == 0) generateValidPos();
    }

    //function to register a new death
    public void newDeath()
    {
        deathCount += 1;
        if (deathCount == enemyCount) enemies = false;
        else enemies = true;
    }

    public void OnArrowTouch()
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
