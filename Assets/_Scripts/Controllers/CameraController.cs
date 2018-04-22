using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float TargetDistanceThreshold = 0.1f;
    public float ScrollingSpeed = 10.0f;

    private GameObject Path;
    private Transform CP;
    private int pathIndex = 0;

	// Use this for initialization
	void Start () {
        Path = GameObject.FindGameObjectWithTag("Path");
        GetNextCP();
        transform.position = CP.transform.position;
	}
	
    void GetNextCP() {
        CP = Path.transform.GetChild(pathIndex++);
    }

	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, CP.position) > TargetDistanceThreshold)
            transform.position += (CP.position - transform.position).normalized * ScrollingSpeed * Time.deltaTime;
        else
            GetNextCP();

        transform.LookAt(CP);
    }
}
