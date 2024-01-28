using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Score;
    public static int DeathEnemies;
    public static event Action<int> ScoreIncreased;

    [SerializeField] private TMP_Text _text;
    

    private void OnEnable()
    {
        DeathEnemies = 0;
    }

    public static void IncreasePoints(int multiplier)
    {
        Score += multiplier;
        ScoreIncreased?.Invoke(Score);
    }
    
    public void IncreaseDeadEnemiesCounter(int multiplier)
    {
        DeathEnemies += multiplier;
        _text.text = $"x{DeathEnemies}";
    }
    
    public static void ResetScore()
    {
        Score = 0;
    }
}
