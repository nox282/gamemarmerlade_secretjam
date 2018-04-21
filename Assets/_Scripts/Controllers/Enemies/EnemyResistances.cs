using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResistances : MonoBehaviour {

    public Damage[] DamageTypes;
    public float ResistanceFactor = 1.25f;

    private Dictionary<Damage, float> Resistances; 

	void Start () {
        Resistances = new Dictionary<Damage, float>();
        foreach (Damage d in DamageTypes) {
            Resistances.Add(d, 1.0f);
        }
	}

    void OnCollisionEnter(Collision collision) {
        Damage d = collision.gameObject.GetComponent<Damage>();
        if (d != null && Resistances.ContainsKey(d))
            Resist(d);
    }

    private void Resist(Damage dType) {
        foreach (KeyValuePair<Damage, float> r in Resistances) {
            if (r.Key == dType)
                Resistances[r.Key] *= ResistanceFactor;
            else
                Resistances[r.Key] *= 1 - ResistanceFactor;
        }
    }
}
