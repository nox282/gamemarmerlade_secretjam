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
        handler.SetTargetFromTimestampDelegate(Target);
        handler.SetMovementFromPositionDelegate(Move);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    Vector3 Target(float timestamp)
    {
        if (timestamp != LastTimeShot)
        {
            LastTimeShot = timestamp;
            return new Vector3(0, 0, timestamp);
        }

        return Vector3.zero;
    }

    Vector3 Move(Vector3 position)
    {
        // Translational motion
        return position + new Vector3(0, 0, 1);
    }
}
