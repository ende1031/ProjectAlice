using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ct_camera : MonoBehaviour
{

    float speed = 7.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y > -176.0f)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
            {
                speed = 50.0f;
            }
            else
                speed = 7.0f;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Stage1");
            }
        }

    }
}
