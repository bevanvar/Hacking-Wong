using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public int xPos;
    public int yPos;
    private int enemyCount = 0;
    public Transform target;
    private int waveCount = 0;
    private int deathCount = 0;
    private bool enemies = false;

    private void Start()
    {
        StartCoroutine(enemyDrop());
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
                    Debug.Log("Wave " + waveCount);
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
                    Debug.Log("E " + enemyCount + " D " + deathCount);
                    enemies = true;
                    //wait 2 seconds before spawning next enemy
                    yield return new WaitForSeconds(2f);
                }
            }        
        }
        Debug.Log("Finished waves");
        //check if all enemies are killed
        //instantiate arrow
    }

    //function to generate a valid spawn position
    void generateValidPos()
    {
        int flag = 0;
        xPos = Random.Range(-14, 12);
        yPos = Random.Range(-5, 6);
        Vector2 pos = new Vector2(xPos, yPos);
        float distance = Vector2.Distance(pos, target.position);
        if (distance >= 15f)
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
        Debug.Log("E " + enemyCount + " D " + deathCount);
    }

}
