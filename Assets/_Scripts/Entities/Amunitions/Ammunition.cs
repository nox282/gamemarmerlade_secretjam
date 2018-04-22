using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    public GameObject Damage;
    public GameObject Projectile;
    
    public void Destroy()
    {
        DestroyObject(gameObject);
    }
}
