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
    Quaternion MakeBulletRotation;

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
    public bool isDie;
    public int tombstoneHP;
    Hart heart;

    public bool canMove;

    //상태 이상
    enum StatusEffect : short {
        None,           //~000
        Slow = 1,       //~001
        Stun = 1 << 1}; //~010
    private StatusEffect statusFlag = StatusEffect.None;

    float slowDuration = 0;
    //float slowReserved = 0; // 슬로우 중첩 방식에 따라 사용하지 않을 것도 같아 보류.

    // Use this for initialization
    void Start ()
    {
        CharacterController = GetComponent<CharacterController>();
        KeySetting();

        HartCount = 3;

        Scale = transform.localScale;
        TempPosition = transform.position;
        DirLeft = true;
        DirRight = false;
        Direction = false;

        animator = GetComponent<Animator>();
        isMove = false;
        isAttack = false;

        
        ColPossible = false;
        HitTimer = 0;
        isDie = false;
        tombstoneHP = 5;

        canMove = false;

        shootSource = GetComponent<AudioSource>();

        Hart[] hs = GameManager.instance.UICanvas.GetComponentsInChildren<Hart>();
        heart = hs[PlayerNumber - 1];
        
    }
	
	void FixedUpdate ()
    {
        if (!isDie)
        {
            if (canMove)
            {
                playerInput();
                SyncHPUI();
            }
            Move();
        }
        LeftRight();
        AnimationSetting();
        HitTimer += Time.deltaTime;

        if (HartCount > 0 && HitTimer > 0.5f)
            ColPossible = true;
        else
            ColPossible = false;

        if (HartCount <= 0 && isDie == false)
        {
            Die();
        }

        if (tombstoneHP <= 0 && isDie == true)
        {
            revival();
        }

        if (slowDuration > 0)
            slowDuration -= Time.deltaTime;
        if (slowDuration < 0)
        {
            slowDuration = 0;
            ReleaseStatus(StatusEffect.Slow);
        }
    }

    //죽었을 때 한번만 실행
    void Die()
    {
        //죽는 효과음도 여기에
        tombstoneHP = 5;
        isDie = true;
        isMove = false;
        isAttack = false;

        if (OtherPlayer.GetComponent<PlayerController>().isDie)
        {
            GameManager.instance.FadeAndLoadScene("persistent");//GameOver
        }

    }

    //부활시 한번만 실행
    void revival()
    {
        //부활 효과음도 여기에
        HartCount = 3;
        SyncHPUI();

        isDie = false;
    }

    void SyncHPUI()
    {
        heart.SetHP(HartCount);
    }

    void ShootSound_Play()
    {
        shootSource.Play();
    }

    void ShootSound_Stop()
    {
        shootSource.Stop();
    }

    //입력을 받음
    void playerInput()
    {
        //공격
        if (Input.GetKey(Key.Attack) && this.CharacterController.isGrounded)
        {
            Attack();    
        }
        else
        { 
            Attack_Pre_delay = 0;
            isAttack = false;
        }
       
        //총소리가 발사 간격 보다 길게 재생되지 않게 중지.
        /*
        if (shootSource.isPlaying && Time.time > delay)
        {
            shootSource.Stop();
        }*/

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
        if (Attack_Pre_delay > 0.25f && Time.time > delay)
        {
            //shootSource.Play();

            MakeBulletPosition = new Vector3(transform.position.x, transform.position.y - 3.0f, transform.position.z);
            MakeBulletRotation = Quaternion.Euler(90, 180, 0);
            if (Direction == DirLeft)
            {
                Instantiate(PrefabBullet, MakeBulletPosition, MakeBulletRotation).GetComponent<BulletCollider>().Direction = DirLeft;
            }
            else if (Direction == DirRight)
            {
                Instantiate(PrefabBullet, MakeBulletPosition, MakeBulletRotation).GetComponent<BulletCollider>().Direction = DirRight;
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
            animator.SetBool("isDie", isDie);
        }
    }

    public void ColHitbox()
    {
        HartCount--;
        SyncHPUI();
        HitTimer = 0;
    }


    public void Slow(float degree, float time)
    {
        StartCoroutine(SlowByTimeAndReset(degree, time));
    }

    IEnumerator SlowByTimeAndReset(float degree, float time)
    {
        if (time > slowDuration)
            slowDuration = time;

        AddStatus(StatusEffect.Slow);

        float slowEffective = degree > Speed ? Speed : degree;

        Speed -= slowEffective;

        yield return new WaitForSeconds(time);

        Speed += slowEffective;
             
    }

    void AddStatus(StatusEffect se)
    {
        statusFlag |= se;
    }

    bool CheckStatus(StatusEffect se)
    {
        return ((statusFlag & se) != 0);
    }

    void ReleaseStatus(StatusEffect se)
    {
        statusFlag &= ~se;
    }
}