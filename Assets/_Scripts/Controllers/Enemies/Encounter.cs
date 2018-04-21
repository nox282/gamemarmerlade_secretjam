using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Encounter : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    private List<GameObject> Enemies;


    public void CreateEnemies(GameObject target)
    {
        Enemies = new List<GameObject>();

        for (int index = 0; index < enemyPrefabs.Count; index++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[index],
                enemyPrefabs[index].transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyController>().LookTarget = target;
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
