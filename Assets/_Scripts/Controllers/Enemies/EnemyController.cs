using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    // Config
    public float MoveSpeed = 10.0f;
    public float TargetDistanceThreshold = 0.1f;
    public float AmmunitionSpawnPosition = 1.0f;

    public bool IsAIActive = true;

    public GameObject Ammunition;

    public GameObject LookTarget = null;
    public Vector3 MoveTarget = new Vector3(0.0f, 0.0f, 0.0f);

    // Environmental
    public bool InTutorial = false;
    public bool IsPaused;


    // Update is called once per frame
    void Update () {
        if (IsAIActive && !IsPaused) {
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

    public void Shoot() {
        ProjectileController pc = CreateBullet();
        if (pc != null)
            pc.Initialize(LookTarget);
    }

    public void Shoot(Vector3 target) {
        ProjectileController pc = CreateBullet();
        if (pc != null)
            pc.InitializeWithDirection(target);
    }

    private ProjectileController CreateBullet() {
        if (Ammunition == null) return null;

        GameObject projectile = Instantiate(Ammunition,
                                            transform.position + transform.forward * AmmunitionSpawnPosition,
                                            Quaternion.identity);

        return projectile.GetComponent<ProjectileController>();
    }

    public void Hit(float damage) {
        Debug.Log(gameObject.name + " hit for " + damage);
    }

    // TODO: on die, notify factory
}
