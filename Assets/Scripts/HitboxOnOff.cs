using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxOnOff : MonoBehaviour {

    public GameObject Hitbox;

    void HitboxOn()
    {
        Hitbox.GetComponent<HitBox>().ColPossible = true;
    }

    void HitboxOff()
    {
        Hitbox.GetComponent<HitBox>().ColPossible = false;
    }
}
