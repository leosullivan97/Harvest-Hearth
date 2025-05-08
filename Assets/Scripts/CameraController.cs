using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform clampMin, clampMax;

    private Transform target;
    private Camera cam;
    private float halfWidth, halfHeight;

    // Initializes camera bounds and target tracking
    private void Start()
    {
        target = PlayerController.instance.transform;

        clampMin.SetParent(null);
        clampMax.SetParent(null);

        cam = GetComponent<Camera>();
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    // Follows the player and clamps camera position within defined boundaries
    private void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight);

        transform.position = clampedPosition;
    }
}
