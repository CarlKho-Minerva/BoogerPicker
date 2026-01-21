using UnityEngine;

public class StickyFinger : MonoBehaviour
{
    private Rigidbody heldBooger;
    private SpringJoint boogerSpring; // To control the bounciness
    private float defaultDamper = 0.2f; // Floppy mode
    private float holdingDamper = 10f;  // Gooey/Stable mode

    void OnTriggerStay(Collider other)
    {
        // 1. Grab Logic
        if (other.gameObject.name == "Booger" && Input.GetMouseButton(0) && heldBooger == null)
        {
            // Connect to Finger
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = other.GetComponent<Rigidbody>();

            heldBooger = other.GetComponent<Rigidbody>();
            boogerSpring = other.GetComponent<SpringJoint>();

            // Turn OFF gravity so it doesn't sag while holding
            heldBooger.useGravity = false;

            // Make it "Thick" so it doesn't jitter
            if (boogerSpring != null)
            {
                boogerSpring.damper = holdingDamper;
            }
        }
    }

    void Update()
    {
        // 2. Release Logic
        if (Input.GetMouseButtonUp(0))
        {
            // Destroy the finger connection
            Destroy(GetComponent<FixedJoint>());

            if (heldBooger != null)
            {
                // Turn gravity ON so it falls
                heldBooger.useGravity = true;

                // Make it "Loose" again so it snaps or falls naturally
                if (boogerSpring != null)
                {
                    boogerSpring.damper = defaultDamper;
                }

                // Forget the booger
                heldBooger = null;
                boogerSpring = null;
            }
        }
    }
}