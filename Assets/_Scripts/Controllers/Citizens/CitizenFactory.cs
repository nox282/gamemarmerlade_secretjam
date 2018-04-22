using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenFactory : MonoBehaviour {
    public int CitizenPerFrame = 5;
    public int NumberOfCitizen = 100;
    //public float CleanUpRange = 5.0f;

    public List<GameObject> CitizenPrefabs;
    public List<GameObject> CitizenList;

    // Update is called once per frame
    void LateUpdate () {
        if (CitizenList.Count < NumberOfCitizen)
            FillUpCitizens(CitizenPerFrame);
	}

    private void FillUpCitizens(int maxSpawn) {
        int i = 0;
        while(CitizenList.Count < NumberOfCitizen && i < maxSpawn) {
            GameObject go = PickCitizen();
            if(go != null)
                CitizenList.Add(go);
            i++;
        }
    }

    private GameObject PickCitizen() {
        return Instantiate(CitizenPrefabs[Random.Range(0, CitizenPrefabs.Count)],
            transform.position,
            Quaternion.identity);
    }

    public void Dissmiss(CitizenController cc) {
        CitizenList.Remove(cc.gameObject);
        Destroy(cc.gameObject);
    }
}
