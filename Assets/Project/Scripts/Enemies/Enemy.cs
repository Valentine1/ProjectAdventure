using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;

    public virtual void Hit() {

        health--;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Sword>() != null && col.GetComponent<Sword>().IsAttacking)
        {
            this.Hit();
        }
        else if (col.GetComponent<Arrow>() != null)
        {
            this.Hit();
            Destroy(col.gameObject);
        }
    }
}
