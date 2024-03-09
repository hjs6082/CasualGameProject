using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform player;
    [SerializeField] private float pushForce = 500f; // ���� �з����� ��
    [SerializeField] private float obstaclePushForce = 200f; // ��ֹ��� �̴� ��

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
            // ��ֹ��� �о �� ����� ���� (������ ��ֹ����� ����)
            Vector2 pushDirection = collision.transform.position - transform.position;
            pushDirection.Normalize();

            // ��ֹ��� ���� ����
            collision.rigidbody.AddForce(pushDirection * obstaclePushForce);

            // �� �ڽ��� �ݴ� �������� �з���
            rb.AddForce(-pushDirection * pushForce);
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            Vector2 pushDirection = collision.transform.position - transform.position;

            rb.AddForce(-pushDirection * pushForce);
        }

    }
}
