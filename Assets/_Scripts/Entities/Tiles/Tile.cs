using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public static int TileCount = 0;
    public int Id;

    public GameObject[] CPReferences;

    public GameObject CheckpointPrefab;
    public GameObject Path;

    void Awake () {
        Id = TileCount++;
        Path = GameObject.FindGameObjectWithTag("Path");

    }

    void Start() {
        MakeCheckpoints();
    }

    private void MakeCheckpoints() {
        foreach(GameObject go in CPReferences) {
            GenerateCheckpoint(go);
        }
    }

    private GameObject GenerateCheckpoint(GameObject reference) {
        return Instantiate(CheckpointPrefab,
            reference.transform.position,
            Quaternion.identity,
            Path.transform);
    }
}