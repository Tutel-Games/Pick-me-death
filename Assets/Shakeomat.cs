using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shakeomat : MonoBehaviour
{
    public static Shakeomat Instance;
    [SerializeField] private List<Shaker> _shakers;

    private void Awake()
    {
        Instance = this;
    }

    public void ShakerShakerPartyMaker()
    {
        foreach (var shaker in _shakers)
        {
            shaker.Shakenado();
        }
    }
    
    public void SmallShakerShakerPartyMaker()
    {
        foreach (var shaker in _shakers)
        {
            shaker.SmallShakenado();
        }
    }
    
    public void AverageShakerShakerPartyMaker()
    {
        foreach (var shaker in _shakers)
        {
            shaker.AverageShakenado();
        }
    }
}
