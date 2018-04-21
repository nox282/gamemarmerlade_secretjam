using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern2 : MonoBehaviour
{
    public float Frequency = 0.5f;
    public float Magnitude = 1.0f;

    
	// Use this for initialization
	void Start ()
    {
        EnemyAI handler = GetComponent<EnemyAI>();
        handler.SetTargetFromTimestampDelegate(Target);
        handler.SetMovementFromTimestampDelegate(Move);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    Vector3 Target(float timestamp)
    {
        return new Vector3(0, 0, timestamp*2);
    }


    Vector3 Move(float timestamp)
    {
        // Sine wave motion
        return new Vector3(Mathf.Sin(Time.time * Frequency) * Magnitude, 0, timestamp);
    }
}
