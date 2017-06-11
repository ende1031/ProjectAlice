using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Persistent 씬에 계속 남아 다음을 관리한다.
//버튼음 등등 재생 (다른 함수도 만들어 놨는데 마땅한 소스가 없다), 씬 컨트롤(async 페이드 사용)
//나중에 세이브할 무언가가 생기면 이것을 활용하면 될 것.
public class GameManager : MonoBehaviour
{
    public AudioSource efxSource;                   //효과음(버튼 사운드) 소스
    public static GameManager instance = null;            
    public float lowPitchRange = .95f;              //피치 랜덤
    public float highPitchRange = 1.05f;            //ㄴ
    //public float musicVol;                          //볼륨 조절 //현재는 초기화에서 밖에 없다.

    public AudioClip buttonSound1;                  //효과음(버튼 사운드) 클립

    public CanvasGroup faderCanvasGroup;           //신 페이드
    public float fadeDuration = 1f;                 //private?
    private bool isFading;

    public string startingSceneName = "main";      //첫 씬 이름.

    public bool isClear;
    public GameObject UICanvas;
    public GameObject GameOverCanvas;
    public Text scoreText;
    public Text gameEndText;
    //inumerate?

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        isClear = false;
        efxSource.ignoreListenerPause = true;
 
        DontDestroyOnLoad(gameObject);

    }

    //첫 씬 로드
    private IEnumerator Start()
    {
        faderCanvasGroup.alpha = 1f;
        yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName));
        StartCoroutine(Fade(0f));
    }


    //ui창 표시
    public void ShowUI()
    {
        UICanvas.SetActive(true);
    }

    public void HideUI()
    {
        UICanvas.SetActive(false);
    }
   


    //간단한 재생. 게임 오버, 버튼 선택 등에 쓰일 것.
    public void PlaySingleEfx(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }


    //버튼 사운드를 재생하는 경우가 많을 것이라 생각되서 만듬.
    public void PlayBS()
    {
        PlaySingleEfx(buttonSound1);
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


    //신 바꿀때 다른 스크립트에서 이 함수 호출
    public void FadeAndLoadScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName));
        }
    }

    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        yield return StartCoroutine(Fade(1f));

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        if (sceneName != "persistent")
        {
            yield return StartCoroutine(LoadSceneAndSetActive(sceneName));
        }
        else
        {
            SetGameover();
        }

        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Debug.Log(sceneName);

        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);
        
    }

    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;

        faderCanvasGroup.blocksRaycasts = true;
        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;
        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha,
                fadeSpeed * Time.deltaTime);
            yield return null;
        }
        faderCanvasGroup.blocksRaycasts = false;

        isFading = false;

    }

    void SetGameover()
    {
        GameOverCanvas.SetActive(true);
        if (isClear)
            gameEndText.text = "Clear!";
        scoreText.text += UICanvas.GetComponentInChildren<Score>().ScoreCount;
        HideUI();
    }
}