using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //TODO: ����Ŵ��� �κ� �߰�
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
