using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public Vector2 speed = new Vector2 (50, 50);
	public Transform enemyPrefab;

	private Vector2 movement;
	private Rigidbody2D rigidbodyComponent;

	// Use this for initialization
	void Update () {
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");

		movement = new Vector2 (speed.x * inputX, speed.y * inputY);

		bool shoot = Input.GetButtonDown ("Fire1") | Input.GetButtonDown("Fire2");

		if (shoot) {
			WeaponScript weapon = GetComponent<WeaponScript> ();
			if (weapon != null) {
				weapon.Attack (false);
				SoundEffectsHelper.Instance.MakePlayerShotSound ();
			}
		}

		AvoidOutOfScene ();

		MakeEnemy ();
	}

	void AvoidOutOfScene() {
		var dist = (transform.position - Camera.main.transform.position).z;

		var leftBorder = Camera.main.ViewportToWorldPoint (
			new Vector3(0, 0, dist)
		).x;
		var rightBorder = Camera.main.ViewportToWorldPoint (
			new Vector3(1, 0, dist)
		).x;
		var topBorder = Camera.main.ViewportToWorldPoint (
			new Vector3(0, 0, dist)
		).y;
		var bottomBorder = Camera.main.ViewportToWorldPoint (
			new Vector3(0, 1, dist)
		).y;

		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
			transform.position.z
		);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (rigidbodyComponent == null) {
			rigidbodyComponent = GetComponent<Rigidbody2D> ();
		}
		rigidbodyComponent.velocity = movement;
	}

	void OnCollisionEnter2D(Collision2D otherCollider) {
		Debug.Log ("OnCollisionEnter2D");
		bool damagePlayer = false;

		EnemyScript enemy = otherCollider.gameObject.GetComponent<EnemyScript> ();
		if (enemy != null) {
			HealthScript enemyHealth = enemy.GetComponent<HealthScript> ();
			if (enemyHealth != null) {
				enemyHealth.Damage (enemyHealth.hp);
				damagePlayer = true;
			}
		}

		if (damagePlayer) {
			HealthScript playerHealth = GetComponent<HealthScript> ();
			if (playerHealth != null) {
				playerHealth.Damage (1);
			}
		}
	}

	void OnDestroy() {
		var gameOver = FindObjectOfType<GameOverScript> ();
		if (gameOver != null) {
			gameOver.ShowButtons ();
		} else {
			Debug.Log ("game over is null.");
		}
	}

	void MakeEnemy() {
		if (enemyPrefab != null) {
			if (0 == Random.Range (0, 5)) {
				Debug.Log ("Make a enemy");
				var enemyTransform = Instantiate (enemyPrefab) as Transform;
				if (enemyTransform != null) {
					var camaraXPos = Camera.main.transform.position.x;
					var camaraYPos = Camera.main.transform.position.y;
					enemyTransform.position = new Vector3 (
						Random.Range (camaraXPos + 8, camaraXPos + 30), 
						Random.Range (camaraYPos - 9, camaraYPos + 9), 
						0);
				}
			}
		}
	}
}
