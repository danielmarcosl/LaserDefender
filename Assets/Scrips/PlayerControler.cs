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
		Vector3 pos = new Vector3 (this.transform.position.x, this.transform.position.y, 0f);
		// Movement by key press once
		// Left movement, key press once
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			pos.x -= 0.25f;
			gameObject.transform.position = pos;
		}
		// Right movement, key press once
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			pos.x += 0.25f;
			gameObject.transform.position = pos;
		}
		// Up movement, key press once
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			pos.y += 0.25f;
			gameObject.transform.position = pos;
		}
		// Down movement, key press once
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			pos.y -= 0.25f;
			gameObject.transform.position = pos;
		}
		// Movement by key hold down
		// Left movement, key hold down
		if (Input.GetKey (KeyCode.LeftArrow)) {
			pos.x -= 0.05f;
			gameObject.transform.position = pos;
		}
		// Right movement, key hold down
		if (Input.GetKey (KeyCode.RightArrow)) {
			pos.x += 0.05f;
			gameObject.transform.position = pos;
		}
		// Up movemnet, key hold down
		if (Input.GetKey (KeyCode.UpArrow)) {
			pos.y += 0.05f;
			gameObject.transform.position = pos;
		}
		// Down movement, key hold down
		if (Input.GetKey (KeyCode.DownArrow)) {
			pos.y -= 0.05f;
			gameObject.transform.position = pos;
		}
	}
}
