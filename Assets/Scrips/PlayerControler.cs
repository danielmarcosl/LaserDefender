using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		movement ();
	}

	void movement() {
		float speed = 10.0f;

		if (Input.GetKey (KeyCode.LeftArrow)) { // Left movement
			gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.RightArrow)) { // Right movement
			gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.UpArrow)) { // Up movement
			gameObject.transform.position += Vector3.up * speed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.DownArrow)) { // Down movement
			gameObject.transform.position += Vector3.down * speed * Time.deltaTime;
		}
	}
}
