using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;                   //효과음
    public AudioSource musicSource;                 //BGM
    public static SoundManager instance = null;     //싱글톤       
    public float lowPitchRange = .95f;              //피치 랜덤
    public float highPitchRange = 1.05f;            //ㄴ


    void Awake()
    {
        
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    //간단한 재생. 게임 오버, 버튼 선택 등에 쓰일 것.
    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;

        efxSource.Play();
    }


    //클립 여러개를 받아서 그중에 랜덤으로 재생, 위에서 설정한 범위의 랜덤 피치로 재생. 이동, 점프, 총, 피격 등에 쓰일 것.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);

        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;

        efxSource.clip = clips[randomIndex];

        efxSource.Play();
    }
}