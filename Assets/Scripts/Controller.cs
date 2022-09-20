
using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float moveForce = 20f;
    [SerializeField] float moveForceWhileJumpingMultiplier = 0.5f;
    [SerializeField] float maxSpeed = 13f;
    [SerializeField] float flyMaxSpeed = 20f;
    [SerializeField] float accelerationLowerThreshold = 3f;
    [SerializeField] float jumpPower = 8f;
    [SerializeField] float imprControlDiscardThreshold = 0.1f;

    [SerializeField] float playerMaterialFriction = 4f;

    [SerializeField] Transform moveRelativeTo;

    bool jump = false;

    Rigidbody rb;
    Collider boxCollider;
    GroundContactChecker groundContactChecker;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        boxCollider = GetComponentInChildren<Collider>();
        groundContactChecker = GetComponentInChildren<GroundContactChecker>();
    }

    void Update()
    {
        ProcessPlayerPhysics();
    }

    Func<float, float, float, float> getMotorForce = (x, a, b) => x < a ? 1 : x > b ? 0 : .5f + .5f * Mathf.Cos((x - a) / (b - a) * Mathf.PI);

    void ProcessPlayerPhysics()
    {
        jump = Input.GetButtonDown("Jump") ? true : jump && !Input.GetButtonUp("Jump");

        bool isOnGround = groundContactChecker.Collided;

        boxCollider.material.dynamicFriction = isOnGround ? playerMaterialFriction : 0;

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 horizontalVelocity = Vector3.ProjectOnPlane(rb.velocity, Vector3.up);

        if (isOnGround && jump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jump = false;
        }

        Vector3 movementDir = Vector3.ProjectOnPlane(moveRelativeTo.TransformVector(movement), Vector3.up);

        Vector3 velDir = horizontalVelocity.magnitude > imprControlDiscardThreshold ? horizontalVelocity : transform.forward;
        Vector3 ort = Vector3.Project(movementDir, Vector3.Cross(velDir, Vector3.up));
        Vector3 tang = Vector3.Project(movementDir, velDir);

        float velAlongForce = Vector3.Dot(rb.velocity, tang.normalized);

        float resMaxSpeed = isOnGround ? maxSpeed : flyMaxSpeed;
        float movementCoef = getMotorForce(velAlongForce, resMaxSpeed - accelerationLowerThreshold, resMaxSpeed);

        float forceMultiplier = isOnGround ? moveForce : moveForce * moveForceWhileJumpingMultiplier;

        rb.AddForce((ort + tang * movementCoef) * forceMultiplier * Time.deltaTime, ForceMode.VelocityChange);
    }
}
