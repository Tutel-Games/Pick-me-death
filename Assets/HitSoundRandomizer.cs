using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundRandomizer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private AudioClip[] _audioClap;
    [SerializeField] private AudioSource _audioSource;
    private int _random;
    private int _random2;
    [ContextMenu("playaudio")]
    public void PlaySound()
    {
        _random = Random.Range(0, 6);
        _random2 = Random.Range(0, 4);
        _audioSource.PlayOneShot(_audioClap[_random2]);
        _audioSource.PlayOneShot(_audioClips[_random], 0.4f);
    }
}
