using UnityEngine;
using System.Collections;

public class GUIMenuController : MonoBehaviour {

	public GameManager GM;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play(){
		Application.LoadLevel (GM.getLastLevelUnlocked());
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
