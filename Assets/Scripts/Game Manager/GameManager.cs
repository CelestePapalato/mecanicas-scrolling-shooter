using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    [SerializeField] Canvas gameStart;
    [SerializeField] Canvas gameOver;
    [SerializeField] TMP_Text scoreText;

    [Header("Game Parameters")]
    [SerializeField] float pointRate = 1f;
    [SerializeField] PowerUp powerUp1;
    [SerializeField] PowerUp powerUp2;
    [SerializeField] int powerUp1Score = 500;
    [SerializeField] int powerUp2Score = 1000;

    [Header("Debug")]
    [SerializeField] bool pauseGameOnAwake = true;

    private static int _score = 0;
    private static int _scorePowerUp1 = 0;
    private static int _scorePowerUp2 = 0;
    public static int Score { get => _score; }

    private static float _gameTime = 0;
    public static float GameTime { get => _gameTime; }

    static bool gameStarted = true;
    public static bool GameStarted { get => gameStarted; }

    static Player player;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 0f;
        gameStart.gameObject.SetActive(true);
        GameObject aux = GameObject.FindGameObjectWithTag("Player");
        player = aux.GetComponent<Player>();
        if (!pauseGameOnAwake)
        {
            StartGame();
        }
    }

    private void Update()
    {
        if (gameStarted)
        {
            _gameTime += Time.deltaTime;
        }
    }

    public void AddPoints(int points)
    {
        if (!gameStarted)
        {
            return;
        }
        points = Mathf.Max(0, points);
        _score += points;
        _scorePowerUp1 += points;
        _scorePowerUp2 += points;
        scoreText.text = _score.ToString();
        if (_scorePowerUp1 >= powerUp1Score)
        {
            _scorePowerUp1 = 0;
            player.Accept(powerUp1);
        }
        if (_scorePowerUp2 >= powerUp2Score)
        {
            _scorePowerUp2 = 0;
            player.Accept(powerUp2);
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        gameStart.gameObject.SetActive(false);
        StartCoroutine(AddPoints());
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameStarted = false;
        gameOver.gameObject.SetActive(true);
        StopAllCoroutines();
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        _score = 0;
        _gameTime = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void BuffPlayer(PowerUp powerUp)
    {
        if (player)
        {
            player.Accept(powerUp);
        }
    }

    IEnumerator AddPoints()
    {
        while (gameStarted)
        {
            yield return new WaitForSeconds(pointRate);
            AddPoints(1);
        }
    }
}
