using System.Collections;
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

    // Use this for initialization
    void Start ()
    {
        StartPosition = transform.position;
        TempPosition = transform.position;
        Scale = transform.localScale;
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
        if(other.gameObject.tag == "monster")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "object")
        {
            Destroy(this.gameObject);
        }
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
