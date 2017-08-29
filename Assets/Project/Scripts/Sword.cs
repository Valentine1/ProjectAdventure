using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Sword : MonoBehaviour
{
    public float attackDuration = 0.1f; // time for moving sword forward, after it expires sword moves back
    public float attackRetreatDuration = 0.2f;
    public bool IsAttacking
    {
        get
        {
            return isAttacking;
        }
    }


    private float startRotationX = 30f;
    private float endRotationX = 90f;
    private float attackCurrentTime;
    private float rotationCurrentX;

    private bool isAttacking;

    private Quaternion targetRotation;

    // Use this for initialization
    void Start()
    {
        targetRotation = Quaternion.Euler(startRotationX, 0, 0);
        attackCurrentTime = attackDuration;
        rotationCurrentX = startRotationX;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localRotation = targetRotation;
    }

    public void Attack()
    {
        if (rotationCurrentX <= startRotationX)
        {
            isAttacking = true;
            StartCoroutine(AttackAnimation());
        }
    }

    private IEnumerator AttackAnimation()
    {
        while (rotationCurrentX < endRotationX)
        {
            yield return null;
            float numberOfFrames = attackCurrentTime / Time.deltaTime;
            float rangeRotationX = endRotationX - rotationCurrentX;
            float stepRotationX = rangeRotationX / numberOfFrames;
            rotationCurrentX += stepRotationX;
            attackCurrentTime -= Time.deltaTime;
            targetRotation = Quaternion.Euler(rotationCurrentX, 0, 0);
        }
        if (rotationCurrentX >= endRotationX)
        {
            isAttacking = false;
            yield return null;
            attackCurrentTime = attackRetreatDuration;
            StartCoroutine(AttackRetreatAnimation());
        }
    }

    private IEnumerator AttackRetreatAnimation()
    {

        while (rotationCurrentX > startRotationX)
        {
            yield return null;
            float numberOfFrames = attackCurrentTime / Time.deltaTime;
            float rangeRotationX = startRotationX - rotationCurrentX;
            float stepRotationX = rangeRotationX / numberOfFrames;
            rotationCurrentX += stepRotationX;
            attackCurrentTime -= Time.deltaTime;
            targetRotation = Quaternion.Euler(rotationCurrentX, 0, 0);
        }
        if (rotationCurrentX <= startRotationX)
        {
            yield return null;
            attackCurrentTime = attackDuration;
            rotationCurrentX = startRotationX;
        }
    }


}
