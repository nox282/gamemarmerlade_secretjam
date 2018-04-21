using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public delegate Vector3 GetTarget(float timestamp);
    public delegate Vector3 GetMovement(float timestamp);

    public float FiringRate = 2.0f;
    public float MovementRate = 1.0f;


    private GetTarget CallGetTarget;
    private GetMovement CallGetMovement;

    private float Timer = 0.0f;
    private EnemyController enemyController = null;

    public EnemyAI(GetTarget _gt, GetMovement _gm) {
        CallGetTarget = _gt;
        CallGetMovement = _gm;
    }

	// Use this for initialization
	void Start () {
        enemyController = GetComponent<EnemyController>();
        StartCoroutine(AITimer());
	}

    private IEnumerator AITimer() {
        while (true) {
            yield return new WaitForSeconds(1.0f);
            Timer++;
        }
    }

	// Update is called once per frame
	void Update () {
        AcquireTarget(Timer);
	}

    private void AcquireTarget(float timestamp) {
        if (timestamp % FiringRate == 0)
            enemyController.Shoot(CallGetTarget(timestamp));
    }

    private void AcquireMovement(float timestamp) {
        if (timestamp % MovementRate == 0)
            enemyController.MoveTo(CallGetMovement(timestamp));
    }
}
