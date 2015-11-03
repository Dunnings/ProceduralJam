using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour 
{
    //the amount of enemies to object pool
    public int SpawnerCapactity;
    //maximum amount to spawn
    public int SpawnAmount;
    //Time delay to spawn enemy
    public float SpawnTime;
    //enemy type for spawner
    public GameObject m_Enemy;

    public GameObject Player;

    private int SpawnStack;
    //counter for amount spawned
    private int AmountSpawned;
    //counter for spawning
    private float SpawnCounter;
    //list to store object pooled enemies
    private List<GameObject> m_Enemies = new List<GameObject>();
    //list of spawned enemies with spawn id
    private List<KeyValuePair<int, GameObject>> m_SpawnedEnemies = new List<KeyValuePair<int, GameObject>>(); 

    /// <summary>
    /// On Awake pool all enemies
    /// </summary>
    void Awake()
    {
        SpawnStack = SpawnerCapactity - 1;
        //set up SpawnnCounter 
        SpawnCounter = SpawnTime;
        //set up initial Spawned amount 
        AmountSpawned = 0;

        //loop over amount of enemies to spawn
        for (int i = 0; i < SpawnerCapactity; i++)
        {
            //scene name with iterator
            string eName = "Enemy - " + i;
            //create object and set initial state to inactive
            GameObject enemy = GameObject.Instantiate(m_Enemy, new Vector2(0.0f,0.0f), Quaternion.identity)as GameObject;
            enemy.name = eName;

            enemy.GetComponent<Enemy>().m_Spawner = this;
            enemy.transform.position = this.transform.position;
            enemy.transform.parent = this.transform;

            enemy.SetActive(false);

            //add to spawn pool
            m_Enemies.Add(enemy);
        }
    }

    /// <summary>
    /// Called every frame
    /// </summary>
    void Update()
    {
        //if count down has hit or surpassed 0 and Amount currently 
        //spawned is not => the SpawnAmount
        if(SpawnCounter <= 0.0f && AmountSpawned < SpawnAmount)
        {
            if (!m_Enemies[SpawnStack].activeSelf)
            {
                m_Enemies[SpawnStack].transform.position = this.transform.position;
                m_Enemies[SpawnStack].SetActive(true);

                m_Enemies[SpawnStack].GetComponent<Enemy>().ID = SpawnStack;

                KeyValuePair<int, GameObject> newEnemy = new KeyValuePair<int, GameObject>(SpawnStack, m_Enemies[SpawnStack]);

                m_SpawnedEnemies.Add(newEnemy);

                SpawnStack--;

                //increment amount of enemies spawned from this spawner
                AmountSpawned++;

                //reset counter 
                SpawnCounter = SpawnTime;
            }
        }
        else
        {
            //count down timer
            SpawnCounter -= Time.deltaTime; 
        }

        if(SpawnStack < 0)
        {
            SpawnStack = SpawnerCapactity - 1;
        }

    }

    /// <summary>
    /// Will be called from an enemy inside m_SpawnedEnemies
    /// the enemy needs to pass the function it's spawn ID 
    /// and this enemy can then be turned off and removed 
    /// from the list of spawned enemies
    /// </summary>
    /// <param name="id"></param>
    public void EnemyDestroy(int id)
    {
        int i = 0;
        foreach (KeyValuePair<int, GameObject> enemy in m_SpawnedEnemies)
        {
            //if the current spawned enemies ID matches enemy
            if(enemy.Key == id)
            {
                //turn off enemy
                m_SpawnedEnemies[i].Value.SetActive(false);
                //remove that enemy from spawned
                m_SpawnedEnemies.RemoveAt(i);


                //decrement Amount of spawned enemies
                AmountSpawned--;
                break;
            }
            else
            {
                //increment counter 
                i++;
            }
        }
    }
}
