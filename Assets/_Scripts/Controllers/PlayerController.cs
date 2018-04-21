using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Config
    public float speed;

    // Keyboard Controls
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public int primaryShootButton;
    //public int secondaryShootButton;
    public KeyCode pause;

    // Components
    private Rigidbody body;
    private Animator anim;
    private Vector3 movement;
    public bool isPaused;

    // Environmental
    public bool inTutorial = false;

    // Projectile
    public GameObject projectilePrefab;

    // Number of seconds between each projectile
    public float fireDelay;
    

    // Use this for initialization
    void Start ()
    {
        body = GetComponent<Rigidbody>();
        movement = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (!isPaused)
        {
            CheckForInputs();
            ApplyMovement();
        }
	}

    void CheckForInputs()
    {
        if (Input.GetKey(up))
            movement += new Vector3(0, 1, 0);

        if (Input.GetKey(down))
            movement += new Vector3(0, -1, 0);

        if (Input.GetKey(right))
            movement += new Vector3(1, 0, 0);

        if (Input.GetKey(left))
            movement += new Vector3(-1, 0, 0);

        if (Input.GetMouseButton(primaryShootButton))
        {
            PrimaryShoot();
            Debug.Log("shoot!");
        }            
    }

    void ApplyMovement()
    {
        Vector3 target = transform.position + movement;

        transform.position = Vector3.MoveTowards(body.position, target, Time.deltaTime * speed);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(movement),
                Time.deltaTime * speed
            );
        }

        movement = new Vector3(0, 0, 0);
    }

    void PrimaryShoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectilePrefab.transform.position,
            Quaternion.identity) as GameObject;

        // Place projectile under player transform
        projectile.transform.parent = transform;
    }
}
