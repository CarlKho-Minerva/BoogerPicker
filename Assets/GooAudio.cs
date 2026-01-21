using UnityEngine;

public class GooAudio : MonoBehaviour
{
    public AudioSource slimeSound;
    public float maxPitch = 1.5f; // High squeak
    public float minPitch = 0.6f; // Low rumble

    // We compare current position to last frame's position to find speed
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        // 1. Calculate Speed
        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;

        // 2. Control Volume (Silence if not moving)
        // If speed is 0, volume is 0. If speed is fast (over 2), volume is 1.
        slimeSound.volume = Mathf.Clamp01(speed / 2f);

        // 3. Control Pitch (Frequency)
        // Lerp means "Blend". We blend between Low Pitch and High Pitch based on speed.
        slimeSound.pitch = Mathf.Lerp(minPitch, maxPitch, speed / 5f);
    }
}