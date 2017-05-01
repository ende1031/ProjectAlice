using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public int ScoreCount; //실제 스코어
    int ScoreOutput; //화면출력용 스코어
    Text ScoreText;

    // Use this for initialization
    void Start () {
        ScoreText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (ScoreOutput < ScoreCount)
        {
            ScoreOutput += 27; //화면에 보이는 점수는 프레임당 이만큼씩 증가
            if (ScoreOutput >= ScoreCount)
                ScoreOutput = ScoreCount;
        }
        ScoreText.text = ScoreOutput + "";
    }
}