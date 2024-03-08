using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int touchIndex; //몇번 터치했는지
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bee")
        {
            touchIndex++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(touchIndex <= 3)
        {
            GameManager.Instance.GameOver();
        }
    }
}
