using System;
using System.Collections;
using AllIn1VfxToolkit.Demo.Scripts;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> Death;
    public bool MoveRight;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _health = 2;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private bool _isDead;
    [Header("AFTER DEATH RAGDOLL")] 
    [SerializeField] private int _rageThreshold;
    [SerializeField] private float _defaultPunchPower;
    [SerializeField] private AnimationCurve _punchPower;
    [SerializeField] private float _defaultTorque;
    [SerializeField] private AnimationCurve _torquePower;
    [Header("PARTICLES")]
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private ParticleSystem _particleSystem2;
    [SerializeField] private WordRandomizer _randomizer;
    [SerializeField] private HitSoundRandomizer _soundRandomizer;

    private float _ms;
    private bool _isKnockedBack;
    private bool _hasReachedStopPosition;
    private Rigidbody2D _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _spriteRenderer.flipX = MoveRight;
        _ms = _movementSpeed;
    }

    private void FixedUpdate()
    {
        if (_isDead) return;
        if (_isKnockedBack) return;

        float direction = MoveRight ? 1 : -1;
        Vector2 move = new Vector2(transform.position.x + direction * (_ms * Time.fixedDeltaTime), transform.position.y);
        _rb.MovePosition(move);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StopPosition"))
        {
            _hasReachedStopPosition = true;
            _ms = 0;
            //TODO: ATTACK PLAYER
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("StopPosition"))
        {
            _hasReachedStopPosition = false;
            _ms = _movementSpeed;
        }
    }

    public void GetDamage(float knockbackMultiplier = 1)
    {
        _health--;
        StartCoroutine(PushBack(knockbackMultiplier));
        _animator.Play("Hit");
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
            _animator.Play("Death");
            EnableRagdoll();
        }
    }

    private IEnumerator PushBack(float multiplier)
    {
        if (_hasReachedStopPosition) yield return null;

        _isKnockedBack = true;
        float pushForceX = MoveRight ? -1 : 1;
        _rb.AddForce(new Vector2(pushForceX * 25 * multiplier, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        _isKnockedBack = false;
    }

    private void EnableRagdoll()
    {
        _rb.constraints = RigidbodyConstraints2D.None;
        float punchPower;
        float torquePower;
        int currentStreak = ComboCounter.Instance.CurrentStreak;
        if (currentStreak <= _rageThreshold)
        {
            punchPower = _defaultPunchPower;
            torquePower = _defaultTorque;
        }
        else
        {
            punchPower = _punchPower.Evaluate(currentStreak);
            torquePower = _torquePower.Evaluate(currentStreak);
        }

        torquePower *= MoveRight ? 1 : -1;
        GetComponent<Collider2D>().enabled = false;
        Vector2 punchForce = new Vector2((MoveRight ? -1 : 1) * punchPower / 2,  currentStreak <= _rageThreshold ? punchPower * 2 : punchPower);
        _rb.AddForce(punchForce, ForceMode2D.Impulse);
        _rb.AddTorque(torquePower, ForceMode2D.Impulse);
    }
}
