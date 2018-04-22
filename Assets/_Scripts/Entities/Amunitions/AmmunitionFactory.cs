using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionFactory : MonoBehaviour {
    public List<GameObject> AmmunitionPrefabs;
    public List<GameObject> AmmunitionList;
    public GameObject Path;

    public int PathIndex = 0;

    void Start() {
        Path = GameObject.FindGameObjectWithTag("Path");
        AmmunitionList = new List<GameObject>();
    }
	
	void Update () {
        int oldPathIndex = PathIndex;
        PathIndex = Camera.main.GetComponent<CameraController>().pathIndex;
        if(PathIndex != oldPathIndex) {
            AmmunitionList.Add(PickAmmunition());
        }
	}

    private GameObject PickAmmunition() {
        return Instantiate(AmmunitionPrefabs[Random.Range(0, AmmunitionPrefabs.Count)],
            Path.transform.GetChild(PathIndex).transform.position,
            Quaternion.identity);
    }
}
