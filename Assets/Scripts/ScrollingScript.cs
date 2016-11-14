using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ScrollingScript : MonoBehaviour {
	public Vector2 speed = new Vector2 (2, 2);
	public Vector2 direction = new Vector2(-1, 0);
	public bool isLinkedToCamera = false;
	public bool isLooping = false;

	private List<SpriteRenderer> backgroundPart;


	// Use this for initialization
	void Start () {
		if (isLooping) {
			backgroundPart = new List<SpriteRenderer> ();
			for (var i = 0; i < transform.childCount; ++i) {
				Transform child = transform.GetChild (i);
				SpriteRenderer r = child.GetComponent<SpriteRenderer> ();
				if (r != null) {
					backgroundPart.Add(r);
				}
			}
			backgroundPart = backgroundPart.OrderBy(
				t => t.transform.position.x
			).ToList();
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = new Vector3 (
			speed.x * direction.x,
			speed.y * direction.y,
			0);
		movement *= Time.deltaTime;
		transform.Translate (movement);

		if (isLinkedToCamera) {
			Camera.main.transform.Translate (movement);
		}

		if (isLooping) {
			SpriteRenderer firstChild = backgroundPart.FirstOrDefault ();
			if (firstChild != null) {
				if (firstChild.transform.position.x < Camera.main.transform.position.x) {
					if (firstChild.IsVisibleFrom (Camera.main) == false) {
						Debug.Log ("disapear.");
						SpriteRenderer lastChild = backgroundPart.LastOrDefault ();
						if (lastChild != null) {
							Vector3 lastPosition = lastChild.transform.position;
							Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);
							firstChild.transform.position = new Vector3 (lastPosition.x + lastSize.x, 
								firstChild.transform.position.y, 
								firstChild.transform.position.z);
							backgroundPart.Remove (firstChild);
							backgroundPart.Add (firstChild);
						}
					}
				}
			}
		}
	}
}
