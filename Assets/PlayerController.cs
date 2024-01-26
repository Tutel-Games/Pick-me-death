using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _leftAttackSphere;
    [SerializeField] private GameObject _rightAttackSphere;
    [SerializeField] private InputReader _inputs;
    [SerializeField] private SpriteRenderer _sr;

    private GameObject _currentActiveObj;

    private void Start()
    {
        _currentActiveObj = _leftAttackSphere;
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
    }

    void SetActiveObj(GameObject newObj)
    {
        _currentActiveObj.SetActive(false);
        _currentActiveObj = newObj;
        _currentActiveObj.SetActive(true);
    }
}
