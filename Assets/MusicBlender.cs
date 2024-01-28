using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MusicBlender : MonoBehaviour
{
    [SerializeField] private AudioSource _firstSong;
    [SerializeField] private AudioSource _secoundSong;
    [SerializeField] private AudioSource _thirdSong;
    [SerializeField] private float _blendLength = 2;
    [SerializeField] private float _maxVolume = .25f;

    private float timeStartedBlending;
    private void OnEnable()
    {
        ComboCounter.Instance.StreakReset += On_StreakReset;
    }
    private void OnDisable()
    {
        ComboCounter.Instance.StreakReset -= On_StreakReset;
    }

    private void On_StreakReset()
    {
        DOVirtual.Float(_maxVolume, 0, _blendLength, (value) => {
            _firstSong.volume = _maxVolume - value;
        });
        _secoundSong.volume = 0f;
        _thirdSong.volume = 0f;
    }

    public void MusicBlenderPoints(int currentComboValue)
    {
        if(currentComboValue == 20)
        {
            DOVirtual.Float(_maxVolume, 0, _blendLength, (value) => {
                _firstSong.volume = value;
                _secoundSong.volume = _maxVolume - value;
            });
        }
        if (currentComboValue == 40)
        {
            DOVirtual.Float(_maxVolume, 0, _blendLength, (value) => {
                _secoundSong.volume = value;
                _thirdSong.volume = _maxVolume - value;
            });
        }
    }
}
