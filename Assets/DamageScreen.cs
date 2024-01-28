using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScreen : MonoBehaviour
{
    [SerializeField] private GameObject _screen;
    
    public void GetDamage()
    {
        _screen.SetActive(true);
        Invoke(nameof(xxx) , 0.2f);
    }

    private void xxx()
    {
        _screen.SetActive(false);
    }
}
