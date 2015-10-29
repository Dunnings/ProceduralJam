using UnityEngine;
using System.Collections;

//ensure that GameObject has SpriteRenderer for fog
public class FogOfWar : MonoBehaviour 
{
    private GameObject undisFogGO;
    private GameObject[,] blackFog;
    private GameObject[,] greyFog;
    private GameObject fogGO;

    private SpriteRenderer _undiscoveredFog;

    //Player Ref
    public GameObject player;  

    //Level width and height
    public int mapWidth, mapHeight;

    public int tileWidth, tileHeight;

    /// <summary>
    /// Set up components for depth fog
    /// </summary>
    void Awake()
    {
        blackFog = new GameObject[mapWidth, mapHeight];
        greyFog = new GameObject[mapWidth, mapHeight];
        //set up both GameObjects for Fog  
        undisFogGO = new GameObject ("Black Fog");
        CreateFog(Color.black, blackFog, undisFogGO);

        fogGO = new GameObject("Alpha Fog");
        CreateFog(new Color(0.0f,0.0f,0.0f,0.5f), greyFog, fogGO);
        fogGO.transform.parent = this.transform;

        undisFogGO.transform.parent = this.transform;
        fogGO.transform.parent = this.transform;    
    }

    /// <summary>
    /// Used to set up fog initial state
    /// </summary>
    /// <param name="fogCol"></param>
    /// <param name="fogSpriteRenderer"></param>
    void CreateFog(Color fogCol, GameObject[,] fogTiles, GameObject parent)
    {
        //temp holder for fog text/sprite
        Texture2D fog = new Texture2D(tileWidth, tileHeight);
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapWidth; y++)
            {
                fog.SetPixel(x, y, fogCol);
            }
        }
        fog.Apply();

        Sprite _fogSprite = Sprite.Create(fog, new Rect(0.0f, 0.0f, fog.width, fog.height), new Vector2(0.0f, 0.0f));

        //loop over texture width and height
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                fogTiles[x, y] = new GameObject("fog-" + x + "-" + y);
                fogTiles[x, y].transform.parent = parent.transform;
                fogTiles[x, y].transform.position = new Vector3(x * (tileWidth / 100.0f), y * (tileHeight / 100.0f), 0.0f);
                SpriteRenderer f = fogTiles[x, y].AddComponent<SpriteRenderer>();
                f.sprite = _fogSprite;
            }
        }
    }

    void Update()
    {
        if (blackFog[(int)player.transform.position.x, (int)player.transform.position.y].activeSelf == true)
        {
            blackFog[(int)player.transform.position.x, (int)player.transform.position.y].SetActive(false);
        }
    }
  
}
