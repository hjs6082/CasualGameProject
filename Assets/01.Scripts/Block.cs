using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Trap
}
public class Block : MonoBehaviour
{
    public BlockType blockType;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (blockType)
            {
                case BlockType.Trap:
                    GameManager.Instance.GameOver();
                    break;
                default:
                    break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (blockType)
            {
                case BlockType.Trap:
                    GameManager.Instance.GameOver();
                    break;
                default:
                    break;
            }
        }
    }
}
