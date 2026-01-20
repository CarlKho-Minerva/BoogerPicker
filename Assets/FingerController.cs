using UnityEngine;

public class FingerController : MonoBehaviour
{
    public Transform fingerTip; // Assign in Inspector
    public float moveSpeed = 10f;
    public float depthSpeed = 5f;
    public float curlSpeed = 5f;

    void Update()
    {
        // 1. Movement (X and Y follow mouse, Z uses Scroll Wheel)
        float moveX = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime;
        float moveZ = Input.mouseScrollDelta.y * depthSpeed * Time.deltaTime;

        // Apply movement
        transform.Translate(moveX, moveY, moveZ);

        // 2. Curling (Left Click)
        if (Input.GetMouseButton(0))
        {
            // Rotate the tip forward to "Curl"
            fingerTip.localRotation = Quaternion.Slerp(fingerTip.localRotation, Quaternion.Euler(90, 0, 0), Time.deltaTime * curlSpeed);
        }
        else
        {
            // Return to straight
            fingerTip.localRotation = Quaternion.Slerp(fingerTip.localRotation, Quaternion.identity, Time.deltaTime * curlSpeed);
        }

        // 3. Drilling (Right Click + Mouse movement rotates the whole hand)
        if (Input.GetMouseButton(1))
        {
            float rotateAmount = Input.GetAxis("Mouse X") * 5f;
            transform.Rotate(0, 0, -rotateAmount);
        }
    }
}