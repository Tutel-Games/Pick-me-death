using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CumDealer : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetDamage(_knockbackForce);
        }
    }
}
