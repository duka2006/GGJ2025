using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Godess : MonoBehaviour
{
    float timer = 0f;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (timer >= Barrier.instance.barrierHealingSpeed && Barrier.instance.barrierCurrentHp <= Barrier.instance.barrierHealth)
            {
                Barrier.instance.barrierCurrentHp += Barrier.instance.barrierHealingIncrement;
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer = 0f;
        }
    }
}
