using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BallMovement : MonoBehaviour
{
    [Header("Ball Movement Settings")]
    [SerializeField] private float speed = 200f;

    public Rigidbody2D BallRigidbody { get; private set; }

    private void Awake()
    {
        BallRigidbody = GetComponent<Rigidbody2D>();
    }

    public void ResetPosition()
    {
        BallRigidbody.linearVelocity = Vector2.zero;
        BallRigidbody.position = Vector2.zero;
    }

    public void AddStartingForce()
    {
        // Flip a coin to determine if the ball starts left or right
        float x = Random.value < 0.5f ? -1f : 1f;
        // Flip a coin to determine if the ball goes up or down.
        float y = Random.value < 0.5f ? Random.Range(-1f, -0.5f) : Random.Range(0.5f, 1f);

        Vector2 direction = new(x, y);
        BallRigidbody.AddForce(direction * speed);
    }
}