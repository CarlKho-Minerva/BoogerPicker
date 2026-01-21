using UnityEngine;

public class FingerController : MonoBehaviour
{
    public Transform fingerTip;
    public float moveSpeed = 2f;
    public float depthSpeed = 1f;
    public float curlSpeed = 5f;

    // BOUNDARIES: Adjust these numbers to fit your nose tunnel size
    public float minX = -1.0f;
    public float maxX = 1.0f;
    public float minY = -3.5f; // How far down (out of nose)
    public float maxY = -1.14f;  // How deep (into nose)

    void Start()
    {
        // 1. LOCK THE CURSOR
        // This hides the cursor and keeps it stuck in the center.
        // You can move the mouse infinitely without hitting the screen edge.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Unlock cursor if we press Escape (so you can close the game)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Movement Logic
        float moveX = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime;
        float moveZ = Input.mouseScrollDelta.y * depthSpeed * Time.deltaTime;

        // Apply movement
        transform.Translate(moveX, moveY, moveZ);

        // 2. CLAMP POSITION (Keep finger inside the nose)
        // We get the current position
        Vector3 currentPos = transform.position;

        // We force x and y to stay within our limits
        currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);
        currentPos.y = Mathf.Clamp(currentPos.y, minY, maxY);

        // We apply the fixed position back to the finger
        transform.position = currentPos;

        // Curling Logic (Same as before)
        if (Input.GetMouseButton(0))
        {
            fingerTip.localRotation = Quaternion.Slerp(fingerTip.localRotation, Quaternion.Euler(90, 0, 0), Time.deltaTime * curlSpeed);
        }
        else
        {
            fingerTip.localRotation = Quaternion.Slerp(fingerTip.localRotation, Quaternion.identity, Time.deltaTime * curlSpeed);
        }

        // Drill Logic (Same as before)
        if (Input.GetMouseButton(1))
        {
            float rotateAmount = Input.GetAxis("Mouse X") * 5f;
            transform.Rotate(0, 0, -rotateAmount);
        }
    }
}