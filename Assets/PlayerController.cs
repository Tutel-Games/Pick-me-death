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
    [SerializeField] private Sprite _idleSprite;
    [SerializeField] private Sprite _normalAttack1;
    [SerializeField] private Sprite _normalAttack2;
    [SerializeField] private float _jumpForce = 1f;
    
    private GameObject _currentActiveObj;
    private List<Sprite> _normalAttacks = new();
    private float _timer;
    private Random _random = new ();
    private bool _isGrounded;
    private Rigidbody2D _rb;
    public LayerMask GroundLayer;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentActiveObj = _leftAttackSphere;
        _normalAttacks.Add(_normalAttack1);
        _normalAttacks.Add(_normalAttack2);
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
        }

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _currentActiveObj.SetActive(false);
            _sr.sprite = _idleSprite;
        }
    }

    void SetActiveObj(GameObject newObj)
    {
        _timer = 0.2f;
        _currentActiveObj.SetActive(false);
        _currentActiveObj = newObj;
        _currentActiveObj.SetActive(true);
        _sr.sprite = _random.Next(2) == 0 ? _normalAttack1 : _normalAttack2;
    }
    
}
