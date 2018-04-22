﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    // Config
    public float MoveSpeed = 10.0f;
    public float TargetDistanceThreshold = 0.1f;
    public float AmmunitionSpawnPosition = 1.0f;

    public bool IsAIActive = true;

    public GameObject Ammunition;

    public GameObject LookTarget = null;
    public Vector3 MoveTarget = new Vector3(0.0f, 0.0f, 0.0f);

    // Environmental
    public bool InTutorial = false;
    public bool IsPaused;

    public float HP = 50;

    public GameObject LabelPrefab;
    private Vector3 LabelOffset = Vector3.up;

    private GameObject Label;
    private Text HealthText;

    void Start() {
        Label = Instantiate(LabelPrefab, transform.position, Quaternion.identity);
        Label.transform.parent = FindObjectOfType<Canvas>().transform;

        foreach (Text child in Label.GetComponentsInChildren<Text>())
        {
            if (child.tag == "EnemyHealthText")
                HealthText = child;
        }

        if (HealthText != null)
            HealthText.text = "" + HP;
    }

    // Update is called once per frame
    void Update () {
        if (IsAIActive && !IsPaused) {
            ApplyMovement();
            transform.LookAt(LookTarget.transform);
        }

        Label.transform.position = Camera.main.WorldToScreenPoint(transform.position + LabelOffset);
    }

    private void ApplyMovement() {
        if (Vector3.Distance(transform.localPosition, MoveTarget) > TargetDistanceThreshold)
            transform.localPosition += (MoveTarget - transform.localPosition).normalized * MoveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 target) {
        MoveTarget = target;
    }

    public void Target(GameObject target) {
        LookTarget = target;
    }

    public void Shoot() {
        ProjectileController pc = CreateBullet();
        if (pc != null)
            pc.Initialize(LookTarget);
    }

    public void Shoot(Vector3 target) {
        ProjectileController pc = CreateBullet();
        if (pc != null)
            pc.InitializeWithDirection(target);
    }

    private ProjectileController CreateBullet() {
        if (Ammunition == null) return null;

        GameObject projectile = Instantiate(Ammunition,
                                            transform.position + transform.forward * AmmunitionSpawnPosition,
                                            Quaternion.identity);

        return projectile.GetComponent<ProjectileController>();
    }

    public void Hit(float damage)
    {
        TakeDamage(damage);
    }

    private bool TakeDamage(float damage)
    {
        if (damage < 0)
            return true;

        HP = Mathf.Max(0, HP - damage);

        if (HP == 0)
        {
            EnemyFactory factory = FindObjectOfType<EnemyFactory>();
            if (factory != null)
                factory.EnemyDied(gameObject);

            Destroy(Label);
            Destroy(gameObject);

            return false;
        }
        else
            HealthText.text = "" + HP;

        return true;
    }
   
    private void OnTriggerEnter(Collider other)
    {
        ProjectileController projectile = other.GetComponent<ProjectileController>();
        
        if (projectile != null)
        {
            Damage damage = projectile.Damage.GetComponent<Damage>();
            EnemyResistances resistances = GetComponent<EnemyResistances>();

            if (damage != null)
            {
                float damagePoints = damage.DamagePoints;

                damagePoints = damagePoints - (damagePoints * resistances.GetResistance(damage));
                
                if (TakeDamage(damagePoints))
                    resistances.Resist(damage);
            }
        }
    }
}