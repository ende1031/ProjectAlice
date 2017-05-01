using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    public Sprite AliceSprite;
    public Sprite DianaSprite;
    public Text Name;
    public Text Text;
    public Image Illust;
    public Image Box;

    int DialogueNum;
    bool DialogueEnd;

    // Use this for initialization
    void Start () {
        DialogueNum = 0;
        DialogueEnd = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(DialogueEnd)
                Destroy(this.gameObject);
            else
                DialogueNum++;
        }

        switch(DialogueNum)
        {
            case 0:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스(15세, 귀여움)";
                Text.text = "여긴 어디지?";
                break;
            case 1:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스(15세, 귀여움)";
                Text.text = "디아나 저거 봐! 계란이 뒹굴고 있어!";
                break;
            case 2:
                Illust.sprite = DianaSprite;
                Name.text = "디아나(3세, 원래 고양이)";
                Text.text = "뒹굴고 있달까.. 우릴 때리고 있는걸요?";
                break;
            case 3:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스(15세, 계란한테 맞는 중)";
                Text.text = "이럴수가\n괜찮아! 원래 애들은 맞으면서 크는거야.\n그리고 아직 이 게임 피격판정 안만들어서 죽을 일도 없어.";
                break;
            case 4:
                Illust.sprite = DianaSprite;
                Name.text = "디아나(3세, 맞는 중)";
                Text.text = "...\n그러면 하트 UI는 뭐하러 구현한거야;";
                break;
            case 5:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스";
                Text.text = "그보다 디아나 너 어떻게 말을 하게 된거야?\n아니 언제 인간 모습이 된거야?";
                break;
            case 6:
                Illust.sprite = DianaSprite;
                Name.text = "디아나(수인)";
                Text.text = "이 팀 프로그래머 한명이 린족 빠라서 그래요.";
                break;
            case 7:
                Illust.sprite = AliceSprite;
                Name.text = "앨리스";
                Text.text = "납득.";
                DialogueEnd = true;
                break;
        }
    }
}
