using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> Death;
    public bool MoveRight;
    [SerializeField] private int _health;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private bool _isDead;
    private Animator _animator;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_isDead) return;

        float direction = MoveRight ? 1 : -1;
        Vector2 move = new Vector2(transform.position.x + direction * (_movementSpeed * Time.fixedDeltaTime), transform.position.y);
        _rb.MovePosition(move);
    }

    public void GetDamage()
    {
        _health--;
        if (_health <= 0)
        {
            _isDead = true;
            GameManager.IncreasePoints(ComboCounter.Instance.ScoreMultiplier);
            Death?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
