using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab; // Enemy prefab object
	public float width; // width of the square of the enemy formation
	public float height; // height of the square of the enemy formation
	public float speed; // speed of the enemy formation

	private bool movingRight = true; // 
	private float xmin; // left border of the screen
	private float xmax; // right border of the screen

	// Use this for initialization
	void Start () {
		setBoundaries ();
		instantiateEnemies ();
	}

	/**
	 * Spawn the enemies in the positions
	 */
	public void instantiateEnemies () {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	/*
	 * Set the left and right borders of the screen
	 */
	public void setBoundaries () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;

		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));

		xmin = leftBoundary.x;
		xmax = rightBoundary.x;
	}

	/*
	 * Draw the square of the enemy formation
	 */
	public void OnDrawGizmos () {
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height));
	}
	
	// Update is called once per frame
	void Update () {
		enemyMovement ();
		changeDirection ();

		if (AllMembersDead ()) {
			instantiateEnemies ();
		}
	}

	/*
	 * Movement of the emeny formation
	 */
	public void enemyMovement () {
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
	}

	/*
	 * If the formation reach a border of the screen, change the direction
	 */
	public void changeDirection () {
		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);

		if (leftEdgeOfFormation < xmin) {
			movingRight = true;
		} else if (rightEdgeOfFormation > xmax) {
			movingRight = false;
		}
	}

	/*
	 * Boolean that controls if there's an enemy left
	 */
	bool AllMembersDead() {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}
}
