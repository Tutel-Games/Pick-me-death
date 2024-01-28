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
    [SerializeField] private ParticleSystem _particlesSmash;
    private GameObject _currentActiveObj;
    private float _timer;
    private Random _random = new ();
    private bool _isGrounded;
    private bool _isSmashing;
    private bool _canDouble;
    private Rigidbody2D _rb;
    public LayerMask GroundLayer;

    private PhysicsMaterial2D bounce, nobounce;
    private void Start()
    {
        bounce = new PhysicsMaterial2D();
        bounce.bounciness = 0.5f;
        nobounce = new PhysicsMaterial2D();
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
        if (_inputs.W)
        {
            if (_isGrounded)
            {
                _rb.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
                _anim.Play("Jump");
                _particlesJump.Play();
            }
            else if (_canDouble)
            {
                _canDouble = false;
                print("x");
                _anim.Play("Kolowrotek");
            }
            
        }
        
        if (_inputs.S && !_isGrounded && !_isSmashing)
        {
            _rb.AddForce(Vector3.down * (_jumpForce * 1.25f), ForceMode2D.Impulse);
            _rb.sharedMaterial = bounce;
            _isSmashing = true;
            _anim.Play("Smash");
        }

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _currentActiveObj.SetActive(false);
            if (_isGrounded && !_isSmashing)
            {
                _anim.Play("Idle");
            }
            else if(!_isSmashing && _canDouble)
            {
                _anim.Play("Jump");
            }
        }

        if (_isGrounded && _isSmashing)
        {
            Invoke(nameof(ResetParticleSmash), .02f);
            Invoke(nameof(ResetSmash), .2f);
        }

        if (_isGrounded)
        {
            _canDouble = true;
        }
    }

    private void ResetParticleSmash()
    {
        _particlesSmash.Play();
    }
    private void ResetSmash()
    {
        _rb.sharedMaterial = nobounce;
        _isSmashing = false;
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
