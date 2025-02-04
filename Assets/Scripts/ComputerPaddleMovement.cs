using UnityEngine;

public class ComputerPaddleMovement : PaddleMovement
{
    private BallMovement ball;

    private Vector2 direction;

    protected override void Awake()
    {
        base.Awake();
        ball = FindFirstObjectByType<BallMovement>();
    }

    private void Update()
    {
        if (gameManager.IsGameActive) CalculateMovementDirection();
    }

    private void FixedUpdate()
    {
        if (gameManager.IsGameActive) MovePaddle();
    }

    private void CalculateMovementDirection()
    {
        if (ball == null) return;

        float paddleY = transform.position.y;
        float ballY = ball.transform.position.y;

        // Add a small deadzone to prevent jittering
        if (Mathf.Abs(paddleY - ballY) < 0.1f)
        {
            direction = Vector2.zero;
            return;
        }

        direction = (ballY > paddleY) ? Vector2.up : Vector2.down;
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
}