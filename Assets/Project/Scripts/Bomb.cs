using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float duration = 2f;
    public float radius = 4f;
    public GameObject explosionModel;
    public float explosionDuration = 0.5f;

    private float explosionTimer;
    private bool exploded;

	// Use this for initialization
	void Start () {
        explosionTimer = duration;

        explosionModel.transform.localScale = Vector3.one * radius; // = new Vector3(radius, radius, radius);
        explosionModel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        explosionTimer -= Time.deltaTime;

        if (explosionTimer <= 0f && !exploded)
        {
            exploded = true;
            Collider[] hitObjects = Physics.OverlapSphere(this.transform.position, radius);
            foreach (var ob in hitObjects)
            {
                if (ob.GetComponent<Enemy>() != null)
                {
                    ob.GetComponent<Enemy>().Hit();
                }
            }
            StartCoroutine(Explode());
        }
	}

    private IEnumerator Explode()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        explosionModel.SetActive(true);

        yield return new WaitForSeconds(explosionDuration);
        Destroy(this.gameObject);
    }
}
