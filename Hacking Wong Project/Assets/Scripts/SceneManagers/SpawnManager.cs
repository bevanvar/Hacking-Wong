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
    private float xPos;
    private float yPos;
    public int waveLimit;
    private int enemyCount = 0;
    Transform target;
    private int waveCount = 0;
    private int deathCount = 0;
    private bool enemies = false;
    public Vector2[] spawnPositions;
    private List<int> usedPositions = new List<int>();
    public Animator screenTransitionAnim;
    public float transitionTime = 2f;
    public float arrowX;

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
        while(waveCount<waveLimit)
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
                usedPositions.Clear();
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
        Instantiate(spawnFinished, new Vector3(arrowX, 0, 0), Quaternion.identity);
    }

    //function to generate a valid spawn position
    void generateValidPos()
    {
        int flag = 0;
        int positionNumber = Random.Range(0, spawnPositions.Length);
        float distance = Vector2.Distance(spawnPositions[positionNumber], target.position);
        if (distance >= 12f && !usedPositions.Contains(positionNumber))
        {
            flag = 1;
            xPos = spawnPositions[positionNumber].x;
            yPos = spawnPositions[positionNumber].y;
            usedPositions.Add(positionNumber);
            return;
        }
        if (flag == 0) generateValidPos();
    }

    //function to register a new death
    public void newDeath()
    {
        deathCount += 1;
        Debug.Log(deathCount);
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
