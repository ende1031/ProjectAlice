using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2_beam_destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
