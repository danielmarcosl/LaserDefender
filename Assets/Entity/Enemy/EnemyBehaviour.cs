using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject projectile;
	public float health;
	public float projectileSpeed;
	public float shootsPerSeconds;
	public int scoreValue = 100;
	public AudioClip fireSound;
	public AudioClip deathSound;

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
		GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -projectileSpeed,0);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	void OnTriggerEnter2D (Collider2D col) {
		Projectile missile = col.gameObject.GetComponent<Projectile> ();

		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit ();
			if (health <= 0) {
				Die ();
			}
		}
	}

	private void Die () {
		Destroy (gameObject);
		scoreKeeper.Score (scoreValue);
		AudioSource.PlayClipAtPoint (deathSound, transform.position);
	}
}
