﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //키설정용 구조체
    struct InputKey
    {
        public UnityEngine.KeyCode Left;
        public UnityEngine.KeyCode Right;
        public UnityEngine.KeyCode Up;
        public UnityEngine.KeyCode Down;
        public UnityEngine.KeyCode Jump;
        public UnityEngine.KeyCode Attack;
    };
    InputKey Key;

    //점프 및 이동 관련 변수
    public float Gravity; //중력
    public float Speed; //이동속도
    public float JumpPower; //점프력
    public int PlayerNumber; //플레이어 1인지 2인지
    Vector3 MoveDirection = Vector3.zero; //이동벡터
    CharacterController CharacterController;

    //좌우 방향 관련 변수
    bool DirLeft;
    bool DirRight;
    public bool Direction;
    Vector3 Scale;
    Vector3 TempPosition;

    //공격 관련 변수
    public GameObject PrefabBullet;
    GameObject Bullet;
    public float AttackSpeed;
    float Attack_Pre_delay; //공격 시전 모션
    float delay;
    Vector3 MakeBulletPosition;

    //다른 플레이어
    public GameObject OtherPlayer;

    //애니메이션 관련
    Animator animator;
    bool isMove;
    bool isAttack;

    //사운드 관련
    AudioSource shootSource;

    //목숨(하트)
    public int HartCount;
    float HitTimer;
    public bool ColPossible;

    // Use this for initialization
    void Start ()
    {
        CharacterController = GetComponent<CharacterController>();
        KeySetting();

        Scale = transform.localScale;
        TempPosition = transform.position;
        DirLeft = true;
        DirRight = false;
        Direction = false;

        animator = GetComponent<Animator>();
        isMove = false;

        ColPossible = false;
        HitTimer = 0;

        shootSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        playerInput();
        Move();
        LeftRight();
        AnimationSetting();
        HitTimer += Time.deltaTime;

        if (HartCount > 0 && HitTimer > 0.5f)
            ColPossible = true;
        else
            ColPossible = false;
    }

    //입력을 받음
    void playerInput()
    {
        //공격
        if (Input.GetKey(Key.Attack) && this.CharacterController.isGrounded)
        {
            Attack();    
        }
        if(Input.GetKeyUp(Key.Attack))
        {
            Attack_Pre_delay = 0;
            isAttack = false;
        }
       
        //총소리가 발사 간격 보다 길게 재생되지 않게 중지.
        if (shootSource.isPlaying && Time.time > delay)
        {
            shootSource.Stop();
        }

        //공격중 이동불가
        if (isAttack == false)
        {
            //점프
            if (this.CharacterController.isGrounded)
            {
                if (Input.GetKeyDown(Key.Jump))
                {
                    MoveDirection.y = JumpPower;
                }
            }
            //좌우 이동
            if (Input.GetKey(Key.Left) && Input.GetKey(Key.Right))
            {
                MoveDirection.x = 0;
            }
            else if (Input.GetKey(Key.Left))
            {
                if (OtherPlayer.transform.position.x < transform.position.x + 90)
                    MoveDirection.x = -Speed;
                else
                    MoveDirection.x = 0;
            }
            else if (Input.GetKey(Key.Right))
            {
                if (OtherPlayer.transform.position.x > transform.position.x - 90)
                    MoveDirection.x = Speed;
                else
                    MoveDirection.x = 0;
            }
            else
            {
                MoveDirection.x = 0;
            }

            //앞뒤 이동
            if (Input.GetKey(Key.Up) && Input.GetKey(Key.Down))
            {
                MoveDirection.z = 0;
            }
            else if (Input.GetKey(Key.Up))
            {
                if (OtherPlayer.transform.position.z > transform.position.z - 90)
                    MoveDirection.z = Speed;
                else
                    MoveDirection.z = 0;
            }
            else if (Input.GetKey(Key.Down))
            {
                if (OtherPlayer.transform.position.z < transform.position.z + 90)
                    MoveDirection.z = -Speed;
                else
                    MoveDirection.z = 0;
            }
            else
            {
                MoveDirection.z = 0;
            }
        }
        else
        {
            MoveDirection.x = 0;
            MoveDirection.z = 0;
        }
    }

    //이동
    void Move()
    {
        MoveDirection.x *= Mathf.Abs(new Vector2(MoveDirection.x, MoveDirection.z).normalized.x);
        MoveDirection.z *= Mathf.Abs(new Vector2(MoveDirection.x, MoveDirection.z).normalized.y);

        if (!this.CharacterController.isGrounded)
            MoveDirection.y -= Gravity * 0.5f;
        this.CharacterController.Move(MoveDirection * Time.deltaTime);

        if (MoveDirection.x == 0 && MoveDirection.z == 0)
            isMove = false;
        else
            isMove = true;
    }

    //공격
    void Attack()
    {
        isAttack = true;
        Attack_Pre_delay += Time.deltaTime;
        if (Attack_Pre_delay > 0.4f && Time.time > delay)
        {
            shootSource.Play();

            MakeBulletPosition = new Vector3(transform.position.x, transform.position.y - 3.5f, transform.position.z);
            if (Direction == DirLeft)
            {
                Instantiate(PrefabBullet, MakeBulletPosition, transform.rotation).GetComponent<BulletCollider>().Direction = DirLeft;
            }
            else if (Direction == DirRight)
            {
                Instantiate(PrefabBullet, MakeBulletPosition, transform.rotation).GetComponent<BulletCollider>().Direction = DirRight;
            }
            delay = AttackSpeed + Time.time;
        }
    }

    //플레이어 1,2에 따라 키설정
    void KeySetting()
    {
        switch (PlayerNumber)
        {
            case 1:
                Key.Up = KeyCode.T;
                Key.Left = KeyCode.F;
                Key.Down = KeyCode.G;
                Key.Right = KeyCode.H;
                Key.Jump = KeyCode.X;
                Key.Attack = KeyCode.Z;
                break;

            case 2:
                Key.Up = KeyCode.UpArrow;
                Key.Left = KeyCode.LeftArrow;
                Key.Down = KeyCode.DownArrow;
                Key.Right = KeyCode.RightArrow;
                Key.Jump = KeyCode.Slash;
                Key.Attack = KeyCode.Period;
                break;
        }
    }

    //왼쪽으로 갈땐 스프라이트를 뒤집음
    void LeftRight()
    {
        if(transform.position.x > TempPosition.x) //오른쪽으로 이동시
        {
            if (Scale.x < 0) Scale.x *= -1;
            transform.localScale = Scale;
            TempPosition = transform.position;
            Direction = DirRight;
        }
        else if (transform.position.x < TempPosition.x) //왼쪽으로 이동시
        {
            if (Scale.x > 0) Scale.x *= -1;
            transform.localScale = Scale;
            TempPosition = transform.position;
            Direction = DirLeft;
        }
    }

    void AnimationSetting()
    {
        if (animator)
        {
            animator.SetBool("isMove", isMove);
            animator.SetBool("isGround", this.CharacterController.isGrounded);
            animator.SetBool("isAttack", isAttack);
        }
    }

    public void ColHitbox()
    {
        HartCount--;
        HitTimer = 0;
    }
}