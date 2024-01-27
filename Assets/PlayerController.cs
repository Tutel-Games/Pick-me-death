using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _leftAttackSphere;
    [SerializeField] private GameObject _rightAttackSphere;
    [SerializeField] private InputReader _inputs;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Animator _anim;
    [SerializeField] private float _jumpForce = 1f;
    [SerializeField] private ParticleSystem _particlesJump;
    private GameObject _currentActiveObj;
    private float _timer;
    private Random _random = new ();
    private bool _isGrounded;
    private Rigidbody2D _rb;
    public LayerMask GroundLayer;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentActiveObj = _leftAttackSphere;
    }

    void Update()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, GroundLayer);
        
        if (_inputs.A)
        {
            _sr.flipX = true;
            SetActiveObj(_leftAttackSphere);
        }
        if (_inputs.D)
        {
            _sr.flipX = false;
            SetActiveObj(_rightAttackSphere);
        }

        if (_inputs.W && _isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
            _anim.Play("Jump");
            _particlesJump.Play();
        }

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _currentActiveObj.SetActive(false);
            if (_isGrounded)
            {
                _anim.Play("Idle");
            }
            else
            {
                _anim.Play("Jump");
            }
        }
    }

    void SetActiveObj(GameObject newObj)
    {
        _timer = 0.2f;
        _currentActiveObj.SetActive(false);
        _currentActiveObj = newObj;
        _currentActiveObj.SetActive(true);
        if (_random.Next(2) == 0)
        {
            _anim.Play("Punch");
        }
        else
        {
            _anim.Play("Kick");
        }
    }



}
