using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenController : MonoBehaviour {
    public float MovementSpeed = 1.0f;

    void Update () {
        transform.position += new Vector3(0.0f, 0.0f, 1.0f) * MovementSpeed * Time.deltaTime;
	}
}
