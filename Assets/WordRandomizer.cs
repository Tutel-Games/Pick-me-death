using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordRandomizer : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particleSystems;
    [SerializeField] private ParticleSystem[] _particleSystems2;
    private int _random;
    
    public void PlayParticle()
    {
        _random = Random.Range(0, 3);
        var s = _particleSystems[_random];
        s.gameObject.SetActive(true);
        s.Play();
    }

    public void PlayParticle2()
    {
        _random = Random.Range(0, 3);
        var s = _particleSystems2[_random];
        s.gameObject.SetActive(true);
        s.Play();
    }
}
