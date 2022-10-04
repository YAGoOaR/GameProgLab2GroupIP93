using UnityEngine;

//A script that makes camera follow player
public class Follow : MonoBehaviour
{
    [SerializeField] float moveStep = 0.5f;
    [SerializeField] float offset = 6f;
    [SerializeField] float sensitivity = 1f;

    //Camera Cam;

    Transform camTransform;
    Rigidbody rb;

    void Awake()
    {
        GameObject cameraObject = Camera.main.gameObject;
        camTransform = cameraObject.transform;
        camTransform.position = new Vector3(transform.position.x, transform.position.y, camTransform.position.z);

        //Cam = cameraObject.GetComponent<Camera>();

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 camRotation = camTransform.eulerAngles;

        float targetRotationX = camRotation.x + Input.GetAxis("Mouse Y") * -sensitivity;
        float closestBearingX = (targetRotationX + 540) % 360 - 180;
        float resultingRotationX = Mathf.Clamp(closestBearingX, -90, 90);

        Vector3 newCamRotation = new Vector3(
            resultingRotationX,
            camRotation.y + Input.GetAxis("Mouse X") * sensitivity, 
            0
        );
        camTransform.rotation = Quaternion.Euler(newCamRotation);
    }

    void LateUpdate()
    {
        Vector3 cameraOffsetDir = Vector3.Normalize(Vector3.back * 2 + Vector3.up);

        Vector3 offsetVec = Quaternion.Euler(0, camTransform.rotation.eulerAngles.y, 0) * cameraOffsetDir * offset;
        Vector3 targetPoint = transform.position + offsetVec;

        Vector3 delta = camTransform.position - targetPoint;
        float distance = delta.magnitude;

        Vector3 move = delta.normalized * moveStep * (1 + 3 * distance) * Time.deltaTime;
        if (move.magnitude > distance) move = delta;
        camTransform.position += rb.velocity * Time.deltaTime * 0.5f - move;
    }

}
