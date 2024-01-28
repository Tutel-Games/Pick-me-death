using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int DamageValue = 10;
    public PlayerController PlayerController;
    public void DealDamage()
    {
        if (Vector2.Distance(transform.position, PlayerController.transform.position) <= 4f)
        {
            PlayerController.GetDamage(DamageValue);
        }
    }
}
