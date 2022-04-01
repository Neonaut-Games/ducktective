using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    public float angle;
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, angle, 0), Space.World);
    }
}
