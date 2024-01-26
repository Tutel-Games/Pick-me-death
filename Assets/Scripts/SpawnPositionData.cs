using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnPositionData : MonoBehaviour
{
    public Vector2 Position;
    public bool MoveRight;

    private void OnValidate()
    {
        Position = transform.position;
    }
}
