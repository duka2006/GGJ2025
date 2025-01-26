using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{ 
    public int typeOfPickUp = 0; //0 - default, 1-spears, 2-mortar shells, 3-crystals
    public float weight;
    public static event Action<GameObject> pickUpEvent;
    private Rigidbody rb;
    LayerMask ground = 3;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        rb.AddForce(new Vector3(0, -1, 0), ForceMode.Force);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickUpEvent?.Invoke(gameObject);
        }
    }
}
