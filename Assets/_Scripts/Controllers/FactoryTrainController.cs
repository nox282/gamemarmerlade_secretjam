using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryTrainController : MonoBehaviour {
    public int DistanceFromCamera = 6;

    private CameraController cameraController;
    private GameObject Path;
    private Transform CP;
    public int pathIndex = 0;

    // Use this for initialization
    void Start () {
        Path = GameObject.FindGameObjectWithTag("Path");
        cameraController = GameObject.FindObjectOfType<CameraController>();
        GetNextCP();
    }

    void GetNextCP() {
        pathIndex = cameraController.pathIndex + DistanceFromCamera;
        CP = Path.transform.GetChild(pathIndex);
    }

    // Update is called once per frame
    void Update() {
        GetNextCP();
        transform.position = CP.position;
    }
}
