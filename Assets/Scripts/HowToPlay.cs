﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Space))
        {
            GameManager.instance.FadeAndLoadScene("main");
        }
    }
}
