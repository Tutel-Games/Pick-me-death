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

    private void Start()
    {
        mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOShakePosition(.5f, _shakePower, 10, 90.0f));
        mySequence.SetAutoKill(false);
        mySequence.Pause();
        
        smallShake = DOTween.Sequence();
        smallShake.Append(transform.DOShakePosition(.5f, _shakePower * .4f, 10, 90.0f));
        smallShake.SetAutoKill(false);
        smallShake.Pause();
        
        avarageShake = DOTween.Sequence();
        avarageShake.Append(transform.DOShakePosition(.5f, _shakePower * .8f, 10, 90.0f));
        avarageShake.SetAutoKill(false);
        avarageShake.Pause();
    }
    
    public void Shakenado()
    {
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
