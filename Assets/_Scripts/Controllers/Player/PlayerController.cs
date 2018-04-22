using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
    // Config
    public float speed = 10.0f;
    public float ZAimFactor = 1.25f;

    // MeleeHitzone
    public string MeleeHitzoneTag = "MeleeHitzone";
    public Vector3 MeleeHitzoneOffset;
    public float MeleeHitzoneScale = 3.0f;
    private MeleeController meleeController;

    // Components
    private Vector3 Movement;

    // Environmental
    public bool inTutorial = false;
    public bool isPaused;

    // Projectile
    public GameObject Ammunition;
    public float AmmunitionSpawnPosition = 1.0f;

    // Number of seconds between each projectile
    public float fireDelay;

    // Inventory
    public List<GameObject> Inventory;
    

    // Use this for initialization
    void Start () {
        GameObject meleeHitzone = GameObject.FindGameObjectWithTag(MeleeHitzoneTag);
        meleeHitzone.transform.localPosition = MeleeHitzoneOffset;
        meleeHitzone.transform.localScale = new Vector3(MeleeHitzoneScale, MeleeHitzoneScale);
        meleeController = meleeHitzone.GetComponent<MeleeController>();

        Movement = new Vector3(0, 0, 0);

        Inventory = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused) {
            CheckForInputs();
            ApplyMovement();
            Movement = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    void CheckForInputs() {
        Movement += transform.up * Input.GetAxis("Vertical");
        Movement += transform.right * Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Fire1")) PrimaryFire();
        if (Input.GetButtonDown("Fire2")) SecondaryFire();
    }

    void ApplyMovement() {
        transform.position += Movement.normalized * speed * Time.deltaTime;
    }

    void PrimaryFire() {
        if (Ammunition == null) return;

        GameObject projectile = Instantiate(Ammunition,
                                            transform.position + transform.forward * AmmunitionSpawnPosition,
                                            Quaternion.identity);

        ProjectileController pc = projectile.GetComponent<ProjectileController>();
        if (pc != null) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            pc.InitializeWithDirection(ray.direction);
        }
    }

    void SecondaryFire() {
        foreach(GameObject go in meleeController.Contacts) {
            EnemyController ec = go.GetComponent<EnemyController>();
            if(ec != null) {
                ec.Hit(50.0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        Amunition amunition = other.GetComponent<Amunition>();

        if (amunition != null) {
            PickUpAmunition(amunition.Damage);
            amunition.Destroy();
        }
    }

    private void PickUpAmunition(GameObject newDamage) {
        string newDamageName = newDamage.GetComponent<Damage>().DamageName;

        foreach(GameObject damage in Inventory) {
            if (damage.GetComponent<Damage>().DamageName == newDamageName)
                return;
        }

        Inventory.Add(newDamage);
    }
}
