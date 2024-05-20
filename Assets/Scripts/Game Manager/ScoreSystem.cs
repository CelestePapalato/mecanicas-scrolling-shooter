using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreSystem
{
    private static int _score = 0;
    public static int Score { get => _score; }

    private static bool gameStarted = false;

    public static void AddPoints(int points)
    {
        if (!gameStarted)
        {
            return;
        }
        points = Mathf.Max(0, points);
        _score += points;
    }

    public static void UpdateGameState()
    {
        bool wasPaused = !gameStarted;
        gameStarted = GameManager.GameStarted;
        if (gameStarted && wasPaused)
        {
            _score = 0;
        }
    }

}
