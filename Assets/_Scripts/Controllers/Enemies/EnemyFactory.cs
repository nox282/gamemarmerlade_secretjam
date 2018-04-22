﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory instance = null;

    public List<GameObject> EncounterPrefabs;       // Pre-defined groups of enemies
    public GameObject Target;

    public float SpawnFrequency = 3.0f;
    public Vector3 SpawnPosition = Vector3.zero;

    // Environmental
    public bool isPaused;

    private GameObject Encounter = null;
    private float Timer;
    private bool InCoolDown = false;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start ()
    {
        SpawnEncounter();
    }

    private IEnumerator SpawnTimer()
    {
        InCoolDown = true;
        Timer = 0;

        while (Timer < SpawnFrequency)
        {
            yield return new WaitForSeconds(1.0f);
            if (!isPaused)
                Timer++;
        }

        SpawnEncounter();
        InCoolDown = false;
    }

    void Update()
    {
        if (!isPaused)
        {
            if (CanSpawnEncounter() && !InCoolDown)
                StartCoroutine(SpawnTimer());
        }    
    }

    private bool CanSpawnEncounter()
    {
        return Encounter == null || Encounter.GetComponent<Encounter>().IsEmpty();
    }

    private void SpawnEncounter()
    {
        int index = Random.Range(0, EncounterPrefabs.Count);

        if (Encounter != null)
            DestroyObject(Encounter);

        Encounter = Instantiate(EncounterPrefabs[index], SpawnPosition, Quaternion.identity);
        Encounter.GetComponent<Encounter>().CreateEnemies(Target, transform.position);
    }    

    public void EnemyDied(GameObject enemy)
    {
        if (enemy != null)
            Encounter.GetComponent<Encounter>().RemoveEnemy(enemy);
    }
}
