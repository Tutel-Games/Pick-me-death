using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    public static ComboCounter Instance { get; private set; }
    public float StreakLength;
    public int CurrentStreak = 0;
    public int ScoreMultiplier = 1;
    public event Action StreakReset;
    public event Action<int> StreakIncrease;
    public List<int> MultiplierThresholds = new();
    [SerializeField] private TMP_Text _comboText;
    [SerializeField] private MusicBlender _musicBlender;
    [SerializeField] private PlayerController _pc;
    
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

    public void IncreaseStreak()
    {
        CurrentStreak++;
        _musicBlender.MusicBlenderPoints(CurrentStreak);
        if (CurrentStreak is 25 or 50 or 75 or 100 or 125 or 150 or 175 or 200 or 225 or 250 or 275 or 300 or 325 or 350 or 375 or 400 or 425 or 450 or 475 or 500 or 525 or 550 or 575 or 600 or 625 or 650 or 675 or 700)
        {
            _pc.Rage();
        }
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
        
        _comboText.text = $"Combo x{CurrentStreak}";
        StreakIncrease?.Invoke(CurrentStreak);
    }

    public void ResetStreak()
    {
        CurrentStreak = 0;
        _currentThresholdIndex = 0;
        ScoreMultiplier = 1;
        var sequence = DOTween.Sequence()
            .Append(_comboText.transform.DOScale(new Vector3(1 - 0.2f, 1 - 0.2f, 1 - 0.2f), .1f))
            .Append(_comboText.DOFade(0, 0.1f));
        sequence.Play();
        if (!_comboText) return;
        
        _comboText.text = $"Combo x{CurrentStreak}";
        StreakReset?.Invoke();
    }

}
