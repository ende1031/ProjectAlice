using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject[] Players;
    public int ScreenX;
    public int ScreenY;

    public float OffsetY;
    public float OffsetZ;

    public float CameraSpeed;

    public GameObject FrontWall;
    public GameObject LeftWall;
    public GameObject RightWall;

    Vector3 TempTransform; //카메라의 위치에서 Offset을 적용하기 전 값
    Vector3 TargetTransform; //이동해야 하는 위치

    void Awake()
    {
        //윈도우 사이즈와 카메라 사이즈를 설정
        Screen.SetResolution(ScreenX, ScreenY, false);
        GetComponent<Camera>().orthographicSize = ScreenY / 20f;
    }

    // Use this for initialization
    void Start () {
        //플레이어 오브젝트를 Players 배열에 불러옴
        Players = GameObject.FindGameObjectsWithTag("Player");

        //카메라 초기 위치 설정
        TempTransform = GetAverage(Players[0].transform.position, Players[1].transform.position);
        TempTransform.y += OffsetY;
        TempTransform.z += OffsetZ;
        transform.position = TempTransform;
    }
	
	// Update is called once per frame
	void Update () {
        //카메라 위치에서 Offset을 적용하기 전 값
        TempTransform = new Vector3(transform.position.x, transform.position.y - OffsetY, transform.position.z - OffsetZ);
        //플레이어 위치의 평균값을 TargetTransform에 저장
        TargetTransform = GetAverage(Players[0].transform.position, Players[1].transform.position);

        //벽에 가까이 있으면 TargetTransform를 조정
        if (TargetTransform.x - LeftWall.transform.position.x < 48)
            TargetTransform.x = LeftWall.transform.position.x + 48;
        if (RightWall.transform.position.x - TargetTransform.x < 48)
            TargetTransform.x = RightWall.transform.position.x - 48;
        if (TargetTransform.z - FrontWall.transform.position.z < 50)
            TargetTransform.z = FrontWall.transform.position.z + 50;

        //TargetTransform을 향해 움직임
        TempTransform.x += (TempTransform.x - TargetTransform.x) / -15 * CameraSpeed * Time.deltaTime;
        TempTransform.y += (TempTransform.y - TargetTransform.y) / -15 * CameraSpeed * Time.deltaTime;
        TempTransform.z += (TempTransform.z - TargetTransform.z) / -15 * CameraSpeed * Time.deltaTime;

        //카메라가 플레이어와 붙어있으면 안되므로 살짝 위치 조정
        TempTransform.y += OffsetY;
        TempTransform.z += OffsetZ;

        //위치 적용
        transform.position = TempTransform;
    }

    //두 좌표의 평균값을 구하는 함수
    Vector3 GetAverage(Vector3 pos1, Vector3 pos2)
    {
        pos1 += pos2;   
        pos1 /= 2;

        return pos1;
    }
}
