using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amunition : MonoBehaviour
{
    public GameObject Damage;
    
    public void Destroy()
    {
        DestroyObject(gameObject);
    }
}
