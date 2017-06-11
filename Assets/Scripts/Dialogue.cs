using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    public int StageNumber;

    public Sprite AliceSprite;
    public Sprite DianaSprite;
    public Sprite Transparent;
    public Text Name;
    public Text Text;
    public Image Illust;
    public Image Box;

    int DialogueNum;
    bool DialogueEnd;

    GameObject[] Players;

    // Use this for initialization
    void Start()
    {
        DialogueNum = 0;
        DialogueEnd = false;
        Players = GameObject.FindGameObjectsWithTag("Player");

        GameManager.instance.HideUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (DialogueEnd)
            {
                Players[0].GetComponent<PlayerController>().canMove = true;
                Players[1].GetComponent<PlayerController>().canMove = true;

                GameManager.instance.ShowUI();

                Destroy(this.gameObject);
            }
            else
                DialogueNum++;
        }

        switch (StageNumber)
        {
            case 1:
                Stage1Dialogue();
                break;
            case 2:
                Stage2Dialogue();
                break;
            case 3:
                Stage3Dialogue();
                break;
        }
    }

    //스테이지1 대화
    void Stage1Dialogue()
    {
        switch (DialogueNum)
        {
            case 0:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스(10세, 귀여움)";
                Text.text = "으아아아! 떨어진다!";
                break;
            case 1:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스(10세, 귀여움)";
                Text.text = "엇! 떨어졌는데도 아무렇지도 않네?\n근데 그 토끼는 뭐야! 갑자기 발로 차다니!";
                break;
            case 2:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스(10세, 귀여움)";
                Text.text = "그런데 여기는 어디지?\n분명 나무 아래 구멍으로 떨어진 것 같은데…….";
                break;
            case 3:
                Illust.sprite = DianaSprite;
                Name.text = "디아나(3세, 원래 고양이)";
                Text.text = "냐냥?";
                break;
            case 4:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스(10세)";
                Text.text = "우왓! 디아나가 사람이 됐어!";
                break;
            case 5:
                Illust.sprite = DianaSprite;
                Name.text = "디아나(3세)";
                Text.text = "아마도 이상한 나라로 떨어진 것 같다냥.\n여기서는 나도 말을 할 수 있다냥.";
                break;
            case 6:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스";
                Text.text = "그렇구나. 완전 이해했어.\n역시 난 이해력이 좋은 것 같아.";
                break;
            case 7:
                Illust.sprite = DianaSprite;
                Name.text = "디아나";
                Text.text = "그 토끼가 우리를 여기로 보낸 것에는 분명 이유가 있을거냥.\n어쩌면…….";
                break;
            case 8:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스";
                Text.text = "어쩌면 여기서 엄마를 찾을 수 있을지도 모른단거지?";
                break;
            case 9:
                Illust.sprite = DianaSprite;
                Name.text = "디아나";
                Text.text = "그렇다냥.";
                break;
            case 10:
                Illust.sprite = DianaSprite;
                Name.text = "디아나";
                Text.text = "냐냥?!\n주인님 조심해냥! 뭔가 나타났다냥!";
                DialogueEnd = true;
                break;
        }
    }

    //스테이지2 대화
    void Stage2Dialogue()
    {
        switch (DialogueNum)
        {
            case 0:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스(10세, 귀여움)";
                Text.text = "헉…헉…….";
                break;
            case 1:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스(10세, 귀여움)";
                Text.text = "큭... 해치웠나?\n힘든 전투였어.";
                break;
            case 2:
                Illust.sprite = DianaSprite;
                Name.text = "디아나(3세, 원래 고양이)";
                Text.text = "주인님 그거 전투시에는 금칙어다냥.\n이상한 플래그 세우지 말고 긴장을 늦추지 말라냥.";
                break;
            case 3:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스(10세)";
                Text.text = "그렇지. 벌써 끝났을 리가 없지.";
                break;
            case 4:
                Illust.sprite = Transparent;
                Name.text = " ";
                Text.text = "(어디선가 정체불명의 노랫소리가 들려온다.)";
                break;
            case 5:
                Illust.sprite = Transparent;
                Name.text = "모자장수의 노래";
                Text.text = "신사숙녀 여러분!\n자, 오늘 들려드릴 노래는 저의 화려한 인생.";
                break;
            case 6:
                Illust.sprite = Transparent;
                Name.text = "모자장수의 노래";
                Text.text = "가난한 내력에서부터 막을 여는, 눈물에 눈물을 더하는 이야기랍니다~!\n자 여러분! 손에 눈물 닦을 손수건을 들어주세요!";
                break;
            case 7:
                Illust.sprite = Transparent;
                Name.text = "모자장수의 노래";
                Text.text = "라라라라~ 라라라라~ 라라라라라\n라라라라~ 라라라라~ 라라라라라";
                break;
            case 8:
                Illust.sprite = Transparent;
                Name.text = "모자장수의 노래";
                Text.text = "아-- 아--\n라라라라~ 라라라라~ 라라라라라\n아-- 아-- 라라라라~";
                break;
            case 9:
                Illust.sprite = DianaSprite;
                Name.text = "디아나";
                Text.text = "뭐냥? 가사도 없지 않냥!\n그냥 미쳤구냥.";
                break;
            case 10:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스";
                Text.text = "미친 모자장수(Mad Hatter)네.";
                break;
            case 11:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스";
                Text.text = "엄마를 찾으러 가는 길을 막는다면\n미쳤다고 해서 봐주지 않겠어!";
                break;
            case 12:
                Illust.sprite = Transparent;
                Name.text = "모자장수의 노래";
                Text.text = "아-- 아--\n라라라라~ 라라라라~ 라라라라라";
                DialogueEnd = true;
                break;
        }
    }

    //스테이지3 대화
    void Stage3Dialogue()
    {
        switch (DialogueNum)
        {
            case 0:
                Illust.sprite = DianaSprite;
                Name.text = "디아나";
                Text.text = "주인님의 엄마님의 냄새가 난다냥.";
                break;
            case 1:
                Illust.sprite = DianaSprite;
                Name.text = "디아나";
                Text.text = "저 쪽 커다란 성에 주인님의 엄마님이 있는 것 같다냥!\n고양이 직감은 확실하다냥!";
                break;
            case 2:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스";
                Text.text = "이제 진짜로 엄마를 만날 수 있어!";
                break;
            case 3:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스";
                Text.text = "하지만 저 성으로 가는게 쉽지만은 않을 것 같네.";
                break;
            case 4:
                Illust.sprite = DianaSprite;
                Name.text = "디아나";
                Text.text = "문제 없다냥. 내가 있으니까 걱정말라냥.";
                break;
            case 5:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스";
                Text.text = "디아나야말로 비석에 깔리지나 말고 잘 따라오라구!\n물론 깔리면 구해줄거지만.";
                break;
            case 6:
                Illust.sprite = DianaSprite;
                Name.text = "디아나";
                Text.text = "출발이다냥!";
                DialogueEnd = true;
                break;
        }
    }
}
