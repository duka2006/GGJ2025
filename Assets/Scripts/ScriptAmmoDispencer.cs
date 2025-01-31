using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class ScriptAmmoDispencer : MonoBehaviour
{
    [SerializeField] float timeToDispence;
    [SerializeField] GameObject ammo;
    float timer = 0f;


    // Update is called once per frame
    void Update()
    {
        if (timer < timeToDispence)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            Instantiate(ammo, transform.position, Quaternion.identity);
        }
    }
}
