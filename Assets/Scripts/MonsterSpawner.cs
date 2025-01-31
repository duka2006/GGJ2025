using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Instance;

    [SerializeField] GameObject enemy;

    public int maxNumOfMonsters;
    int currentNumOfMonsters = 0;

    Vector3 positionOfEnemy;

    float timer = 0f;

    [SerializeField] float length, depth;
    [SerializeField] float minDistance;
    float genRanLength, genRanDepth;
    
    void SpawnMonster(int n)
    {
        for (int i =0; i<n; i++)
        {
            Spawner();
        }
    }
     void Spawner()
    {
        do
        {
            genRanLength = Random.Range(length, depth);
        }
        while (Vector3.Distance(transform.position, new Vector3(genRanLength, 3.5f, genRanDepth)) < minDistance);

        do
        {
            genRanDepth = Random.Range(-depth, depth);
        }
        while (Vector3.Distance(transform.position, new Vector3(genRanLength, 3.5f, genRanDepth)) < minDistance);

        positionOfEnemy = new Vector3(genRanLength, 3.5f, genRanDepth);
        Instantiate(enemy, positionOfEnemy, Quaternion.identity);
    }
    private void OnEnable()
    {
        GameManager.nextRound += SpawnMonster;
    }
    private void OnDisable()
    {
        GameManager.nextRound -= SpawnMonster;
    }
}

