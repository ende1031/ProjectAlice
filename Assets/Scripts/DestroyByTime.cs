using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//얼마 안되는 오브젝트들이라 성능에 큰 문제가 없어 일단은 이렇게 처리하지만, 풀링을 사용하는 것이 좋을 것 같다. 총알 등등도 마찬가지.
public class DestroyByTime : MonoBehaviour
{

    public float lifetime;


    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
