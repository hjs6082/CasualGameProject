using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform player;

    private Rigidbody2D rb;
    private Vector2 movement;
    
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
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
}
