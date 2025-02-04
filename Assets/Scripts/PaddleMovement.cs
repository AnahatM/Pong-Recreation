using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PaddleMovement : MonoBehaviour
{
    [Header("Paddle Movement Settings")]
    [SerializeField] protected Vector2 initialPosition = new(-6.5f, 0);
    [SerializeField] protected float verticalSpeed = 10f;

    protected Rigidbody2D paddleRigidbody;
    protected GameManager gameManager;

    protected virtual void Awake()
    {
        paddleRigidbody = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
        paddleRigidbody.linearVelocity = Vector2.zero;
    }
}