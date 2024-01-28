using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public event Action<Enemy> Death;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _health = 2;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private bool _isDead;
    [SerializeField] private ParticleSystem[] _particleSystem;
    [SerializeField] private InputReader _inputs;
    [SerializeField] private GameObject _gameObjectA;
    [SerializeField] private GameObject _gameObjectD;
    [SerializeField] private GameObject _gameObjectWW;
    [SerializeField] private GameObject _gameObjectWA;
    [SerializeField] private GameObject _gameObjectWD;
    [SerializeField] private GameObject _gameObjectWS;
    [SerializeField] private GameObject _gameObjectWS2;
    [SerializeField] private GameObject _Gra;
    [SerializeField] private GameObject _Gra2;
    [SerializeField] private PlayerController Player;
    [SerializeField] private AudioSource _audioSource;
    private int counter = 0;
    private GameObject _currentActiveObj;
    private float _timer;
    private bool _isSmashing;
    private bool _canDouble;
    private Rigidbody2D _rb;
    public LayerMask GroundLayer;
    

    private void Update()
    {
        //Player._isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, GroundLayer);
        if (counter == 5)
        {
            _Gra.SetActive(true);
            _Gra2.SetActive(true);
        }
        if (_inputs.A && Player._isGrounded)
        {
            _audioSource.Play();
            _particleSystem[0].Play();
            StartCoroutine(Pop(_gameObjectA));

            counter++;
        }
        if (_inputs.D && Player._isGrounded)
        {
            _audioSource.Play();
            _particleSystem[1].Play();
            StartCoroutine(Pop(_gameObjectD));

            counter++;
        }
        if (_inputs.A && !Player._isGrounded)
        {
            _audioSource.Play();
            _particleSystem[3].Play();
            StartCoroutine(Pop(_gameObjectWA));

            counter++;
        }
        if (_inputs.D && !Player._isGrounded)
        {
            _audioSource.Play();
            _particleSystem[4].Play();
            StartCoroutine(Pop(_gameObjectWD));

            counter++;
        }
        if (_inputs.W && !Player._isGrounded)
        {
            _audioSource.Play();
            _particleSystem[2].Play();
            StartCoroutine(Pop(_gameObjectWW));
            counter++;
        }
        if (_inputs.W)
        {
            if (_canDouble)
            {
                StartCoroutine(Pop(_gameObjectWW));
                counter++;
            }
        }

        if (_inputs.S && !Player._isGrounded && !_isSmashing)
        {
            _audioSource.Play();
            _particleSystem[5].Play();
            _particleSystem[6].Play();
            StartCoroutine(Pop(_gameObjectWS));
            StartCoroutine(Pop(_gameObjectWS2));
            counter++;
        }
    }
    IEnumerator Pop(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
