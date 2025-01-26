using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class GoblinAI : MonoBehaviour
{
    NavMeshAgent agent;
    bool inRange;
    [SerializeField] float damage = 20f;
    [SerializeField] float AttackSpeed = 1f;
    [SerializeField] float HP = 50f;

    public static event Action deathEvent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(GameObject.FindGameObjectWithTag("target").transform.position);
        agent.stoppingDistance = 1f;
    }
    private void Update()
    {
        if (HP <= 0)
        {
            deathEvent?.Invoke();
            gameObject.SetActive(false);
        }
    }
    void TakeDamage(float damage, GameObject obj)
    {
            if (obj && obj == this.gameObject)
            {
                HP -= damage;
            }
    }
    private void OnEnable()
    {
        Turret.shootEvent += TakeDamage;
    }
    private void OnDisable()
    {
        Turret.shootEvent -= TakeDamage;
    }
}
