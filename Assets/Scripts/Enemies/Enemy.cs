using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action Death;
    public bool MoveRight;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StopPosition"))
        {
            //TODO: CHANGE THIS TO ANIMATION ETC
            Destroy(gameObject);
        }
    }

    public void GetDamage()
    {
        _isDead = true;
        //TODO: ADD REST OF STUFF
        Death?.Invoke();
    }
}
