using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour {

    public bool muted = false;

    public Sprite onSpt;
    public Sprite onSpt2;
    public Sprite offSpt;
    public Sprite offSpt2;

    public Image onImage;
    public Image offImage;
    
    public void Off()
    {
        if (muted) return;

        muted = true;

        offImage.sprite = offSpt;
        onImage.sprite = onSpt2;

        AudioListener.pause = true;
    }

    public void On()
    {
        if (!muted) return;

        offImage.sprite = offSpt2;
        onImage.sprite = onSpt;

        muted = false;

        

        AudioListener.pause = false;
    }
}
