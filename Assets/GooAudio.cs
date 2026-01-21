using UnityEngine;

public class GooAudio : MonoBehaviour
{
    public AudioSource slimeSound;
    public AudioClip[] slimeClips; // Array to hold your 2 files

    public float maxPitch = 1.5f;
    public float minPitch = 0.8f;

    private Vector3 lastPosition;
    private bool isMoving = false;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        // 1. Calculate Speed
        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;

        // 2. Are we moving?
        if (speed > 0.1f)
        {
            // If we weren't moving before, this is a "New Move"
            if (!isMoving)
            {
                PlayRandomSquish();
                isMoving = true;
            }

            // Adjust Volume and Pitch based on speed
            slimeSound.volume = Mathf.Clamp01(speed / 2f);
            slimeSound.pitch = Mathf.Lerp(minPitch, maxPitch, speed / 5f);
        }
        else
        {
            // We stopped moving
            if (isMoving)
            {
                slimeSound.Stop(); // Stop the audio so we can swap it next time
                isMoving = false;
            }
        }
    }

    void PlayRandomSquish()
    {
        // Safety check to make sure you added clips
        if (slimeClips.Length == 0) return;

        // 1. Pick a random clip from your list
        int randomClipIndex = Random.Range(0, slimeClips.Length);
        slimeSound.clip = slimeClips[randomClipIndex];

        // 2. Pick a random start time (Juice factor!)
        // This ensures you don't always hear the first 0.5 seconds of the file.
        // We subtract 1 second so we don't start exactly at the end.
        if (slimeSound.clip.length > 1f)
        {
            slimeSound.time = Random.Range(0f, slimeSound.clip.length - 1f);
        }

        slimeSound.Play();
    }
}