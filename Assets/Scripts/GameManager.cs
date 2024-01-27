using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Score;
    public static event Action<int> ScoreIncreased;

    public static void IncreasePoints(int multiplier)
    {
        Score += multiplier;
        ScoreIncreased?.Invoke(Score);
    }
    
    public static void ResetScore()
    {
        Score = 0;
    }
}
