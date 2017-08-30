using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy {

    public GameObject model;
    public float timeToRotate = 2;
    public float rotationSpeed = 6f;

    public GameObject bulletPrefab;

    private int targetAngle;
    private float rotationTimer;

	// Use this for initialization
	void Start () {
        rotationTimer = timeToRotate;
	}
	
	// Update is called once per frame
	void Update () {
        rotationTimer -= Time.deltaTime;
        if (rotationTimer <= 0)
        {
            rotationTimer = timeToRotate;
            targetAngle += 90;
            if (bulletPrefab != null)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = new Vector3(transform.position.x,
                    transform.position.y + 0.7f,
                    transform.position.z) + model.transform.forward;
                bullet.transform.forward = model.transform.forward;
            }
        
        }

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, targetAngle, 0), Time.deltaTime * rotationSpeed);
	}
}
