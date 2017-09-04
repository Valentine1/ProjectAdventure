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
    public float knockBackSpeed = 300f;

    [Header("Equipment")]
    public int health = 5;
    public Sword mainSword;
    public Bow bow;
    public GameObject bombPrefab;
    public float throwSpeed = 200f;
    public int bombAmount = 5;
    public int arrowAmount = 15;

    private Rigidbody playerBody;
    private bool canJump = true;
    private Quaternion targetRotation;
    private float knockBackTimer;
    // Use this for initialization
    void Start()
    {
        targetRotation = Quaternion.Euler(0, 0, 0);
        bow.gameObject.SetActive(false);
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

        if (knockBackTimer > 0)
        {
            knockBackTimer -= Time.deltaTime;
        }
        else
        {
            ProcessInput();
        }
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
            targetRotation = Quaternion.Euler(0, 90, 0);
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
            targetRotation = Quaternion.Euler(0, -90, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerBody.velocity = new Vector3(
                 playerBody.velocity.x,
                 playerBody.velocity.y,
                 movingVelocity
                );
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerBody.velocity = new Vector3(
                playerBody.velocity.x,
                playerBody.velocity.y,
                -movingVelocity
               );
            targetRotation = Quaternion.Euler(0, 180, 0);
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            mainSword.gameObject.SetActive(true);
            bow.gameObject.SetActive(false);
            mainSword.Attack();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            mainSword.gameObject.SetActive(false);
            bow.gameObject.SetActive(true);
            if (arrowAmount > 0)
            {
                arrowAmount--;
                bow.Attack();
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ThrowBomb();
        }
    }

    private void ThrowBomb()
    {
        if (bombAmount > 0)
        {
            bombAmount--;
            GameObject bomb = Instantiate(bombPrefab);
            bomb.transform.position = this.transform.position + Model.transform.forward;

            Vector3 throwDirection = (Model.transform.forward + Vector3.up).normalized;

            bomb.GetComponent<Rigidbody>().AddForce(throwDirection * throwSpeed);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<EnemyBullet>() != null)
        {
            this.Hit((this.transform.position - col.transform.position).normalized);
            Destroy(col.gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Enemy>() != null)
        {
            this.Hit((this.transform.position - col.transform.position).normalized);
        }
    }

    private void Hit(Vector3 direction)
    {
        knockBackTimer = 1f;
        Vector3 knockBackDirection = (direction + Vector3.up).normalized;
        playerBody.AddForce(knockBackDirection * knockBackSpeed);
        health--;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
