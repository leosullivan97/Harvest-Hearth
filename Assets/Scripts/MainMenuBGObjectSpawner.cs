using UnityEngine;

public class MainMenuBGObjectSpawner : MonoBehaviour
{
    public Transform minPos, maxPos;
    public GameObject[] objects;
    public float timeBetweenSpawns;

    private float spawnCounter;

    // Spawns a background object at random intervals and positions within range
    private void Update()
    {
        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawns;

            GameObject newObject = Instantiate(objects[Random.Range(0, objects.Length)]);
            newObject.transform.position = new Vector3(
                Random.Range(minPos.position.x, maxPos.position.x),
                minPos.position.y,
                0f
            );

            newObject.SetActive(true);
        }
    }
}
