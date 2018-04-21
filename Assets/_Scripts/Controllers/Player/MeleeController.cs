using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour {

    public List<GameObject> Contacts = new List<GameObject>();
    
    private void OnTriggerEnter(Collider other) {
        Contacts.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        Contacts.Remove(other.gameObject);
    }
}
