using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1 : MonoBehaviour
{
    private float LastTimeShot = -1;


    // Use this for initialization
    void Start ()
    {
        EnemyAI handler = GetComponent<EnemyAI>();
        handler.SetTargetDelegate(Target);
        handler.SetMovementDelegate(Move);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    Vector3 Target(float timestamp)
    {
        if (timestamp != LastTimeShot)
        {
            // Shoot in the direction the enemy is facing
            LastTimeShot = timestamp;
            return transform.parent.forward;
        }

        return Vector3.zero;
    }

    Vector3 Move(float timestamp)
    {
        // Translational motion
        return Vector3.zero;
    }
}
