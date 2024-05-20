using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Canvas gameStart;
    [SerializeField] Canvas gameOver;

    [Header("Game Parameters")]
    [SerializeField] float pointRate = 1f;
    [SerializeField] PowerUp powerUp1;
    [SerializeField] PowerUp powerUp2;
    [SerializeField] int powerUp1Score = 500;
    [SerializeField] int powerUp2Score = 1000;

    static bool gameStarted = false;
    public static bool GameStarted { get => gameStarted; }

    static Player player;

    private void Awake()
    {
        Time.timeScale = 0f;
        gameStart.gameObject.SetActive(true);
        GameObject aux = GameObject.FindGameObjectWithTag("Player");
        player = aux.GetComponent<Player>();
    }

    private void Update()
    {
        int currentPoints = ScoreSystem.Score;
        if (currentPoints % powerUp1Score == 0)
        {
            player.Accept(powerUp1);
        }
        if(currentPoints % powerUp1Score == 0)
        {
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
            ScoreSystem.AddPoints(1);
        }
    }
}
