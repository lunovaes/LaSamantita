using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeadPanelBehaviour : MonoBehaviour {

	public Text YouAreDeadLabel;
	public Text YourScoreLabel;
	public Text Score;
	public Text RetryLabel;
	public Image Bg;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private IEnumerator displayDeadPanel(){
		YouAreDeadLabel.enabled = true;
		Bg.enabled = true;
		yield return new WaitForSeconds(2);
		YourScoreLabel.enabled = true;
		Score.enabled = true;
		yield return new WaitForSeconds(2);
		RetryLabel.enabled = true;
	}
}
