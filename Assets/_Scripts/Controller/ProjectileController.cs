using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    // Stats
    public float speed;
    public float damage;
    public float damageDuration;

    // Delimeters
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;


	void Start ()
    {
		
	}
	
	void Update ()
    {
        float currentX = transform.position.x;
        float currentY = transform.position.y;
        if (currentX < minX || currentX > maxX || currentY < minY || currentY > maxY)
            Destroy(gameObject);
	}

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.name)
        {
            case "Enemy":
                StartCoroutine(InflictDamage(collider));
                break;
            case "Ground":
                Destroy(gameObject);
                break;
        }
    }

    private IEnumerator InflictDamage(Collider enemyCollider)
    {
        // Ignore effects of collision with enemy
        Physics.IgnoreCollision(enemyCollider, GetComponent<Collider>());

        // Hide projectile
        GetComponent<Renderer>().enabled = false;

        // Inflict damage to enemy for DAMAGE_DURATION seconds
        //enemy.GetComponent<EnemyController>().StartTakingDamage();
        yield return new WaitForSeconds(damageDuration);
        //enemy.GetComponent<EnemyController>().StopTakingDamage();

        // Destroy Projectile
        Destroy(gameObject);
    }
}
