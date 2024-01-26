using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private bool _isDead;
    [SerializeField] private bool _moveRight;
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

        float direction = _moveRight ? 1 : -1;
        Vector2 move = new Vector2(transform.position.x + direction * (_movementSpeed * Time.fixedDeltaTime), transform.position.y);
        _rb.MovePosition(move);
    }

    public void GetDamage()
    {
        _isDead = true;
        //TODO: ADD REST OF STUFF
    }
}
