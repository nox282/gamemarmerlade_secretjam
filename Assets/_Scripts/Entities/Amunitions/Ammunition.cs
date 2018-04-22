using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    public GameObject Projectile;
    
    public GameObject GetDamageObject()
    {
        if (Projectile == null)
            return null;

        return Projectile.GetComponent<ProjectileController>().Damage;
    }

    public Damage GetDamage()
    {
        GameObject damage = GetDamageObject();
        return (damage == null) ? null : damage.GetComponent<Damage>();
    }

    public void Destroy()
    {
        DestroyObject(gameObject);
    }
}
