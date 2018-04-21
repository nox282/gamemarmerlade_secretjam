using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern3 : MonoBehaviour
{
    public float CircleSpeed = 5.0f;
    public float CircleSize = 10.0f;
    public float zSpeed = 1.0f;


	// Use this for initialization
	void Start ()
    {
        EnemyAI handler = GetComponent<EnemyAI>();
        handler.SetTargetFromPositionDelegate(Target);
        handler.SetMovementFromTimestampDelegate(Move);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    Vector3 Target(Vector3 position)
    {
        return position + new Vector3(0, 0, 1);
    }

    Vector3 Move(float timestamp)
    {
        // Spiral motion
        var xPos = Mathf.Sin(timestamp * CircleSpeed) * CircleSize;
        var yPos = Mathf.Cos(timestamp * CircleSpeed) * CircleSize;
        var zPos = zSpeed * Time.deltaTime;

        return new Vector3(xPos, yPos, zPos);
    }
}
