using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject m_target;

    [SerializeField] float speed = 100f;

    [SerializeField]GameObject horizontalObj = null;
    [SerializeField] GameObject verticalObj = null;
    
    

    Quaternion horRot;
    Quaternion vertRot;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (m_target)
        {
            /*
            horRot = Quaternion.LookRotation(m_target.transform.position - transform.position, Vector3.up);
            if (horizontalObj.transform.rotation != horRot)
            {
                horizontalObj.transform.rotation = Quaternion.RotateTowards(horizontalObj.transform.rotation, horRot, speed * Time.deltaTime);
            }
            */
            horizontalObj.transform.LookAt(m_target.transform.position);
            verticalObj.transform.LookAt(m_target.transform.position);
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
