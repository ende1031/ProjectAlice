using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Button_GameStart : MonoBehaviour
{

    public void OnGUI()
    {
        Debug.Log("버튼 눌림");
        SceneManager.LoadScene("Stage1"); // 2자리에 다음씬
    }

}