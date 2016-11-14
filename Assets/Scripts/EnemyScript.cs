using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	private WeaponScript[] weapons;
	private bool hasSpawn;
	private MoveScript moveScript;
	private SpriteRenderer spriteRender;
	private Collider2D collider;

	void Awake() {
		hasSpawn = false;
		weapons = GetComponentsInChildren<WeaponScript>();
		moveScript = GetComponent<MoveScript> ();
		spriteRender = GetComponent<SpriteRenderer> ();
		collider = GetComponent<Collider2D> ();
	}

	// Use this for initialization
	void Start () {
		hasSpawn = false;
		moveScript.enabled = false;
		collider.enabled = false;
		foreach (WeaponScript weapon in weapons) {
			weapon.enabled = false;
		}
	}

	void Spawn() {
		hasSpawn = true;
		moveScript.enabled = true;
		collider.enabled = true;
		foreach (WeaponScript weapon in weapons) {
			weapon.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (hasSpawn == false) {
			if (spriteRender.IsVisibleFrom (Camera.main) == true) {
				Spawn ();
			}
		} else {
			foreach (WeaponScript weapon in weapons) {
				if (weapon != null && weapon.enabled && weapon.CanAttack) {
					weapon.Attack (true);
					SoundEffectsHelper.Instance.MakeEnemyShotSound ();
				}
			}
			if (spriteRender.IsVisibleFrom (Camera.main) == false) {
				Destroy (gameObject);
			}
		}
	}
}
