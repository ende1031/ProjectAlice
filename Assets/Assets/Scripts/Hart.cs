using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hart : MonoBehaviour {

    int HartCount;

    public Sprite Hart0;
    public Sprite Hart1;
    public Sprite Hart2;
    public Sprite Hart3;

    Image HartImage;

//gameObject.GetComponent<Image>().sprite = 바꾸려는 이미지

    // Use this for initialization
    void Start () {
        HartImage = GetComponent<Image>();
    }

    public void SetHP(int hp)
    {
        HartCount = hp;

        switch (HartCount)
        {
            case 0:
                HartImage.sprite = Hart0;
                break;
            case 1:
                HartImage.sprite = Hart1;
                break;
            case 2:
                HartImage.sprite = Hart2;
                break;
            case 3:
                HartImage.sprite = Hart3;
                break;
        }
    }
}