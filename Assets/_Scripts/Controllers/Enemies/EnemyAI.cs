using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public delegate Vector3 GetTarget(float timestamp);
    public delegate Vector3 GetMovement(float timestamp);

    public float FiringRate = 2.0f;
    public float MovementRate = 1.0f;

    // Environmental
    public bool isPaused;

    private GetTarget CallGetTarget;
    private GetMovement CallGetMovement;

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
        if (!isPaused)
        {
            AcquireTarget(Timer);
            AcquireMovement(Timer);
            FrameCount += Time.deltaTime;
        }
    }

    public void SetTargetDelegate(GetTarget pattern)
    {
        CallGetTarget = pattern;
    }

    public void SetMovementDelegate(GetMovement pattern)
    {
        CallGetMovement = pattern;
    }

    private void AcquireTarget(float timestamp)
    {
        if (timestamp % FiringRate == 0)
        {
            Vector3 target;

            if (CallGetTarget != null)
            {
                target = CallGetTarget(timestamp);
                if (target != Vector3.zero)
                    enemyController.Shoot(target);
            }
            else
                enemyController.Shoot();
        }
    }

    private void AcquireMovement(float timestamp)
    {
        if (FrameCount > MovementRate)
        {
            if (CallGetMovement != null)
                enemyController.MoveTo(CallGetMovement(timestamp));
            
            FrameCount = 0.0f;
        }            
    }
}
