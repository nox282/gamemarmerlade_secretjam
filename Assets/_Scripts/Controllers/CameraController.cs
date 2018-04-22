using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float TargetDistanceThreshold = 0.1f;
    public float ScrollingSpeed = 10.0f;
    public float RotationSpeed = 1.0f;

    private GameObject Path;
    private Transform CP;
    public int pathIndex = 0;

	// Use this for initialization
	void Start () {
        Path = GameObject.FindGameObjectWithTag("Path");
        GetNextCP();
        transform.position = CP.transform.position;
    }
	
    void GetNextCP() {
        CP = Path.transform.GetChild(pathIndex++);
    }

	void Update () {
        if (CP == null)
            GetNextCP();
        if (Vector3.Distance(transform.position, CP.position) > TargetDistanceThreshold)
            transform.position += (CP.position - transform.position).normalized * ScrollingSpeed * Time.deltaTime;
        else
            GetNextCP();

        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, (CP.transform.position - transform.position).normalized);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);
    }
}
