using UnityEngine;
using System.Collections;

//ensure that GameObject has SpriteRenderer for fog
public class FogOfWar : MonoBehaviour 
{
    private GameObject _blackFog;
    private GameObject _alphaFog;

    private GameObject[,] blackFog;
    private GameObject[,] alphaFog;

    private float pastPosX, pastPosY;

    //Player Ref
    public GameObject player;  

    //Level width and height
    public int mapWidth, mapHeight,
        tileWidth, tileHeight, sightRadius, sightBuffer;

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
        //SpriteRenderer bF = _blackFog.AddComponent<SpriteRenderer>();
        //SpriteRenderer af = _alphaFog.AddComponent<SpriteRenderer>();        

        CreateFog(Color.black, blackFog, _blackFog);        
        CreateFog(new Color(0.0f, 0.0f, 0.0f, 0.5f), alphaFog, _alphaFog);

        _alphaFog.transform.parent = this.transform;
        _blackFog.transform.parent = this.transform;
    }

    /// <summary>
    /// Use me in the map gen, make sure you comment out Awake
    /// This sets up the fog. Make sure that the tile width and height 
    /// is not a decimal i.e 32 instead of 0.32. CreateFog and the Update
    /// handle this
    /// </summary>
    /// <param name="_mapWidth"></param>
    /// <param name="_mapHeight"></param>
    /// <param name="_tileWidth"></param>
    /// <param name="_tileHeight"></param>
    public void InitFog(int _mapWidth, int _mapHeight, int _tileWidth, int _tileHeight)
    {
        //set up vars
        mapWidth = _mapWidth;
        mapHeight = _mapHeight;
        tileHeight = _tileHeight;
        tileWidth = _tileWidth;
        
        //Create new GameObjects
        blackFog = new GameObject[mapWidth, mapHeight];
        alphaFog = new GameObject[mapWidth, mapHeight];
        //set up both GameObjects for Fog  
        _blackFog = new GameObject("Black Fog");
        _alphaFog = new GameObject("Alpha Fog");
        //SpriteRenderer bF = _blackFog.AddComponent<SpriteRenderer>();
        //SpriteRenderer af = _alphaFog.AddComponent<SpriteRenderer>();        

        //set up fog sprites
        CreateFog(Color.black, blackFog, _blackFog);
        CreateFog(new Color(0.0f, 0.0f, 0.0f, 0.75f), alphaFog, _alphaFog);

        //parent to this GO
        _alphaFog.transform.parent = this.transform;
        _blackFog.transform.parent = this.transform;
    }

    /// <summary>
    /// Used to set up fog initial state
    /// </summary>
    /// <param name="fogCol"></param>
    /// <param name="fogSpriteRenderer"></param>
    private void CreateFog(Color fogCol, GameObject[,] fogTiles, GameObject parent)
    {
        //temp holder for fog text/sprite
        Texture2D fog = new Texture2D(tileWidth, tileHeight);
        for (int x = 0; x < tileWidth; x++)
        {
            for (int y = 0; y < tileHeight; y++)
            {
                fog.SetPixel(x, y, fogCol);
            }
        }
        fog.Apply();

        Sprite _fogSprite = Sprite.Create(fog, new Rect(0.0f, 0.0f, fog.width, fog.height), new Vector2(0.5f, 0.5f));
       

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
                f.sortingOrder = 32767;
            }
        }

        float posX = player.transform.position.x * 100.0f / tileWidth;
        float posY = player.transform.position.y * 100.0f / tileHeight;

        ClearFog((int)posX, (int)posY, fogTiles, false);
    }
    
    /// <summary>
    /// Called every frame, checks player pos and updates the fog
    /// </summary>
    private void Update()
    {

        //get array pos from player pos
        float posX = player.transform.position.x * 100.0f / tileWidth;
        float posY = player.transform.position.y * 100.0f / tileHeight;

        //if the player has moved into the sight buffer update FoW
        if (posX >= pastPosX + sightBuffer || posX <= pastPosX - sightBuffer ||
            posY >= pastPosY + sightBuffer || posY <= pastPosY - sightBuffer)
        {
            
            ClearFog((int)posX, (int)posY, blackFog, false);
            ClearFog((int)pastPosX, (int)pastPosY, alphaFog, true);
            ClearFog((int)posX, (int)posY, alphaFog, false);            

            //reset pastPosition
            pastPosX = posX;
            pastPosY = posY;
        }        
    }

    /// <summary>
    /// Using the x and y pos and the fogTiles passed
    /// a circle is created around the point of x and y
    /// and the fogTiles at the points of the circle created 
    /// are set to the visibilty defined by the visibility parameter
    /// </summary>
    /// <param name="_x"></param>
    /// <param name="_y"></param>
    /// <param name="fogTiles"></param>
    /// <param name="visibility"></param>
    private void ClearFog(int _x, int _y, GameObject[,] fogTiles, bool visibility)
    {
        //iterate down the line of sight
        for (int rad = sightRadius; rad >= 0; rad--)
        {
            //create a circle with 360 points
            for (int pointNum = 0; pointNum <= 360; pointNum++)
            {
                float i = (pointNum * 1.0f) / 360;

                //Get angle for current step
                var angle = i * Mathf.PI * 2;
                // the X &amp; Y position for this angle are calculated using Sin &amp; Cos
                float x = Mathf.Sin(angle) * rad;
                float y = Mathf.Cos(angle) * rad;
                Vector3 pos = new Vector3(x, y, 0) + new Vector3(_x,_y,0.0f);

                //make sure X does not go out of range of map
                if (pos.x < 0.0f)
                {
                    pos.x = 0;
                }
                if (pos.x > mapWidth-1)
                {
                    pos.x = mapWidth - 1;
                }

                //make sure Y does not go out of range of map
                if (pos.y < 0.0f)
                {
                    pos.y = 0;
                }
                if (pos.y > mapHeight-1)
                {
                    pos.y = mapHeight - 1;
                }

                if (fogTiles[(int)pos.x, (int)pos.y].activeSelf != visibility)
                {
                    //set to be inactive
                    fogTiles[(int)pos.x, (int)pos.y].SetActive(visibility);
                }

            }

        }

        //// old creates a square
        //from the player point - the sight radius up to the player 
        ////point + the sight radius
        //for (int x = _x - sightRadius; x < _x + sightRadius; x++)
        //{
        //    for (int y = _y-sightRadius; y < _y + sightRadius; y++)
        //    {
        //        //make sure X does not go out of range of map
        //        if(x < 0.0f)
        //        {
        //            x = 0;
        //        }
        //        if(x > mapWidth)
        //        {
        //            x = mapWidth;
        //        }

        //        //make sure Y does not go out of range of map
        //        if (y < 0.0f)
        //        {
        //            y = 0;
        //        }
        //        if (y > mapHeight)
        //        {
        //            y = mapHeight;
        //        }

        //        float ang = 360;
        //        Vector3 pos;
        //        pos.x = x + sightRadius * Mathf.Sin(ang * Mathf.Deg2Rad);
        //        pos.y = y + sightRadius * Mathf.Cos(ang * Mathf.Deg2Rad);
            

        //        //if the targted tile is active
        //        if (fogTiles[(int)pos.x, (int)pos.y].activeSelf != visibility)
        //        {
        //            //set to be inactive
        //            fogTiles[x, y].SetActive(visibility);
        //        }
        //    }            
        //}
    }
 
  
}
