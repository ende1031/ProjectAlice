using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Button_GameStart : MonoBehaviour
{
    public AudioClip buttonSound;


    public void OnGUI()
    {
        Debug.Log("버튼 눌림");

        SoundManager.instance.PlaySingle(buttonSound);
        //SoundManager.instance.musicSource.Stop(); //bgm을 계속 바꿀 것인가?
        SceneManager.LoadScene("Stage1"); // 2자리에 다음씬
        
    }

}