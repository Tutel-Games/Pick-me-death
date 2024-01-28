using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private float _shakePower = 0.5f;
    private float _multiplier = 1;
    Sequence mySequence;
    Sequence smallShake;
    Sequence avarageShake;

    private bool _isShakenado;

    private void Start()
    {
        smallShake = DOTween.Sequence();
        smallShake.Append(transform.DOShakePosition(.5f, _shakePower * .3f, 10, 90.0f));
        smallShake.Pause();
        smallShake.SetAutoKill(false);
        
        avarageShake = DOTween.Sequence();
        avarageShake.Append(transform.DOShakePosition(.5f, _shakePower * .6f, 10, 90.0f));
        avarageShake.Pause();
        avarageShake.SetAutoKill(false);
        
        mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOShakePosition(1.5f, _shakePower * 1.5f, 10, 90.0f)).OnComplete( () => _isShakenado = false);
        mySequence.SetAutoKill(false);
        mySequence.Pause();
    }

    public void Shakenado()
    {
        if (_isShakenado) return;
      
        _isShakenado = true;
        smallShake.Pause();
        avarageShake.Pause();
        mySequence.Restart();
    }
    
    public void AverageShakenado()
    {
        avarageShake.Restart();
    }
    
    public void SmallShakenado()
    {
        smallShake.Restart();
    }
}
