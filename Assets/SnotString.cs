using UnityEngine;

public class SnotString : MonoBehaviour
{
    private LineRenderer line;
    private SpringJoint joint;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        joint = GetComponent<SpringJoint>();
        line.startWidth = 0.1f;
        line.endWidth = 0.05f;
    }

    void Update()
    {
        // Only draw the line if the joint still exists (Hasn't popped)
        if (joint != null)
        {
            line.SetPosition(0, transform.position); // Start at Booger

            // End at the wall anchor point (We convert the local anchor to world space)
            Vector3 wallAnchor = joint.connectedBody.transform.TransformPoint(joint.connectedAnchor);
            line.SetPosition(1, wallAnchor);
        }
        else
        {
            // If joint is broken (popped), hide the line
            line.enabled = false;
        }
    }
}