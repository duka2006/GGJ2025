using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using TMPro;

public class Turret : MonoBehaviour
{
    

    public GameObject m_target;

    [SerializeField] float speed = 100f;

    [SerializeField] GameObject horizontalObj = null;
    [SerializeField] GameObject verticalObj = null;

    [SerializeField] float timeBetweenAttack;
    float timer;
    [SerializeField] float damage;
    [SerializeField] int maxAmmo = 100;
    [SerializeField] int startingAmmo = 40;
    [SerializeField] int ammoToAdd = 30;
    public int currentAmmo;

    Quaternion horRot;
    Quaternion verRot;

    public GameObject[] enemies;

    float minDistanceToEnemy;
    GameObject currentTarget;

    public static event Action<float, GameObject> shootEvent;
    [SerializeField] GameObject Text;  
    

    //[SerializeField] Animator animator;
    [SerializeField] Animation shoot;




    // Start is called before the first frame update
    private void Start()
    {
        currentAmmo = startingAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        SetTarget();

        if (currentAmmo <= 0)
        {
            ShutDown();
        }
        else
        {
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
        
    }
    void ShutDown()
    {
        verRot.eulerAngles = new Vector3(120, 0, verRot.eulerAngles.z);
        verticalObj.transform.rotation = Quaternion.RotateTowards(verticalObj.transform.rotation, verRot, speed * Time.deltaTime);
    }
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
            Text.GetComponent<TMP_Text>().text = currentAmmo.ToString() + "/" + maxAmmo;
            currentAmmo--;
            shoot.Play(-1, timeBetweenAttack);
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
                if (Vector3.Distance(enemies[i].transform.position, gameObject.transform.position) <= minDistanceToEnemy)
                {
                    
                    minDistanceToEnemy = Vector3.Distance(enemies[i].transform.position, gameObject.transform.position);
                    currentTarget = enemies[i];
                }
            }
            m_target = currentTarget;
        }
    }
    void AddAmmunition()
    {
        currentAmmo += ammoToAdd;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pickUp"))
        {
            PickUp pu = other.GetComponent<PickUp>();

            if (pu.typeOfPickUp == 1)
            {
                if (PlayerMovement.pm.currentPickUp != null)
                {
                    Debug.Log("IMA ITEM");
                    PlayerMovement.pm.ThrowItem();
                    other.gameObject.SetActive(false);
                }
                AddAmmunition();
            }
        }
    }
}
