using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Encounter : MonoBehaviour
{
    public float TargetDistanceThreshold = 0.1f;
    public float ScrollingSpeed = 10.0f;

    public List<GameObject> enemyPrefabs;
    private List<GameObject> Enemies;

    private GameObject Path;
    private Transform CP;
    private int pathIndex = 0;


    void Start()
    {
        Path = GameObject.FindGameObjectWithTag("Path");
        GetNextCP();
        transform.position = CP.transform.position;
    }

    void Update()
    {
        if (CP != null && Vector3.Distance(transform.position, CP.position) > TargetDistanceThreshold)
            transform.position += (CP.position - transform.position).normalized * ScrollingSpeed * Time.deltaTime;
        else
            GetNextCP();

        transform.LookAt(CP);
    }

    void GetNextCP()
    {
        CP = Path.transform.GetChild(pathIndex++);
    }

    public void CreateEnemies(GameObject target, Vector3 position)
    {
        Enemies = new List<GameObject>();

        for (int index = 0; index < enemyPrefabs.Count; index++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[index],
                position, Quaternion.identity);
            enemy.GetComponent<EnemyController>().LookTarget = target;
            enemy.transform.parent = gameObject.transform;
            Enemies.Add(enemy);
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        Enemies.Remove(enemy);
    }

    public bool IsEmpty()
    {
        return Enemies.Count == 0;
    }
}
