using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public void StartGame() {
		Debug.Log ("StartGame");
		Application.LoadLevel ("Stage1");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
