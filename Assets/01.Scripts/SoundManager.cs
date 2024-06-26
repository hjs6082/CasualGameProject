using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //TODO: 사운드매니저 부분 추가
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
