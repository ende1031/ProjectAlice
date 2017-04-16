using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAlpa : MonoBehaviour {

    public float AlpaSpeed;
    public float StartAlpa;

    float Alpa;
    Color TempColor;
    float AlpaTimer;
    bool DecreaseAlpa; //true일때 Alpa감소

	// Use this for initialization
	void Start () {
        Alpa = StartAlpa;
        AlpaTimer = 0;
        DecreaseAlpa = false;
        TempColor = GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {
        AlpaTimer += Time.deltaTime;
        if (AlpaTimer > AlpaSpeed)
        {
            if(!DecreaseAlpa) //Alpa증가
            {
                Alpa += 0.005f;
                if (Alpa >= 0.3f)
                    DecreaseAlpa = true;
            }
            else //Alpa감소
            {
                Alpa -= 0.01f;
                if (Alpa <= 0.1f)
                    DecreaseAlpa = false;
            }
            AlpaTimer = 0.0f;
        }
        TempColor.a = Alpa;
        GetComponent<Renderer>().material.color = TempColor;
    }
}
