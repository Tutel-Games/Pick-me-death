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
            print("DŻAKI CZAN SPIZGANE OCZY MAM");
            //other.TryGetComponent<Enemy>().GetDamage();
        }
    }
}
