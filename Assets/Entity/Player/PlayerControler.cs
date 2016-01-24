using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {

	float xmin;
	float xmax;
	float ymin;
	float ymax;

	// Use this for initialization
	void Start () {
		restrictPosition ();
	}

	void restrictPosition() {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		Vector3 upmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, distance));
		Vector3 downmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));

		xmin = leftmost.x + 0.6f;
		xmax = rightmost.x - 0.6f;
		ymin = downmost.y + 0.5f;
		ymax = upmost.y - 0.5f;
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

		// Restrict the player to the gamespace
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		float newY = Mathf.Clamp (transform.position.y, ymin, ymax);
		transform.position = new Vector3 (newX, newY, transform.position.z);
	}
}
