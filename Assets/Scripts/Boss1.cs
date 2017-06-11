using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{

    GameObject[] Players;
    Vector3 MoveDirection;
    Vector3 TargetPos;
    Vector3 TargetPos_temp;
    GameObject AttackTarget;
    float TagetDistans; //타겟(플레이어)까지의 거리

    public float AggroRange;
    public float MoveSpeed;

    //좌우 방향 관련 변수
    bool DirLeft;
    bool DirRight;
    public bool Direction;
    Vector3 Scale;
    Vector3 TempPosition;

    //애니메이션 관련
    Animator animator;
    bool isAttack;

    public int MaxHP;
    public int HP;

    //스토어 관련
    public int Score; //처치시 획득하는 스코어

    // Use this for initialization
    void Start()
    {
        //플레이어 오브젝트를 Players 배열에 불러옴. 플레이어가 딱 2명만 씬에 배치돼어있지 않으면 어떻게될지 모름;
        Players = GameObject.FindGameObjectsWithTag("Player");
        MoveDirection = Vector3.zero;

        Scale = transform.localScale;
        TempPosition = transform.position;
        DirLeft = true;
        DirRight = false;
        Direction = false;

        animator = transform.Find("Render Object").GetComponent<Animator>();
        isAttack = false;

        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AnimationSetting();
        LeftRight();
    }

    void Move()
    {
        TargetPos = Players[0].transform.position;
        TargetPos_temp = Players[1].transform.position;
        TagetDistans = Vector3.Magnitude(transform.position - TargetPos);
        AttackTarget = Players[0];
        if (TagetDistans > Vector3.Magnitude(transform.position - TargetPos_temp) || Players[0].GetComponent<PlayerController>().isDie == true)
        {
            if (Players[1].GetComponent<PlayerController>().isDie == false)
            {
                AttackTarget = Players[1];
                TargetPos = TargetPos_temp;
                TagetDistans = Vector3.Magnitude(transform.position - TargetPos);
            }
        }

        if (TagetDistans < 15.0f && AttackTarget.GetComponent<PlayerController>().isDie == false)
        {
            MoveDirection = Vector3.zero;
            isAttack = true;
        }
        else
        {
            isAttack = false;
            if (TagetDistans < AggroRange && AttackTarget.GetComponent<PlayerController>().isDie == false)
            {
                MoveDirection.x = (transform.position.x - TargetPos.x) / -TagetDistans * MoveSpeed * Time.deltaTime;
                MoveDirection.z = (transform.position.z - TargetPos.z) / -TagetDistans * MoveSpeed * Time.deltaTime;
            }
            else
            {
                MoveDirection = Vector3.zero;
            }
        }

        transform.Translate(MoveDirection);
    }

    //애니메이션 세팅
    void AnimationSetting()
    {
        if (animator)
        {
            animator.SetBool("isAttack", isAttack);
        }
    }

    //왼쪽 오른쪽 스프라이트 뒤집기
    void LeftRight()
    {
        if (transform.position.x > TempPosition.x) //오른쪽으로 이동시
        {
            if (Scale.x > 0) Scale.x *= -1;
            transform.localScale = Scale;
            TempPosition = transform.position;
            Direction = DirRight;
        }
        else if (transform.position.x < TempPosition.x) //왼쪽으로 이동시
        {
            if (Scale.x < 0) Scale.x *= -1;
            transform.localScale = Scale;
            TempPosition = transform.position;
            Direction = DirLeft;
        }
    }
}