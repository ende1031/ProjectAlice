using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Button_GameStart : MonoBehaviour
{
    public void OnGUI()
    {
        Debug.Log("버튼 눌림");

        GameManager.instance.PlayBS();

        //메인 브금 스탑.
        //클립을 바꾸면 필요 없는것으로 보임.

 
        SceneManager.LoadScene("Stage1"); // 2자리에 다음씬
        
    }

}