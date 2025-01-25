using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject m_target;

    [SerializeField] float speed = 100f;

    GameObject horizontalObj = null;
    GameObject verticalObj = null;

    Quaternion horRot;
    Quaternion vertRot;

    // Start is called before the first frame update
    void Start()
    {
        horizontalObj = transform.Find("Horizontal").gameObject;
        horizontalObj = transform.Find("Vertical").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_target)
        {
            horRot = Quaternion.LookRotation(m_target.transform.position - transform.position, Vector3.up);
            if (horizontalObj.transform.rotation != horRot)
            {
                horizontalObj.transform.rotation = Quaternion.RotateTowards(horizontalObj.transform.rotation, horRot, speed * Time.deltaTime);
            }
        }
    }
    void FindTarget()
    {

    }
    bool SetTarget(GameObject target)
    {
        if (target)
        {
            return false;
        }
        m_target = target;
        return true;
    }
}
