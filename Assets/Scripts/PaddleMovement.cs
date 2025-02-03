using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PaddleMovement : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode downKey = KeyCode.S;

    [Header("Paddle Movement Settings")]
    [SerializeField] private Vector2 initialPosition = new(-6.5f, 0);
    [SerializeField] private float verticalSpeed = 10f;

    private Rigidbody2D paddleRigidbody;
    private GameManager gameManager;

    private Vector2 direction;

    private void Awake()
    {
        paddleRigidbody = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.IsGameActive) ProcessInput();
    }

    private void FixedUpdate()
    {
        if (gameManager.IsGameActive) MovePaddle();
    }

    private void ProcessInput()
    {
        direction = true switch
        {
            bool _ when Input.GetKey(upKey) => Vector2.up,
            bool _ when Input.GetKey(downKey) => Vector2.down,
            _ => Vector2.zero,
        };
    }

    private void MovePaddle()
    {
        if (direction.sqrMagnitude == 0) return;
        paddleRigidbody.AddForce(direction * verticalSpeed);

        // Clamp the velocity to the vertical speed
        Vector2 velocity = paddleRigidbody.linearVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -verticalSpeed, verticalSpeed);
        paddleRigidbody.linearVelocity = velocity;
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
        paddleRigidbody.linearVelocity = Vector2.zero;
    }
}