using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResistances : MonoBehaviour {

    public Damage[] DamageTypes;
    public float ResistanceFactor = 0.25f;

    private Dictionary<Damage, float> Resistances; 


	void Start () {
        Resistances = new Dictionary<Damage, float>();
        foreach (Damage d in DamageTypes)
            Resistances.Add(d, ResistanceFactor);
	}

    public float GetResistance (Damage damage) {
        if (damage == null)
            return 1;

        if (!Resistances.ContainsKey(damage))
            Resistances.Add(damage, 0.0f);

        return Resistances[damage];
    }
    
    public void Resist(Damage damage) {
        if (damage != null && Resistances.ContainsKey(damage)) {
            foreach (KeyValuePair<Damage, float> r in Resistances) {
                if (r.Key.DamageName == damage.DamageName)
                    Resistances[r.Key] = Mathf.Min(0.9f, Resistances[r.Key] + ResistanceFactor);
                else
                    Resistances[r.Key] = Mathf.Max(0.1f, Resistances[r.Key] - ResistanceFactor);
            }
        }
    }
}
