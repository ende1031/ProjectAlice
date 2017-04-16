using UnityEngine;
using System.Collections;

//이 프로젝트 코드에서는 각 씬마다 사운드 매니저 생성을 시도하여 이미 있다면 브금을 교체후 소멸한다. 
//디버깅(사운드 확인)이나, 버튼 같은 반복해서 사용하는 사운드의 관리가 편하기 때문에 이러한 구조를 사용하고 있는데, 
//만약 이 시도가 최적화에 문제가 심하다면 메인 외에는 전부 삭제하고, 브금 교체 코드가 신을 바꾸는 부분에 있어야 할 것이다.
public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;                   //효과음
    public AudioSource musicSource;                 //BGM
    public static SoundManager instance = null;     //싱글톤       
    public float lowPitchRange = .95f;              //피치 랜덤
    public float highPitchRange = 1.05f;            //ㄴ
    public float musicVol;                          //볼륨 조절 //현재는 초기화에서 밖에 없다.

    public AudioClip buttonSound1;                  //

    void Awake()
    {
        
        if (instance == null)
            instance = this;
        else if (instance != this) //사운드 매니저가 이전 씬에서 이미 생성이 된 경우 
        {
            //브금 교체
            SoundManager.instance.musicSource.clip = this.musicSource.clip;
            SoundManager.instance.musicSource.Play();
            //Debug.Log("instant");
            Destroy(gameObject);
        }
            

        DontDestroyOnLoad(gameObject);

        musicSource.volume = musicVol;
    }


    //간단한 재생. 게임 오버, 버튼 선택 등에 쓰일 것.
    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;

        efxSource.Play();
    }


    //클립 여러개를 받아서 그중에 랜덤으로 재생, 위에서 설정한 범위의 랜덤 피치로 재생.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        
        Debug.Log(clips[randomIndex]);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;

        efxSource.clip = clips[randomIndex];

        efxSource.Play();
    }
}