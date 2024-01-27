using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> Death;
    public bool MoveRight;
    [SerializeField] private int _health = 2;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private bool _isDead;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private ParticleSystem _particleSystem2;
    [SerializeField] private WordRandomizer _randomizer;
    [SerializeField] private HitSoundRandomizer _soundRandomizer;
    private Animator _animator;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.flipX = MoveRight;
    }

    private void FixedUpdate()
    {
        if (_isDead) return;

        float direction = MoveRight ? 1 : -1;
        Vector2 move = new Vector2(transform.position.x + direction * (_movementSpeed * Time.fixedDeltaTime), transform.position.y);
        _rb.MovePosition(move);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StopPosition"))
        {
            _movementSpeed = 0;
            //TODO: ATTACK PLAYER
        }
    }

    public void GetDamage()
    {
        _health--;
        ComboCounter.Instance.IncreaseStreak();
        if (MoveRight)
        {
            _particleSystem2.Play();
            _randomizer.PlayParticle2();
        }
        else
        {
            _particleSystem.Play();
            _randomizer.PlayParticle();

        }
        _soundRandomizer.PlaySound();
        if (_health <= 0)
        {
            _isDead = true;
            GameManager.IncreasePoints(ComboCounter.Instance.ScoreMultiplier);
            Death?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
