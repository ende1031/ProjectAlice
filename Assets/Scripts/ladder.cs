using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ladder : MonoBehaviour {

    Animator animator;
    bool ladderOn;

    GameObject[] Players;

    // Use this for initialization
    void Start ()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        animator = GetComponent<Animator>();
        ladderOn = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (animator)
        {
            animator.SetBool("ladderOn", ladderOn);
        }

        if(Players[0].transform.position.x > 460 && Players[1].transform.position.x > 460)
        {
            GameManager.instance.FadeAndLoadScene("FinalStage");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("사다리온");
            ladderOn = true;
        }
    }
}
