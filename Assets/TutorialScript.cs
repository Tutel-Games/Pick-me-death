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
    [SerializeField] private ParticleSystem _particleSystem2;
    [SerializeField] private InputReader _inputs;
    [SerializeField] private GameObject _gameObjectA;
    [SerializeField] private GameObject _gameObjectD;
    [SerializeField] private GameObject _gameObjectWW;
    [SerializeField] private GameObject _gameObjectWA;
    [SerializeField] private GameObject _gameObjectWD;
    [SerializeField] private GameObject _gameObjectWS;
    [SerializeField] private GameObject _gameObjectWS2;
    [SerializeField] private GameObject _Gra;
    [SerializeField] private PlayerController Player;
    private AudioSource _audioSource;
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
        }
        if (_inputs.A && Player._isGrounded)
        {
            _gameObjectA.SetActive(false);
            counter++;
        }
        if (_inputs.D && Player._isGrounded)
        {
            _gameObjectD.SetActive(false);
            counter++;
        }
        if (_inputs.A && !Player._isGrounded)
        {
            _gameObjectWA.SetActive(false);
            counter++;
        }
        if (_inputs.D && !Player._isGrounded)
        {
            _gameObjectWD.SetActive(false);
            counter++;
        }
        if (_inputs.W && !Player._isGrounded)
        {
            _gameObjectWW.SetActive(false);
            counter++;
        }
        if (_inputs.W)
        {
            if (_canDouble)
            {
                _gameObjectWW.SetActive(false);
                counter++;
            }
        }

        if (_inputs.S && !Player._isGrounded && !_isSmashing)
        {
            _gameObjectWS.SetActive(false);
            _gameObjectWS2.SetActive(false);
            counter++;
        }
    }
}
