using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 5f;

    void Update()
    {
        // Rotate the object around the Z-axis
        transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
    }
}
