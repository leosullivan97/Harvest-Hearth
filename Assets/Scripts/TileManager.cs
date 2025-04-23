using UnityEngine;
using UnityEditor.Tilemaps;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileManager : MonoBehaviour
{
    Tilemap ground;
    Tilemap groundOverlay;
    Tilemap hardObjects;
    Tilemap hardObjectsOverlay;

    Tile fence;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] Children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Children[i] = transform.GetChild(i).gameObject;
        }

        

        foreach (GameObject child in Children)
        {
            if (child.name == "Ground")
            {
                ground = child.GetComponent<Tilemap>();
            }
            else if (child.name == "Ground Overlay")
            {
                groundOverlay = child.GetComponent<Tilemap>();
            }
            else if (child.name == "Hard Objects")
            {
                hardObjects = child.GetComponent<Tilemap>();
            }
            else if (child.name == "Hard Objects Overlay")
            {
                hardObjectsOverlay = child.GetComponent<Tilemap>();
            }
        }

        if (ground == null || groundOverlay == null || hardObjects == null || hardObjectsOverlay == null)
        {
            Debug.LogError("One or more Tilemaps are missing. Please check the hierarchy.");
            return;
        }

        // Load the tile from the Resources folder
        fence = Resources.Load<Tile>("Tiles/Objects/");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

