using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordRandomizer : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particleSystems;
    private int _random;
    void Start()
    {

    }
    [ContextMenu("playparticle")]
    public void PlayParticle()
    {
        _random = Random.Range(0, 3);
        var s = _particleSystems[_random];
        s.gameObject.SetActive(true);
        s.Play();
    }
    // Update is called once per frame
    void Update()
    {
    }
}
