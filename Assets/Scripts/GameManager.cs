using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI leftScoreText;
    [SerializeField] private TextMeshProUGUI rightScoreText;

    [Header("References")]
    [SerializeField] private PaddleMovement leftPaddle;
    [SerializeField] private PaddleMovement rightPaddle;

    [Header("Game Settings")]
    [SerializeField] private float countdownTime = 3f;
    [SerializeField] private int scoreToWin = 5;
    [SerializeField] private string goText = "Go!";

    public bool IsGameActive { get; private set; }

    public int LeftScore { get; set; } = 0;
    public int RightScore { get; set; } = 0;

    private BallMovement ball;

    private void Awake()
    {
        ball = FindFirstObjectByType<BallMovement>();
    }

    private void Start()
    {
        StartCoroutine(NewRound());
    }

    private IEnumerator CountdownAndStartRound()
    {
        countdownText.gameObject.SetActive(true);
        float timeLeft = countdownTime;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) timeLeft = 0;
            countdownText.text = timeLeft.ToString("F1");
            yield return null;
        }

        countdownText.text = goText;
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
        StartRound();
    }

    private void StartRound()
    {
        ball.AddStartingForce();
        IsGameActive = true;
    }

    public void AddScore(bool isLeftPaddle)
    {
        if (isLeftPaddle) LeftScore++;
        else RightScore++;
        UpdateScoreUI();
        CheckForWin();
    }

    private IEnumerator ScorePauseEffect()
    {
        IsGameActive = false;
        // Flash the ball 3 times
        for (int i = 0; i < 3; i++)
        {
            ball.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.15f);
            ball.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(0.4f); // Additional pause to complete 1 second total
    }

    private void CheckForWin()
    {
        if (LeftScore >= scoreToWin || RightScore >= scoreToWin) ResetGame();
        else StartCoroutine(NewRound());
    }

    private IEnumerator NewRound()
    {
        // Wait for score pause effect if game was active
        if (IsGameActive)
        {
            yield return StartCoroutine(ScorePauseEffect());
        }

        // Reset Positions
        ball.ResetPosition();
        leftPaddle.ResetPosition();
        rightPaddle.ResetPosition();

        // Start Game Round After Countdown
        yield return StartCoroutine(CountdownAndStartRound());
    }

    private void ResetGame()
    {
        IsGameActive = false;
        LeftScore = 0;
        RightScore = 0;
        UpdateScoreUI();
        StartCoroutine(NewRound());
    }

    private void UpdateScoreUI()
    {
        leftScoreText.text = LeftScore.ToString("00");
        rightScoreText.text = RightScore.ToString("00");
    }
}