using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    [field: SerializeField] public PlayerController PlayerController { get; private set; }
}
