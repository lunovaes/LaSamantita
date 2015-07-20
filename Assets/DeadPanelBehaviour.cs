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

	public void ShowDeadPanel(){
		StartCoroutine (displayDeadPanel ());
	}

	private IEnumerator fadeBg(){
		while(Bg.color.a!= 1){
			Color c = Bg.color;
			c.a += 0.0005f;
			Bg.color = c;
			yield return new WaitForSeconds(0.1f);
		}
	}

	private IEnumerator displayDeadPanel(){
		YouAreDeadLabel.gameObject.SetActive(true);
		Bg.gameObject.SetActive(true);
		StartCoroutine (fadeBg ());
		yield return new WaitForSeconds(2);
		YourScoreLabel.gameObject.SetActive(true);
		Score.gameObject.SetActive(true);
		yield return new WaitForSeconds(1);
		RetryLabel.gameObject.SetActive(true);
	}
}
