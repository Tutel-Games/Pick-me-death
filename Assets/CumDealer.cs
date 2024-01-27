using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CumDealer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetDamage();
        }
    }
}
