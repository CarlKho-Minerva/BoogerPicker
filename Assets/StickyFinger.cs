using UnityEngine;

public class StickyFinger : MonoBehaviour
{
    // Variable to remember which booger we are holding
    private Rigidbody heldBooger;

    void OnTriggerStay(Collider other)
    {
        // 1. If we touch a Booger AND hold Click AND we aren't holding anything yet
        if (other.gameObject.name == "Booger" && Input.GetMouseButton(0) && heldBooger == null)
        {
            // Connect the finger to the booger
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = other.GetComponent<Rigidbody>();

            // Remember what we are holding
            heldBooger = other.GetComponent<Rigidbody>();

            // Turn OFF gravity while holding (so it doesn't sag)
            heldBooger.useGravity = false;
        }
    }

    void Update()
    {
        // 2. If we let go of the mouse
        if (Input.GetMouseButtonUp(0))
        {
            // Destroy the joint (Drop it)
            Destroy(GetComponent<FixedJoint>());

            // If we were holding a booger, turn its gravity ON so it falls
            if (heldBooger != null)
            {
                heldBooger.useGravity = true;
                heldBooger = null; // Forget the booger
            }
        }
    }
}