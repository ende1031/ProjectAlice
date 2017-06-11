using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceChanger : MonoBehaviour {

    public string sceneName;

    // Use this for initialization
    public void ScenceChange()
    {
        GameManager.instance.FadeAndLoadScene(sceneName);
    }

}
