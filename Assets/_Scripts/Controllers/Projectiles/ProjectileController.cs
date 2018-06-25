using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    public GameObject origin;
    public float ProjectileSpeed = 10.0f;
    public float LifeSpan = 10.0f;

    public GameObject Damage;

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

    public void Initialize(GameObject originObj, GameObject target) {
        Direction = (target.transform.position - transform.position).normalized;
        InitializeOrigin(originObj);
    }

    public void Initialize(GameObject originObj, Vector3 target) {
        Direction = (target - transform.position).normalized;
        InitializeOrigin(originObj);
    }

    public void InitializeWithDirection(GameObject originObj, Vector3 target) {
        Direction = target;
        InitializeOrigin(originObj);
    }

    public void InitializeOrigin(GameObject obj) {
        origin = obj;
    }

    private void ApplyMovement() { 
        transform.position += Direction.normalized * ProjectileSpeed * Time.deltaTime;
    }
}
