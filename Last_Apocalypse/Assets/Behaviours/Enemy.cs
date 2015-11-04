using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class Enemy : MonoBehaviour 
{
    public int ID;
    public float Damage;
    public float speed, sightRadius;
    //reference to my spawner
    public EnemySpawner m_Spawner;
    public AudioClip enemySound;

    private float health = 5.0f;
    private bool chase = false;

    void FixedUpdate()
    {
        if(Vector3.Distance(m_Spawner.Player.transform.position, this.transform.position) <= 5.0f)
        {        
            MoveEnemy();

            //set chase to true
            chase = true;
        }
        else
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f,0.0f,0.0f);
        }

        //if I am chasing start decreasing my life
        if(chase)
        {
            //once health has gone
            if(health <= 0.0f)
            {
                //destroy enemy
                m_Spawner.EnemyDestroy(ID);

                //reset vars for next timed spawned
                health = 10.0f;
                chase = false;
            }
            else
            {
                //decrement health
                health -= Time.deltaTime;
            }
        }


    }

    /// <summary>
    /// Called in the update to move the enemy 
    /// if the player is in a certain radius of the 
    /// player
    /// </summary>
    void MoveEnemy()
    {
        Vector3 vel = new Vector3(0.0f,0.0f,0.0f);
        //if enemy is less than the player pos increment up on 
        if (this.GetComponent<Rigidbody>().position.x <= m_Spawner.Player.transform.position.x)
        {
            vel = new Vector3(1, vel.y, 0);            
        }
        else if (this.transform.position.x >= m_Spawner.Player.transform.position.x )
        {
            vel = new Vector3(-1, vel.y, 0);   
        }

        if (this.GetComponent<Rigidbody>().position.y <= m_Spawner.Player.transform.position.y )
        {
            vel = new Vector3(vel.x, 1, 0);   
        }
        else if (this.transform.position.y >= m_Spawner.Player.transform.position.y )
        {
            vel = new Vector3(vel.x, -1, 0);
        }

        this.GetComponent<Rigidbody>().velocity = vel * Random.Range(speed, speed * 1.5f);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            m_Spawner.EnemyDestroy(ID);

            OxygenBar.Instance.m_oxygenPercent -= Damage;

            AudioManager.GetInstance().PlaySingle(enemySound);

            GameCamera.Instance.ShakeCamera();
        }
    }

}
