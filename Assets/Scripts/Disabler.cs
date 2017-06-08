using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    public float degree, time;

    void OnTriggerEnter(Collider other)
    {
        //플레이어 슬로우
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Slow(degree, time);
            Destroy(gameObject);
        }
    }
}
