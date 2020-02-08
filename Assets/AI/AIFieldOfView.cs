using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFieldOfView : MonoBehaviour
{
    public float ViewRadius;
    [Range(0, 360)]
    public float ViewAngle;

    private float baseViewAngle;

    public LayerMask targetMask;
    public LayerMask environmentMask;

    private Transform mPlayerTarget;
    public Transform PlayerTarget
    {
        get
        {
            return mPlayerTarget;
        }
        set
        {
            mPlayerTarget = value;
            ViewAngle = mPlayerTarget == null ? baseViewAngle : 360;
        }
    }
      

    private void Start()
    {
        baseViewAngle = ViewAngle;
    }

    private void Update()
    {
        FindVisibleTargets();
    }

    void FindVisibleTargets()
    {
        Transform newPos = null;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, ViewRadius, targetMask);
        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < ViewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position, dirToTarget, distToTarget, environmentMask)){
                    newPos = target;
                }
            }
        }
        PlayerTarget = newPos;
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
