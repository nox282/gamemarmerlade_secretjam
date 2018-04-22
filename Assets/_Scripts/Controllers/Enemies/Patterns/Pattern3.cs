using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern3 : MonoBehaviour
{
    public float CircleSpeed = 5.0f;
    public float CircleSize = 10.0f;
    public float zSpeed = 1.0f;

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
            LastTimeShot = timestamp;
            return transform.parent.forward;
        }

        return Vector3.zero;
    }

    Vector3 Move(float timestamp)
    {
        // Spiral motion
        var xPos = Mathf.Sin(Time.time * CircleSpeed) * CircleSize;
        var yPos = Mathf.Cos(Time.time * CircleSpeed) * CircleSize;

        return new Vector3(xPos, yPos, 0);
    }
}
