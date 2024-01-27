using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _Image;
    [SerializeField] private float _x, _y;

    void Update()
    {
        _Image.uvRect = new Rect(_Image.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _Image.uvRect.size);
    }
}

