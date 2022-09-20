using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    float rotateSpeed = 3f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed);
    }
}
