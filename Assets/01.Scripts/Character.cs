using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int touchIndex; //��� ��ġ�ߴ���

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("��ҽ��ϴ�.");
        if (collision.gameObject.tag == "Bee")
        {
            Debug.Log("���� ��ҽ��ϴ�.");
            touchIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(touchIndex >= 3 && GameManager.Instance.isLive)
        {
            GameManager.Instance.GameOver();
        }
    }
}
