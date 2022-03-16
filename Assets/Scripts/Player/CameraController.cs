using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public bool isTargeting;

    [Header("Debug")]
    [Range(0f, 1f)] public float smoothSpeed = 0.125f;
    public Vector2 offset;

    private Vector3 _velocity = Vector3.zero;


    private void Start()
    {
        target = PlayerController.instance.transform;
        transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, -10);
    }

    private void LateUpdate()
    {
        if (!isTargeting || target == null) return;

        Vector3 desiredPosition = new(target.position.x + offset.x, target.position.y + offset.y, -10);
        Vector3 positionSmoothed = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, smoothSpeed);

        transform.position = positionSmoothed;
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}

