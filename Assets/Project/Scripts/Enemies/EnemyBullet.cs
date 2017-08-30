using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public float speed = 10;
    public float lifeTime = 1.5f;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }

    }
}
