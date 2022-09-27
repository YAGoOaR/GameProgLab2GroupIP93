using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundContactChecker : MonoBehaviour
{
    public bool Collided { get => collided || (Time.time - contactTime) < bonusTime; }

    [SerializeField] float bonusTime = 0.1f;

    float contactTime;

    bool collided = false;
    bool lastFrameCollided = false;

    void OnCollisionStay(Collision collision)
    {
        lastFrameCollided = true;
        contactTime = Time.time;
    }

    void FixedUpdate()
    {
        collided = lastFrameCollided;
        lastFrameCollided = false;
    }
}
