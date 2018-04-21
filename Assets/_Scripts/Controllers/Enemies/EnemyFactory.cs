using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory instance = null;

    public List<GameObject> EnemyPrefabs;
    public GameObject Target;

    // Spawn settings
    public int EncounterSize = 1;                   // Number of enemies to spawn at once
    public float EncounterFrequency = 0.0f;         // How often to spawn encounters
    public Vector3 EncounterSpawn = Vector3.zero;   // Coordinates where to spawn enemies

    // Environmental
    public bool isPaused;

    private float Timer;
    private List<GameObject> Encounter;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start ()
    {
        Encounter = new List<GameObject>();

        if (EncounterFrequency > 0)
            StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            Timer++;
        }
    }

    void Update ()
    {
        if (EncounterFrequency > 0 && !isPaused && Timer >= EncounterFrequency)
        {
            SpawnEncounter();
            Timer = 0;
        }
	}

    private void SpawnEncounter()
    {
        int spawnCount = EncounterSize - Encounter.Count;

        if (spawnCount > 0 && EnemyPrefabs != null && EnemyPrefabs.Count > 0)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                int index = Random.Range(0, EnemyPrefabs.Count);

                GameObject enemy = Instantiate(EnemyPrefabs[index], EncounterSpawn,
                    Quaternion.identity) as GameObject;

                enemy.GetComponent<EnemyController>().LookTarget = Target;

                Encounter.Add(enemy);
            }
        }
    }

    public void EnemyDied(GameObject enemy)
    {
        Encounter.Remove(enemy);
        DestroyObject(enemy);

        if (EncounterFrequency <= 0)
            SpawnEncounter();
    }
}
