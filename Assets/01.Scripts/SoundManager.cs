using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: 사운드 관리 
public class SoundManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
