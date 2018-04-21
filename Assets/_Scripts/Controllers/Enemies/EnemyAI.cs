using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public delegate Vector3 GetTargetFromTimestamp(float timestamp);
    public delegate Vector3 GetTargetFromPosition(Vector3 position);
    public delegate Vector3 GetMovementFromTimestamp(float timestamp);
    public delegate Vector3 GetMovementFromPosition(Vector3 position);

    public float FiringRate = 2.0f;
    public float MovementRate = 1.0f;

    private GetTargetFromTimestamp CallGetTargetFromTimestamp;
    private GetTargetFromPosition CallGetTargetFromPosition;
    private GetMovementFromTimestamp CallGetMovementFromTimestamp;
    private GetMovementFromPosition CallGetMovementFromPosition;

    private float Timer = 0.0f;
    private float FrameCount = 0.0f;
    private EnemyController enemyController = null;


	// Use this for initialization
	void Start ()
    {
        enemyController = GetComponent<EnemyController>();
        StartCoroutine(AITimer());
	}

    private IEnumerator AITimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            Timer++;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        AcquireTarget(Timer, transform.position);
        AcquireMovement(Timer, transform.position);
        FrameCount += Time.deltaTime;
    }

    public void SetTargetFromTimestampDelegate(GetTargetFromTimestamp pattern)
    {
        CallGetTargetFromTimestamp = pattern;
    }

    public void SetTargetFromPositionDelegate(GetTargetFromPosition pattern)
    {
        CallGetTargetFromPosition = pattern;
    }

    public void SetMovementFromTimestampDelegate(GetMovementFromTimestamp pattern)
    {
        CallGetMovementFromTimestamp = pattern;
    }

    public void SetMovementFromPositionDelegate(GetMovementFromPosition pattern)
    {
        CallGetMovementFromPosition = pattern;
    }

    private void AcquireTarget(float timestamp, Vector3 position)
    {
        if (timestamp % FiringRate == 0)
        {
            Vector3 target;

            if (CallGetTargetFromTimestamp != null)
            {
                target = CallGetTargetFromTimestamp(timestamp);
                if (target != Vector3.zero)
                    enemyController.Shoot(target);
            }
            else if (CallGetTargetFromPosition != null)
            {
                target = CallGetTargetFromPosition(position);
                if (target != Vector3.zero)
                    enemyController.Shoot(target);
            }
            else
                enemyController.Shoot();
        }
    }

    private void AcquireMovement(float timestamp, Vector3 position)
    {
        if (FrameCount > MovementRate)
        {
            if (CallGetMovementFromTimestamp != null)
                enemyController.MoveTo(CallGetMovementFromTimestamp(timestamp));
            else if (CallGetMovementFromPosition != null)
                enemyController.MoveTo(CallGetMovementFromPosition(position));

            FrameCount = 0.0f;
        }
            
    }
}
