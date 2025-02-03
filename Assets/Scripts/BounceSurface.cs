using UnityEngine;

/// <summary>
/// This class is responsible for making the ball bounce off surfaces.
/// Used for paddles and the border walls.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class BounceSurface : MonoBehaviour
{
    [Header("Bounce Settings")]
    [SerializeField] private float bounceStrength = 0.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if hit Ball
        if (!collision.gameObject.TryGetComponent<BallMovement>(out var ball)) return;

        // Apply a force to the ball in normal direction of the surface
        Vector2 normal = collision.GetContact(0).normal;
        ball.BallRigidbody.AddForce(-normal * bounceStrength, ForceMode2D.Impulse);
    }
}
