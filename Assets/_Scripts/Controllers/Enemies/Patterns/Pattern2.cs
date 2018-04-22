using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern2 : MonoBehaviour
{
    public float Frequency = 0.5f;
    public float Magnitude = 1.0f;

    private float LastTimeShot = 1;
    private int ShotsFired = 0;

    
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
        if (LastTimeShot == timestamp && ShotsFired % 2 == 0)
        {
            ShotsFired++;
            return new Vector3(0, 0, timestamp * 2);
        }

        else if (LastTimeShot != timestamp)
        {
            LastTimeShot = timestamp;
            ShotsFired = 0;
        }

        return Vector3.zero;
    }


    Vector3 Move(float timestamp)
    {
        // Sine wave motion
        return new Vector3(Mathf.Sin(Time.time * Frequency) * Magnitude, 0,  0);
    }
}
