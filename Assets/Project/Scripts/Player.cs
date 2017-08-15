using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject Model;

    [Header("Movement")]
    public float movingVelocity = 10f;
    public float jumpingVelocity = 10f;
    public float rotatingSpeed = 10f;

    [Header("Weapons")]
    public Sword mainSword;

    private Rigidbody playerBody;
    private bool canJump = true;
    private Quaternion targetRotation;
    // Use this for initialization
    void Start()
    {
        targetRotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        playerBody = this.GetComponent<Rigidbody>();
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, Vector3.down, out hit, 1.01f))
        {
            canJump = true;
        }
        Model.transform.rotation = Quaternion.Lerp(Model.transform.rotation, targetRotation, rotatingSpeed * Time.deltaTime);
        ProcessInput();
    }

    private void ProcessInput()
    {
        playerBody.velocity = new Vector3(
                  0,
                  playerBody.velocity.y,
                 0
                );

        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerBody.velocity = new Vector3(
                  movingVelocity,
                  playerBody.velocity.y,
                  playerBody.velocity.z
                );
            targetRotation = Quaternion.Euler(0, -90, 0);
            //Model.transform.rotation = Quaternion.Lerp(Model.transform.rotation, Quaternion.Euler(0, -90, 0), rotatingSpeed * Time.deltaTime);
            //Model.transform.localEulerAngles = new Vector3(0, -90, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerBody.velocity = new Vector3(
                  -movingVelocity,
                  playerBody.velocity.y,
                  playerBody.velocity.z
                );
            targetRotation = Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerBody.velocity = new Vector3(
                 playerBody.velocity.z,
                 playerBody.velocity.y,
                 movingVelocity
                );
            targetRotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerBody.velocity = new Vector3(
                playerBody.velocity.z,
                playerBody.velocity.y,
                -movingVelocity
               );
            targetRotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            //this.GetComponent<Rigidbody>().AddForce(0, jumpingForce, 0);
            playerBody.velocity = new Vector3(
                 playerBody.velocity.x,
                 jumpingVelocity,
                 playerBody.velocity.z
                );
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            mainSword.Attack();
        }
    }


}
