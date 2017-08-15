using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public float swingingSpeed = 6f;
    public float coolDownSpeed = 2f;
    public float attackDuration = 0.25f; // time for moving sword forward, after it expires sword moves back

    public float coolDownDuration = 0.5f;  // time for full motion - forward and back 

    private Quaternion targetRotation;
    private float coolDownTimer = 0f;

    // Use this for initialization
    void Start()
    {
        targetRotation = Quaternion.Euler(330, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, targetRotation, swingingSpeed * Time.deltaTime);
        coolDownTimer -= Time.deltaTime;
    }

    public void Attack()
    {
        if (coolDownTimer <= 0)
        {
            targetRotation = Quaternion.Euler(270, 0, 0);
            coolDownTimer = coolDownDuration;
            StartCoroutine(CooldownWait());
        }
     
    }

    private IEnumerator CooldownWait()
    {
        yield return new WaitForSeconds(attackDuration);
        targetRotation = Quaternion.Euler(330, 0, 0);
    }
}
