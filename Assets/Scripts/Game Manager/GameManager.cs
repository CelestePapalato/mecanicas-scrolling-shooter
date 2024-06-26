using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

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

    [Header("Level End")]
    [SerializeField] EnemyContainer boss;
    [SerializeField] float timeToReach = 80;

    private static int _score = 0;
    private static int _scorePowerUp1 = 0;
    private static int _scorePowerUp2 = 0;
    public static int Score { get => _score; }

    public static float GameTime { get => Time.timeSinceLevelLoad; }

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
    }
    IEnumerator ControlLevelBossSpawn()
    {
        while (Time.timeSinceLevelLoad <= timeToReach)
        {
            yield return null;
        }
        if (boss)
        {
            EnemyManager.KillAllInstances();
            EnemyContainer container = Instantiate(boss);
            container.OnDead += BossKilled;
        }
        else
        {
            GameOver();
        }
    }

    private void BossKilled(EnemyContainer boss)
    {
        GameOver();
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

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        gameStarted = true;
        gameStart.gameObject.SetActive(false);
        StartCoroutine(AddPoints());
        StartCoroutine(ControlLevelBossSpawn());
        Time.timeScale = 1f;
    }

    [ContextMenu("End Game")]
    public void GameOver()
    {
        gameStarted = false;
        gameOver.gameObject.SetActive(true);
        StopAllCoroutines();
        Time.timeScale = 0f;
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        _score = 0;
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
