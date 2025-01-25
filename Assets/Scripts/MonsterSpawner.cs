using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    [SerializeField] int maxNumberOfMonstersAlive = 0;
    [SerializeField] int maxNumOfMonsters;
    int currentNumOfMonsters = 0;

    Vector3 positionOfEnemy;

    float timer = 0f;

    [SerializeField] float length, depth;
    [SerializeField] float minDistance;
    float genRanLength, genRanDepth;

    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        for (int i = 0; i<maxNumOfMonsters; i++)
        {
            SpawnMonster();
        }
        
    }
    void SpawnMonster()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        do
        {
            genRanLength = Random.Range(-length, length);
        }
        while (Vector3.Distance(transform.position, new Vector3(genRanLength, 3.38f, genRanDepth)) < minDistance);

        /*if (genRanLength <= length && genRanLength >= minLength)
        {
            positionOfEnemy.transform.position = new Vector3(length, 3.8f, depth);
        }
        else if (genRanLength >= -length && genRanLength <= -minLength)
        {
            positionOfEnemy.transform.position = new Vector3(length, 3.8f, depth);
        }
        else
        {
            SpawnMonster();
            return;
        }
        */
        Random.InitState(System.DateTime.Now.Millisecond);
        do
        {
            genRanDepth = Random.Range(-depth, depth);
        }
        while (Vector3.Distance(transform.position, new Vector3(genRanLength, 3.38f, genRanDepth)) < minDistance);
        /*
        if (genRanDepth <= depth && genRanDepth >= minDepth)
        {
            positionOfEnemy.transform.position = new Vector3(length, 3.8f, depth);
        }
        else if (genRanDepth >= -depth && genRanDepth <= -minDepth)
        {
            positionOfEnemy.transform.position = new Vector3(length, 3.8f, depth);
        }
        else
        {
            SpawnMonster();
            return;
        }
        */

        
        positionOfEnemy = new Vector3(genRanLength-0.00001f, 3.38f, genRanLength-0.00001f);
        Debug.Log(positionOfEnemy);
        Instantiate(enemy, positionOfEnemy, Quaternion.identity);
    }
}
