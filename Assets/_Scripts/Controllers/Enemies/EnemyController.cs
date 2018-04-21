using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float MoveSpeed = 10.0f;
    public float TargetDistanceThreshold = 0.1f;
    public float AmmunitionSpawnPosition = 1.0f;

    public bool IsAIActive = true;

    public GameObject Ammunition;

    public GameObject LookTarget = null;
    public Vector3 MoveTarget = new Vector3(0.0f, 0.0f, 0.0f);
	
	// Update is called once per frame
	void Update () {
        if (IsAIActive) {
            ApplyMovement();
            transform.LookAt(LookTarget.transform);
        }
	}

    private void ApplyMovement() {
        if(Vector3.Distance(transform.position, MoveTarget) > TargetDistanceThreshold)
            transform.position += (MoveTarget - transform.position).normalized * MoveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 target) {
        MoveTarget = target;
    }

    public void Target(GameObject target) {
        LookTarget = target;
    }

    public void Shoot(Vector3 target) {
        if (Ammunition == null) return;

        GameObject projectile = Instantiate(Ammunition,
                                            transform.position + transform.forward * AmmunitionSpawnPosition,
                                            transform.rotation, 
                                            transform);

        ProjectileController pc = projectile.GetComponent<ProjectileController>();
        if (pc != null)
            pc.Initialize(target);
    }
}
