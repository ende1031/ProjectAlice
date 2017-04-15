using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Button_GameStart : MonoBehaviour
{
    public AudioClip buttonSound;
    public AudioClip scene1BGM;


    public void OnGUI()
    {
        Debug.Log("버튼 눌림");

        SoundManager.instance.PlaySingle(buttonSound);

        //메인 브금 스탑.
        //SoundManager.instance.musicSource.Stop(); //클립을 바꾸면 필요 없는것으로 보임.

        //신1 브금 재생.
        SoundManager.instance.musicSource.clip = scene1BGM;
        SoundManager.instance.musicSource.Play();
 
        SceneManager.LoadScene("Stage1"); // 2자리에 다음씬
        
    }

}