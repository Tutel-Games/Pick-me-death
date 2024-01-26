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
    
    private GameObject _currentActiveObj;
    private List<Sprite> _normalAttacks = new();
    private float _timer;
    private void Start()
    {
        _currentActiveObj = _leftAttackSphere;
        _normalAttacks.Add(_normalAttack1);
        _normalAttacks.Add(_normalAttack2);
    }

    void Update()
    {
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

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            SetAllObjUnactive();
            _sr.sprite = _idleSprite;
        }
    }

    void SetActiveObj(GameObject newObj)
    {
        _timer = 0.1f;
        _currentActiveObj.SetActive(false);
        _currentActiveObj = newObj;
        _currentActiveObj.SetActive(true);
        _sr.sprite = _normalAttack1;
    }

    void SetAllObjUnactive()
    {
        
    }
}
