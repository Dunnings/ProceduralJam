using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour {

    public Animator anim;
    public float speed = 50.0f;
    float distance;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // When mouse button is held down
	    if (Input.GetMouseButton(0))
        {
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
                Debug.Log(angle);
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
	}
}
