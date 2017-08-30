using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingLogic : MonoBehaviour {

    public Vector3[] directions;
    public float timeToChange = 3f;
    public float movementSpeed = 3f;

    private int directionPointer;
    private float directionTimer;

	// Use this for initialization
	void Start () {
        directionPointer = 0;
        directionTimer = timeToChange;
	}
	
	// Update is called once per frame
	void Update () {
        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0)
        {
            directionTimer = timeToChange;
            directionPointer = ++directionPointer >= directions.Length ? 0 : directionPointer;
        }

        Rigidbody body = this.GetComponent<Rigidbody>();
        body.velocity = new Vector3(directions[directionPointer].x * movementSpeed, body.velocity.y, directions[directionPointer].z * movementSpeed);
	}
}
