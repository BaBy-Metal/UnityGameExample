using UnityEngine;
using System.Collections;

public class SoundEffectsHelper : MonoBehaviour {
	public static SoundEffectsHelper Instance;

	public AudioClip explosionSound;
	public AudioClip playerShotSound;
	public AudioClip enemyShotSound;


	void Awake() {
		if (Instance == null) {
			Instance = this;
		}
	}

	private void MakeSound(AudioClip originalClip) {
		AudioSource.PlayClipAtPoint (originalClip, transform.position);
	}

	public void MakeExplosionSound() {
		MakeSound (explosionSound);
	}

	public void MakePlayerShotSound() {
		MakeSound (playerShotSound);
	}

	public void MakeEnemyShotSound() {
		MakeSound (enemyShotSound);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
