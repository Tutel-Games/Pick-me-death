using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _leftAttackSphere;
    [SerializeField] private GameObject _rightAttackSphere;
    [SerializeField] private GameObject _jumpAttackSphere;
    [SerializeField] private GameObject _smashAttackSphere;
    [SerializeField] private GameObject _rageAttackSphere;
    [SerializeField] private InputReader _inputs;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Animator _anim;
    [SerializeField] private float _jumpForce = 1f;
    [SerializeField] private ParticleSystem _particlesJump;
    [SerializeField] private ParticleSystem _particlesSmash;
    [SerializeField] private ParticleSystem _particlesRage;
    [SerializeField] private AudioSource _audioRage;
    [SerializeField] private AudioSource _audioSmash;
    private GameObject _currentActiveObj;
    private float _timer;
    private Random _random = new ();
    public bool _isGrounded;
    private bool _isSmashing;
    private bool _canDouble;
    private bool _isRaged;
    private Rigidbody2D _rb;
    public LayerMask GroundLayer;
    [SerializeField] private RectTransform _hpSlider;
    [SerializeField] private GameObject _deathScreen;
    
    public int Hp = 100;
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
        if (!_isRaged)
        {
            if (_inputs.A && _timer <=0)
            {
                _sr.flipX = true;
                SetActiveObj(_leftAttackSphere);
            }
            if (_inputs.D && _timer <=0)
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
                    _anim.Play("Kolowrotek");
                    _timer = 0.2f;
                    _currentActiveObj.SetActive(false);
                    _currentActiveObj = _jumpAttackSphere;
                    _currentActiveObj.SetActive(true);
                }
            
            }
        
            if (_inputs.S && !_isGrounded && !_isSmashing)
            {
                _rb.AddForce(Vector3.down * (_jumpForce * 1.25f), ForceMode2D.Impulse);
                _rb.sharedMaterial = bounce;
                _isSmashing = true;
                _audioSmash.Play();
                _anim.Play("Smash");
            }
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
            _particlesSmash.Play();
            _timer = 0.2f;
            _currentActiveObj.SetActive(false);
            _currentActiveObj = _smashAttackSphere;
            _currentActiveObj.SetActive(true);
            Invoke(nameof(ResetSmash), .2f);
            Shakeomat.Instance.AverageShakerShakerPartyMaker();
        }

        if (_isGrounded)
        {
            _canDouble = true;
        }
    }
    private void ResetSmash()
    {
        _rb.sharedMaterial = nobounce;
        _isSmashing = false;
    }

    private int animIndex;
    void SetActiveObj(GameObject newObj)
    {
        _timer = 0.2f;
        _currentActiveObj.SetActive(false);
        _currentActiveObj = newObj;
        _currentActiveObj.SetActive(true);
        if (animIndex == 0)
        {
            _anim.Play("Punch");
            animIndex++;
        }
        else if (animIndex == 1)
        {
            _anim.Play("Kick");
            animIndex++;
        }
        else
        {
            _anim.Play("Push");
            animIndex = 0;
        }
    }
    
    public void GetDamage(int value)
    {
        ComboCounter.Instance.ResetStreak();
        Hp -= value;
        float magicNumber = 584.391f;
        _hpSlider.sizeDelta = new Vector2(magicNumber * (Hp/100f), _hpSlider.sizeDelta.y);
        if (Hp <= 0)
        {
            _deathScreen.SetActive(true);
            Destroy(gameObject);
        }
    }

    public void Rage()
    {
        _isRaged = true;
        _timer = 1f;
        _anim.Play("Rage");
        _particlesRage.Play();
        _audioRage.Play();
        _currentActiveObj.SetActive(false);
        _currentActiveObj = _rageAttackSphere;
        _currentActiveObj.SetActive(true);
        Invoke(nameof(ResetRage), 1);
    }

    public void ResetRage()
    {
        _isRaged = false;
    }
}
