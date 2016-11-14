using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {
	private Button[] buttons;

	void Awake() {
		buttons = GetComponentsInChildren<Button> ();
		HideButtons ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void HideButtons() {
		Debug.Log ("Hide Buttons.");
		foreach (var b in buttons) {
			b.gameObject.SetActive (false);
		}
	}

	public void ShowButtons() {
		Debug.Log ("Show Buttons.");
		foreach (var b in buttons) {
			b.gameObject.SetActive (true);
		}
	}

	public void ExitToMenu() {
		Application.LoadLevel ("Menu");
	}

	public void RestartGame() {
		Application.LoadLevel ("Stage1");
	}
}
