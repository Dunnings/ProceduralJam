using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class OxygenTank : MonoBehaviour 
{
	public float Oxygen;

	/// <summary>
	/// Called in the update to move the enemy 
	/// if the player is in a certain radius of the 
	/// player
	/// </summary>
	
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Player")
		{
			//m_Spawner.EnemyDestroy(ID);
			
			OxygenBar.Instance.m_oxygenPercent += Oxygen;
			
			gameObject.SetActive(false);

			
			//AudioManager.GetInstance().PlaySingle();
			
			//GameCamera.Instance.ShakeCamera();
		}
	}
	
}
