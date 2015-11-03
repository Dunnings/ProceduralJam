using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class Enemy : MonoBehaviour 
{
    public int health, ID;
    public float Damage;
    public float speed, sightRadius;
    //reference to my spawner
    public EnemySpawner m_Spawner;

    void FixedUpdate()
    {
        if(Vector3.Distance(m_Spawner.Player.transform.position, this.transform.position) <= 5.0f)
        {        
            MoveEnemy();
        }
        else
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f,0.0f,0.0f);
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

            GameCamera.Instance.ShakeCamera();
        }
    }

    ///// <summary>
    ///// Called when collision occurs 
    ///// </summary>
    ///// <param name="col"></param>
    //void OnCollisionEnter(Collision2D col)
    //{
    //    if(col.gameObject.tag == "Player")
    //    {
    //        m_Spawner.EnemyDestroy(ID);
    //    }
    //}
}
