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
    public int enemyCount;
    public Transform target;
    public int waveCount = 1;

    private void Start()
    {
        StartCoroutine(enemyDrop());
    }

    IEnumerator enemyDrop()
    {
        yield return new WaitForSeconds(1f);
        while(waveCount<=3)
        {
            for(int i=0; i<waveCount; i++)
            {
                Debug.Log("Wave " +waveCount);
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
                yield return new WaitForSeconds(2f);
            }
            waveCount += 1;
            //add checker to see if wave is defeated
            while (!checkIfPresent()) { };
            Debug.Log("Wave finished");
            //yield return new WaitForSeconds(10f);
        }
        //add arrow instantiation
        Debug.Log("Finished waves");
    }

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

    bool checkIfPresent()
    {
        bool flag = false;
        //if (GameObject.Find("Boar") || GameObject.Find("Dragon") || GameObject.Find("Frog"))
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            flag = true;
        }
        return flag;
    }
}
