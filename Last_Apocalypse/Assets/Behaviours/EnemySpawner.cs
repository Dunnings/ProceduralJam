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
    public GameObject Enemy;

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
        //set up SpawnnCounter 
        SpawnCounter = SpawnTime;
        //set up inital Spawned amount 
        AmountSpawned = 0;

        //loop over amount of enemies to spawn
        for (int i = 0; i < SpawnAmount; i++)
        {
            //scene name with iterator
            string eName = "Enemy - " + i;
            //create object and set initial state to inactive
            GameObject CurrentEnemy = new GameObject(eName);

            //give Enemy reference to Enemy spawner
            Enemy m_enemyScript = CurrentEnemy.AddComponent<Enemy>();
            m_enemyScript.m_Spawner = this;

            CurrentEnemy.transform.parent = this.transform;

            //add to spawn pool
            m_Enemies.Add(CurrentEnemy);
        }
    }

    /// <summary>
    /// Called every frame
    /// </summary>
    void Update()
    {
        //if count down has hit or surpassed 0 and Amount currently 
        //spawned is not => the SpawnAmount
        if(SpawnCounter <= 0.0f && AmountSpawned >= SpawnAmount)
        {   

            
            //reset counter 
            SpawnCounter = SpawnTime;
            //increment amount of enemies spawned from this spawner
            AmountSpawned++;
        }
        else
        {
            //count down timer
            SpawnCounter -= Time.deltaTime; 
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
            }
            else
            {
                //increment counter 
                i++;
            }
        }
    }
}
