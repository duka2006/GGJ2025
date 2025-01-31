using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public static Barrier instance;
    
    public float barrierHealth = 100f;
    public float barrierHealingSpeed;
    public float barrierHealingIncrement;
    public float barrierCurrentHp;
    
    public static event Action loseEvent;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        barrierCurrentHp = barrierHealth;
    }

    private void OnEnable()
    {
        GoblinAI.attackEvent += TakeDamage;
    }

    private void OnDisable()
    {
        GoblinAI.attackEvent -= TakeDamage;
    }

    // Start is called before the first frame update
    void TakeDamage(float damage)
    {
        barrierCurrentHp -= damage;
        if (barrierCurrentHp <= 0)
        {
            loseEvent?.Invoke();
        }
    }
}
