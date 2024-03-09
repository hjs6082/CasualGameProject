using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform player;
    [SerializeField] private float pushForce = 500f; // 벌이 밀려나는 힘
    [SerializeField] private float obstaclePushForce = 200f; // 장애물을 미는 힘

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.Player.transform;
    }

    private void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // 장애물을 밀어낼 때 사용할 방향 (벌에서 장애물로의 방향)
            Vector2 pushDirection = collision.transform.position - transform.position;
            pushDirection.Normalize();

            // 장애물에 힘을 가함
            collision.rigidbody.AddForce(pushDirection * obstaclePushForce);

            // 벌 자신이 반대 방향으로 밀려남
            rb.AddForce(-pushDirection * pushForce);
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            Vector2 pushDirection = collision.transform.position - transform.position;

            rb.AddForce(-pushDirection * pushForce);
        }

    }
}
