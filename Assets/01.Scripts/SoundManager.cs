using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: ���� ���� 
public class SoundManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
