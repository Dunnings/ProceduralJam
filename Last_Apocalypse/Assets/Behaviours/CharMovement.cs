using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour {

    public static CharMovement Instance;

    public Animator anim;
    public float speed = 50.0f;
    float distance;
    public bool isMouseOverUI = false;
    public bool m_followMouse = false;

	// Use this for initialization
	void Awake () {
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
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,
                                             mousePos,
                                             1 / (speed * (Vector3.Distance(gameObject.transform.position, mousePos))) * Time.deltaTime);
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
