using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//일시 정지, 해제, 메인화면으로
public class PauseMenu : MonoBehaviour {

    //일시 정지 메뉴
    public GameObject background;                        
    public GameObject pauseMenu;
    //todo 해상도, 사운드 볼륨 슬라이더  
                   
    private GameObject activeMenu;
                                                                      
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOrResume();
        }
    }

    public void PauseOrResume()
    {
        if (background.activeInHierarchy == false) //메뉴 창이 없는 상태 였다면
        {
            GameManager.instance.PlayBS();
            //이전에 초기 일시 정지 메뉴가 아닌 다른 화면이었을 경우 초기화.
            if (pauseMenu.activeInHierarchy == false)
            {
                pauseMenu.SetActive(true);
                activeMenu = pauseMenu;
                
                // soundsMenu.gameObject.SetActive(false);
                // videoMenu.gameObject.SetActive(false);
            }
            background.SetActive(true);
            Time.timeScale = 0; //정지
            
        }
        else
        {
            Resume();
        }
    }

    public void Resume()
    {
        GameManager.instance.PlayBS();
        background.SetActive(false);
        activeMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMain()
    {
        Resume();
        GameManager.instance.FadeAndLoadScene("main");
    }
}
