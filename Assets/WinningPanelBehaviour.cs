using UnityEngine;
using System.Collections;

public class WinningPanelBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextLevel(){
		Application.LoadLevel (1);
	}

	public void MainMenu(){
		Application.LoadLevel (0);
	}

	public void ShowWinningPanel(){
		this.gameObject.SetActive (true);
	}

	public void HideWinningPanel(){
		this.gameObject.SetActive (false);
	}
}
