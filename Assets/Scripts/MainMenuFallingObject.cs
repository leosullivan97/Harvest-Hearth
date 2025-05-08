using UnityEngine;

public class MainMenuFallingObject : MonoBehaviour
{
    public float minFallSpeed = 2f, maxFallSpeed = 5f;
    public float minRotSpeed = -360f, maxRotSpeed = 360f;
    public float destroyHeight = -6f;

    private float fallSpeed, rotSpeed;
    private float rotValue;

    // Sets a random falling speed and rotation speed for the object
    private void Start()
    {
        fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
        rotSpeed = Random.Range(minRotSpeed, maxRotSpeed);
    }

    // Moves and rotates the object downward; destroys it when below threshold
    private void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        rotValue += rotSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, rotValue);

        if (transform.position.y < destroyHeight)
        {
            Destroy(gameObject);
        }
    }
}
