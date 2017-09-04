using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    public GameObject target;
    public Vector3 offset;
    
    private int delay = 4;
    private List<Vector3> delayBuffer = new List<Vector3>();
    
	// Use this for initialization
	void Start () {
       
	}
	
    void FixedUpdate()
    {
        if (target != null)
        {
            if (delayBuffer.Count < delay)
            {
                delayBuffer.Add(target.transform.position + offset);
            }
            else
            {
                this.transform.position = delayBuffer[0];

                for (int i = 1; i < delayBuffer.Count; i++)
                {
                    delayBuffer[i - 1] = delayBuffer[i];
                }

                delayBuffer[delayBuffer.Count - 1] = target.transform.position + offset;
            }
        }
    }

}
