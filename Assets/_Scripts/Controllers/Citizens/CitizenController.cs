using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenController : MonoBehaviour {
    public float MovementSpeed = 1.0f;
    public float LifeSpan = 25.0f;
    public float MaxRange = 100.0f;

    private PlayerController PC;
    private CitizenFactory CF;

    void Start() {
        Live(LifeSpan);
        PC = GameObject.FindObjectOfType<PlayerController>();
        CF = GameObject.FindObjectOfType<CitizenFactory>();
    }

    private IEnumerator Live(float lifeSpan) {
        yield return new WaitForSeconds(lifeSpan);
        Kamikaze();
    }

    void Update () {
        if (Vector3.Distance(PC.transform.position, transform.position) > MaxRange)
            Kamikaze();
        transform.position += new Vector3(0.0f, 0.0f, 1.0f) * MovementSpeed * Time.deltaTime;
	}

    private void Kamikaze() {
        CF.Dissmiss(this);
    }
}
