using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinAI : MonoBehaviour
{
    NavMeshAgent agent;
    bool inRange;
    [SerializeField] float damage;
    [SerializeField] float AttackSpeed;
    [SerializeField] float HP;
    [SerializeField] float armor;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(GameObject.FindGameObjectWithTag("target").transform.position);
        agent.stoppingDistance = 1f;
    }
    private void Update()
    {
        
    }
}
