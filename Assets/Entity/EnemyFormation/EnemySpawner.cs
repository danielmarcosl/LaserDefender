using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab; // Enemy prefab object
	public float width; // Width of the square of the enemy formation
	public float height; // Height of the square of the enemy formation
	public float speed; // Speed of the enemy formation
	public float spawnDelay; // Time between the spawn of two enemies

	private bool movingRight = true; // Movement direction true = right, false = left
	private float xmin; // Left border of the screen
	private float xmax; // Right border of the screen

	// Use this for initialization
	void Start () {
		setBoundaries ();
		//instantiateEnemies ();
		SpawnUntilFull ();
	}

	/**
	 * Spawn all the enemies at the same time in the positions
	 */
	public void instantiateEnemies () {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	/**
	 * Spawn a single enemy in a free position
	 */
	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition ();

		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition()) {
			Invoke ("SpawnUntilFull", spawnDelay); // Recall this function in a 'spawnDelay' time
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
			SpawnUntilFull ();
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

	Transform NextFreePosition() {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
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
