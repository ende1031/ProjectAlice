using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    GameObject[] Players;
    Vector3 MoveDirection;
    Vector3 TargetPos;
    Vector3 TargetPos_temp;
    float TagetDistans; //타겟(플레이어)까지의 거리

    public float AggroRange;
    public float MoveSpeed;

    // Use this for initialization
    void Start ()
    {
        //플레이어 오브젝트를 Players 배열에 불러옴. 플레이어가 딱 2명만 씬에 배치돼어있지 않으면 어떻게될지 모름;
        Players = GameObject.FindGameObjectsWithTag("Player");

        MoveDirection = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }

    void Move()
    {
        TargetPos = Players[0].transform.position;
        TargetPos_temp = Players[1].transform.position;
        TagetDistans = Vector3.Magnitude(transform.position - TargetPos);
        if(TagetDistans > Vector3.Magnitude(transform.position - TargetPos_temp))
        {
            TargetPos = TargetPos_temp;
            TagetDistans = Vector3.Magnitude(transform.position - TargetPos);
        }

        if(TagetDistans < AggroRange && TagetDistans > 13.0f)
        {
            MoveDirection.x = (transform.position.x - TargetPos.x) / -TagetDistans * MoveSpeed * Time.deltaTime;
            MoveDirection.z = (transform.position.z - TargetPos.z) / -TagetDistans * MoveSpeed * Time.deltaTime;
        }
        else
        {
            MoveDirection = Vector3.zero;
        }

        transform.Translate(MoveDirection);
    }
}