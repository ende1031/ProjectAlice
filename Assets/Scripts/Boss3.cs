using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : MonoBehaviour
{

    GameObject[] Players;
    Vector3 MoveDirection;
    Vector3 TargetPos;
    Vector3 TargetPos_temp;
    GameObject AttackTarget;
    float TagetDistans; //타겟(플레이어)까지의 거리

    public float AggroRange;
    public float MoveSpeed;

    //공격
    public GameObject beam;
    public GameObject monster;
    float coolTime;

    //좌우 방향 관련 변수
    bool DirLeft;
    bool DirRight;
    public bool Direction;
    Vector3 Scale;
    Vector3 TempPosition;

    //애니메이션 관련
    Animator animator;
    bool isAttack1;
    bool isAttack2;
    bool isAttack3;

    int attackCount;

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
        isAttack1 = false;
        isAttack2 = false;
        isAttack3 = false;
        coolTime = 0;
        attackCount = 0;

        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        coolTime += Time.deltaTime;
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

        if (AttackTarget.transform.position.z > transform.position.z - 10 && AttackTarget.transform.position.z < transform.position.z + 10 && AttackTarget.GetComponent<PlayerController>().isDie == false && TagetDistans < 40.0f)
        {
            MoveDirection = Vector3.zero;

            if (coolTime > 3.0f)
            {
                Vector3 temp;
                temp = transform.position;


                if(attackCount <= 3) //거대빔
                {
                    isAttack3 = true;
                    temp.x -= 60;
                    Instantiate(beam, temp, transform.rotation);
                    temp.x += 30;
                    Instantiate(beam, temp, transform.rotation);
                    temp.x += 60;
                    Instantiate(beam, temp, transform.rotation);
                    temp.x += 30;
                    Instantiate(beam, temp, transform.rotation);

                    if (Players[0].GetComponent<PlayerController>().isDie == false)
                    {
                        temp.x = Players[0].transform.position.x;
                        temp.z = Players[0].transform.position.z;
                        Instantiate(beam, temp, transform.rotation);
                    }
                    if (Players[1].GetComponent<PlayerController>().isDie == false)
                    {
                        temp.x = Players[1].transform.position.x;
                        temp.z = Players[1].transform.position.z;
                        Instantiate(beam, temp, transform.rotation);
                    }
                    attackCount++;
                }
                else if (attackCount == 4) //소환된 몬스터에게 맞으면 슬로우
                {
                    isAttack1 = true;
                    temp.y = 8;
                    temp.x -= 10;
                    Instantiate(monster, temp, transform.rotation);
                    temp.x += 20;
                    Instantiate(monster, temp, transform.rotation);
                    attackCount++;
                }
                else if (attackCount == 5) //스턴
                {
                    isAttack2 = true;
                    Players[0].GetComponent<PlayerController>().Stun(Random.Range(1.0f,3.0f));
                    Players[1].GetComponent<PlayerController>().Stun(Random.Range(1.0f, 3.0f));
                    attackCount = 0;
                }
                coolTime = 0;
            }
            else
            {
                isAttack1 = false;
                isAttack2 = false;
                isAttack3 = false;
            }
        }
        else
        {
            isAttack1 = false;
            isAttack2 = false;
            isAttack3 = false;
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
            animator.SetBool("isAttack1", isAttack1);
            animator.SetBool("isAttack2", isAttack2);
            animator.SetBool("isAttack3", isAttack3);
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