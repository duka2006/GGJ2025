using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public static event Action<int> nextRound;
    [SerializeField] int numThisWave = 0;
    [SerializeField] int roundIncrement = 3;
    [SerializeField]int currentMonsters;
    [SerializeField] int waveNumber = 0;
    private void Start()
    {
        NextRound();
    }
    private void OnEnable()
    {
        GoblinAI.deathEvent += MonsterDeath;
    }
    private void OnDisable()
    {
        GoblinAI.deathEvent -= MonsterDeath;
    }
    void NextRound()
    {
        waveNumber++;
        numThisWave = waveNumber * roundIncrement;
        nextRound?.Invoke(numThisWave);
        currentMonsters = numThisWave;
    }
    void MonsterDeath()
    {
        currentMonsters--;
        if (currentMonsters <= 0)
        {
            nextRound?.Invoke(waveNumber * roundIncrement);
            NextRound();
        }
    }
}
