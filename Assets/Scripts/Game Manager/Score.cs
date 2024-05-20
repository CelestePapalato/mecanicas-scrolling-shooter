using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private static int _score = 0;
    public PowerUp powerUp1;
    public PowerUp powerUp2;
    private static int powerUp1Score = 500;
    private static int powerUp2Score = 1000;

    bool GameStarted = false;

    public void UpdateScore(int points)
    {
        if (!GameStarted)
        {
            return;
        }
        points = Mathf.Max(0, points);
        _score += points;
        if(_score % powerUp1Score == 0)
        {
            // Power Up 1
        }
        if(_score % powerUp2Score == 0)
        {
            // Power Up 2
        }
        StartCoroutine(AddPoints());
    }

    private void UpdateGameState(bool state)
    {
        GameStarted = state;
        if (!GameStarted)
        {
            StopCoroutine(AddPoints());
        }
        else
        {
            StartCoroutine(AddPoints());
        }
    }

    IEnumerator AddPoints()
    {
        while (GameStarted)
        {
            yield return new WaitForSeconds(1);
            _score++;
        }
    }
}
