using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int touchIndex; //몇번 터치했는지

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("닿았습니다.");
        if (collision.gameObject.tag == "Bee")
        {
            Debug.Log("벌이 닿았습니다.");
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
