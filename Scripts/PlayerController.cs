using System.Collections;
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
    bool DirLeft = true;
    bool DirRight = false;
    public bool Direction = false;
    Vector3 Scale;
    Vector3 TempPosition;

    //공격 관련 변수
    public GameObject PrefabBullet;
    GameObject Bullet;
    public float AttackSpeed;
    float delay = 0;

    //다른 플레이어
    public GameObject OtherPlayer;

    //애니메이션 관련
    Animator animator;
    public bool isMove;

    //사운드 관련
    public AudioClip mgSound1;
    public float gunSoundOutTime = 0.11f; //나중에 사운드가 추가되면 수정.
    float soundDelay = 0;

    // Use this for initialization
    void Start ()
    {
        CharacterController = GetComponent<CharacterController>();
        KeySetting();
        Scale = transform.localScale;
        TempPosition = transform.position;

        animator = GetComponent<Animator>();
        isMove = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        playerInput();
        Move();
        LeftRight();
        AnimationSetting();
    }

    //입력을 받음
    void playerInput()
    {
        //공격
        if (delay <= 0)
        {
            if (Input.GetKey(Key.Attack))
            {
                if (Direction == DirLeft)
                {
                    Instantiate(PrefabBullet, transform.position, transform.rotation).GetComponent<BulletCollider>().Direction = DirLeft;
                }
                else if(Direction == DirRight)
                {
                    Instantiate(PrefabBullet, transform.position, transform.rotation).GetComponent<BulletCollider>().Direction = DirRight;
                }
                delay = AttackSpeed;


                if (!SoundManager.instance.efxSource.isPlaying)
                {
                    switch (PlayerNumber)
                    {
                        case 1:
                            //SoundManager.instance.RandomizeSfx(mgSound1);
                            SoundManager.instance.PlaySingle(mgSound1);
                            break;
                        case 2:
                            break;
                    }

                    soundDelay = gunSoundOutTime;
                }
                   

            }
        }
        delay -= Time.deltaTime;

        if (soundDelay > 0)
            soundDelay -= Time.deltaTime;


        if (Input.GetKeyUp(Key.Attack) && soundDelay <= 0)
        {
            SoundManager.instance.efxSource.Stop();
        }

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
            if(OtherPlayer.transform.position.x < transform.position.x + 90)
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
        }
    }
}