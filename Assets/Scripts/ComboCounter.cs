using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    public static ComboCounter Instance { get; private set; }
    public float StreakLength;
    public int CurrentStreak = 0;
    public int ScoreMultiplier = 1;
    public List<int> MultiplierThresholds = new();
    [SerializeField] private TMP_Text _comboText;
    [SerializeField] private float _timer;
    private int _currentThresholdIndex;
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        _timer = StreakLength;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0 && CurrentStreak > 0)
        {
            ResetStreak();
        }
    }

    public void IncreaseStreak()
    {
        ResetCounter();
        CurrentStreak++;
        if (_currentThresholdIndex != MultiplierThresholds.Count && CurrentStreak >= MultiplierThresholds[_currentThresholdIndex])
        {
            ScoreMultiplier++;
            if (_currentThresholdIndex < MultiplierThresholds.Count)
            {
                _currentThresholdIndex++;
            }
        }
        var sequence = DOTween.Sequence()
            .Append(_comboText.DOFade(1, 0.1f))
            .Append(_comboText.transform.DOScale(new Vector3(1 + 0.2f, 1 + 0.2f, 1 + 0.2f), .1f))
            .Append(_comboText.transform.DOScale(1, .1f));
        sequence.Play();
        if (!_comboText) return;
        
        _comboText.text = $"COMBO {CurrentStreak} \n x{ScoreMultiplier}";
    }

    private void ResetCounter()
    {
        _timer = StreakLength;
    }

    private void ResetStreak()
    {
        CurrentStreak = 0;
        _currentThresholdIndex = 0;
        ScoreMultiplier = 1;
        var sequence = DOTween.Sequence()
            .Append(_comboText.transform.DOScale(new Vector3(1 - 0.2f, 1 - 0.2f, 1 - 0.2f), .1f))
            .Append(_comboText.DOFade(0, 0.1f));
        sequence.Play();
        if (!_comboText) return;
        
        _comboText.text = $"COMBO {CurrentStreak} \n x{ScoreMultiplier}";
    }
}
