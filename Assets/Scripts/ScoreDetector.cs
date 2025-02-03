using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreDetector : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.TryGetComponent<BallMovement>(out var ball)) return;

        if (transform.position.x < 0) gameManager.AddScore(false);
        else gameManager.AddScore(true);
    }
}
