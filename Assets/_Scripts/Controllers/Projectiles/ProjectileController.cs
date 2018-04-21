using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    public float ProjectileSpeed = 10.0f;
    public float LifeSpan = 10.0f;

    private Vector3 Direction = new Vector3(0.0f, 0.0f, 0.0f);

    void Start() {
        StartCoroutine(Live(LifeSpan));
    }

    private IEnumerator Live(float span) {
        yield return new WaitForSeconds(span);
        Destroy(gameObject);
    }

    void Update() {
        ApplyMovement();
    }

    public void Initialize(GameObject target) {
        Direction = (target.transform.position - transform.position).normalized;
    }

    public void Initialize(Vector3 target) {
        Direction = (target - transform.position).normalized;
    }

    public void InitializeWithDirection(Vector3 target) {
        Direction = target;
    }

    private void ApplyMovement() { 
        transform.position += Direction.normalized * ProjectileSpeed * Time.deltaTime;
    }
}
