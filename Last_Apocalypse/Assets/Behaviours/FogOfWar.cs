using UnityEngine;
using System.Collections;

//ensure that GameObject has SpriteRenderer for fog
public class FogOfWar : MonoBehaviour 
{
    private GameObject _blackFog;
    private GameObject _alphaFog;

    private GameObject[,] blackFog;
    private GameObject[,] alphaFog;
    


    //Player Ref
    public GameObject player;  

    //Level width and height
    public int mapWidth, mapHeight,
        tileWidth, tileHeight, sightRadius;

    /// <summary>
    /// Set up components for depth fog
    /// </summary>
    void Awake()
    {
        blackFog = new GameObject[mapWidth, mapHeight];
        alphaFog = new GameObject[mapWidth, mapHeight];
        //set up both GameObjects for Fog  
        _blackFog = new GameObject ("Black Fog");
        _alphaFog = new GameObject("Alpha Fog");
        _blackFog.AddComponent<SpriteRenderer>();
        _alphaFog.AddComponent<SpriteRenderer>();

        CreateFog(Color.black, blackFog, _blackFog);        
        CreateFog(new Color(0.0f, 0.0f, 0.0f, 0.5f), alphaFog, _alphaFog);

        _alphaFog.transform.parent = this.transform;
        _blackFog.transform.parent = this.transform;
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

        //get array pos from player pos
        float posX = player.transform.position.x * 100.0f / tileWidth;
        float posY = player.transform.position.y * 100.0f / tileHeight;

        //if (posX % sightRadius == 0 ||
        //    posY % sightRadius == 0)
        //{
            ClearFog((int)posX, (int)posY, blackFog);
        //}

        ////set current player pos fog tiles active false
        //if (blackFog[(int)posX, (int)posY].activeSelf == true)
        //{
        //    blackFog[(int)posX, (int)posY].SetActive(false);
        //}
       
    }

    void ClearFog(int _x, int _y, GameObject[,] fogTiles)
    {
        // from the player point - the sight radius up to the player 
        //point + the sight radius
        for (int x = _x - sightRadius; x < _x + sightRadius; x++)
        {
            for (int y = _y-sightRadius; y < _y + sightRadius; y++)
            {
                //make sure X does not go out of range of map
                if(x < 0.0f)
                {
                    x = 0;
                }
                if(x > mapWidth)
                {
                    x = mapWidth;
                }

                //make sure Y does not go out of range of map
                if (y < 0.0f)
                {
                    y = 0;
                }
                if (y > mapHeight)
                {
                    y = mapHeight;
                }

                //if the targted tile is active
                if (fogTiles[x, y].activeSelf)
                {
                    //set to be inactive
                    fogTiles[x, y].SetActive(false);
                }
            }            
        }
    }
  
}
