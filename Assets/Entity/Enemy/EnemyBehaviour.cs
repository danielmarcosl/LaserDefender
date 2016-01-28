using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject projectile;
	public float health;
	public float projectileSpeed;
	public float shootsPerSeconds;
	public int scoreValue = 100;

	private ScoreKeeper scoreKeeper;

	void Start () {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void Update () {
		float probability = Time.deltaTime * shootsPerSeconds;

		if (Random.value < probability) {
			Fire ();
		}
	}

	void Fire () {
		Vector3 startPosition = transform.position + new Vector3(0,-0.7f,0);
		GameObject beam = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -projectileSpeed,0);
	}

	void OnTriggerEnter2D (Collider2D col) {
		Projectile missile = col.gameObject.GetComponent<Projectile> ();

		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit ();
			if (health <= 0) {
				Destroy (gameObject);
				scoreKeeper.Score (scoreValue);
			}
		}
	}
}
