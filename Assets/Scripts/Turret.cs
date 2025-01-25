using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    public static Turret instance;

    public GameObject m_target;

    [SerializeField] float speed = 100f;

    [SerializeField] GameObject horizontalObj = null;
    [SerializeField] GameObject verticalObj = null;

    [SerializeField] float timeBetweenAttack;
    float timer;
    [SerializeField] float damage;

    Quaternion horRot;
    Quaternion verRot;

    public GameObject[] enemies;

    float minDistanceToEnemy;
    GameObject currentTarget;

    public static event Action<float, GameObject> shootEvent;
   
    //[SerializeField] Animator animator;
    [SerializeField] Animation shoot;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        SetTarget();

        if (m_target)
        {
            TrackTarget();
            if (enemies.Length != 0)
            {
                m_target = null;
                minDistanceToEnemy = 1000000000f;
                currentTarget = enemies[0];
            }   
        }
    }
    [ContextMenu("Shoot")]
    void Attack()
    {
        if (timer < timeBetweenAttack)
        {
            timer += Time.deltaTime;
        }
        else 
        {
            timer = 0;
            shootEvent?.Invoke(damage, m_target);
           // animator.SetTrigger("Shoot");
            shoot.Play();
            
        }
    }


    void TrackTarget()
    {
        
        if (m_target)
        {
            horRot = Quaternion.LookRotation(m_target.transform.position - horizontalObj.transform.position, Vector3.up);

            horRot.eulerAngles = new Vector3(0, horRot.eulerAngles.y, 0);

            horizontalObj.transform.rotation = horRot;

            verRot = Quaternion.LookRotation(m_target.transform.position - verticalObj.transform.position, Vector3.up);

            horRot.eulerAngles = new Vector3(verRot.eulerAngles.x, 0, 0);

            verticalObj.transform.rotation = verRot;
            verticalObj.transform.rotation = Quaternion.RotateTowards(verticalObj.transform.rotation, verRot, speed * Time.deltaTime);
            
             Attack();
        }
    }
    void SetTarget()
    {
        
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length != 0)
        {
            minDistanceToEnemy = Vector3.Distance(enemies[0].transform.position, gameObject.transform.position);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (gameObject.name == "Balista (1)")
                {
                    Debug.Log(minDistanceToEnemy);
                }
                if (Vector3.Distance(enemies[i].transform.position, gameObject.transform.position) <= minDistanceToEnemy)
                {
                    
                    minDistanceToEnemy = Vector3.Distance(enemies[i].transform.position, gameObject.transform.position);
                    currentTarget = enemies[i];
                }
            }
            m_target = currentTarget;
        }
    }
}
