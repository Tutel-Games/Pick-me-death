using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundRandomizer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private AudioSource _audioSource;
    private int _random;
    [ContextMenu("playaudio")]
    public void PlaySound()
    {
        _random = Random.Range(0, 6);
        _audioSource.PlayOneShot(_audioClips[_random]);
    }
}
