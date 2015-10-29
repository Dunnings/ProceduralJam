using UnityEngine;
using System.Collections;

public class QuickWorld : MonoBehaviour
{
    public Sprite floor;
    public int mapWidth, mapHeight, tileWidth, tileHeight;

    void Start()
    {
        GameObject world = new GameObject("World");

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject tile = new GameObject("tile " + x + " " + y);

                tile.transform.position = new Vector3(x * (tileWidth / 100.0f), y * (tileHeight / 100.0f), 0.0f);

                tile.AddComponent<SpriteRenderer>();
                tile.GetComponent<SpriteRenderer>().sprite = floor;
                tile.transform.parent = this.transform;                  
                               
            }
        }

    }

}
