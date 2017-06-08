﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : MonoBehaviour {

    public float Speed;
    public float Range;
    Vector3 StartPosition;

    //좌우 방향 관련 변수
    bool DirLeft = true;
    bool DirRight = false;
    public bool Direction;
    Vector3 Scale;
    Vector3 TempPosition;

    public AudioClip hitSound1;
    //AudioSource source;

    public GameObject explosion;

    //점수 관련
    GameObject Score;

    // Use this for initialization
    void Start ()
    {
        StartPosition = transform.position;
        TempPosition = transform.position;
        Scale = transform.localScale;

        Score = GameObject.FindGameObjectWithTag("ScoreUI");

        //source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //이동
        if(Direction == DirLeft)
        {
            transform.Translate(new Vector3(Speed, 0, 0) * Speed * Time.deltaTime);
        }
        else if(Direction == DirRight)
        {
            transform.Translate(new Vector3(-Speed, 0, 0) * Speed * Time.deltaTime);
        }

        //Range만큼 이동시 소멸
        if(StartPosition.x - transform.position.x > Range || StartPosition.x - transform.position.x < -Range)
        {
            Destroy(this.gameObject);
        }

        LeftRight();
    }

    private void OnTriggerEnter(Collider other)
    {
        //쓸모 없는 충돌 리턴.
        if (other.gameObject.tag == "HitBox" || other.gameObject.tag == "Bullet" || other.gameObject.tag == "Bazooka")
        {
            return;
        }

        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().isDie == true)
            {
                if (this.gameObject.tag == "Bullet")
                     other.GetComponent<PlayerController>().tombstoneHP--;
                 else if (this.gameObject.tag == "Bazooka")
                       other.GetComponent<PlayerController>().tombstoneHP -= 3;
            }
            else
                return;
        }
                 
        Instantiate(explosion, transform.position, transform.rotation);
        GameManager.instance.RandomizeSfx(hitSound1);

        if (other.gameObject.tag == "monster")
        {
            Debug.Log("아무튼 충돌");
            //스코어 증가
            Score.GetComponent<Score>().ScoreCount += other.GetComponent<MonsterController>().Score;
            //해당 몬스터 삭제
            Destroy(other.gameObject);
        }

        if (this.gameObject.tag == "Bullet")
            Destroy(this.gameObject); //바주카는 삭제 안하고 관통샷        
    }

    //왼쪽으로 갈땐 스프라이트를 뒤집음
    void LeftRight()
    {
        if (Direction == DirRight) //오른쪽으로 이동시
        {
            if (Scale.x < 0) Scale.x *= -1;
            transform.localScale = Scale;
        }
        else if (Direction == DirLeft) //왼쪽으로 이동시
        {
            if (Scale.x > 0) Scale.x *= -1;
            transform.localScale = Scale;
        }
    }
}
