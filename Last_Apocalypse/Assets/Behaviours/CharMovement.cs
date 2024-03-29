﻿using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour {

    public static CharMovement Instance;

    public Animator anim;
    public float speed = 0.0f;
    float distance;
    float maxDistance = 1.5f;
    float minDistance = 0.1f;
    public float maxSpeed = 0.5f;
    public float minSpeed = 2f;
	public bool isMouseOverUI = false, m_followMouse = false;

    public AudioClip footstep1;
    public AudioClip footstep2;
    float repeatTime = 0.35f;
    float timer = 0f;

    Collision2D col;

    // Use this for initialization
    void Awake()
    {
        Application.targetFrameRate = 60;
        col = new Collision2D();
        Instance = this;
    }
	
	// Update is called once per frame
	void Update ()
    {
        #region Mouse momvement
        // When mouse button is held down
        if (Input.GetMouseButtonDown(0) && !isMouseOverUI)
        {
            m_followMouse = true;
        }
        // When mouse button is held down
        if (Input.GetMouseButtonUp(0) || isMouseOverUI)
        {
            m_followMouse = false;
        }
        if (m_followMouse) {
            
            // Get mouse position on screen
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            distance = Vector3.Distance(gameObject.transform.position, mousePos);
            if (distance > 0.01f)
            {
                var pos = Camera.main.WorldToScreenPoint(transform.position);
                var dir = Input.mousePosition - pos;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                anim.SetBool("isWalking", true);
                
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
            if (distance > maxDistance)
            {
                speed = maxSpeed;
            }
            else if (distance < minDistance)
            {
                speed = minSpeed;
            }
            else
            {
                // Calculate speed based on mouse distance from player
                float distanceRatio = (distance - minDistance) / (maxDistance - minDistance);
                float diffSpeed = maxSpeed - minSpeed;
                speed = (distanceRatio * diffSpeed) + minSpeed;
                anim.speed = (distanceRatio + diffSpeed) + minSpeed;
            }
            // Play delayed footsteps
            timer += Time.deltaTime;
            if (timer >= repeatTime)
            {
                AudioManager.GetInstance().RandomizeSfx(footstep1, footstep2);
                timer = 0;
            }   
            // Move
		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,
                                         mousePos,
                                         1 / (speed * (Vector3.Distance(gameObject.transform.position, mousePos))) * Time.deltaTime);

            //Please father forgive me for the magic numbers 
            if(transform.position.x > 74 - 7)
            {
                transform.position = new Vector2(74 - 7, transform.position.y);
            }
            if (transform.position.x < -7)
            {
                transform.position = new Vector2(-7, transform.position.y);
            }
            if (transform.position.y > 74 - 7)
            {
                transform.position = new Vector2(transform.position.x, 74 - 7);
            }
            if (transform.position.y < -7)
            {
                transform.position = new Vector2(transform.position.x, -7);
            }

        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        #endregion
    }

    public void MouseOverUI(bool isIt)
    {
        isMouseOverUI = isIt;
    }
    
}
