using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

    public GameObject arrowPrefab;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack()
    {
        GameObject arrow = Instantiate(arrowPrefab);
        arrow.transform.position = this.transform.position + this.transform.forward;
        arrow.transform.forward = this.transform.forward;
    }
}
