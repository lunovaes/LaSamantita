using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {
	public Text Distance;
	public Text Orbs;
	public Text Multiplier;
	public Text Score;
	public Input GUIInput;
	public CharacterController2D Character;
	public Slider ProgressBar;
	public GameObject DeadPanel;
	public LevelManager Level;
	public GameObject WinningPanel;

	// Use this for initialization
	void Start () {
	}

	public void arrowButtonRightAction(){
		Debug.Log ("BOTAO DIREITA");
		Character.setAxisH (true);
	}

	public void arrowButtonOnRelease(){
		Character.resetAxis ();
	}

	public void arrowButtonLeftAction(){
		Debug.Log ("BOTAO Esquerda");
		Character.setAxisH (false);
	}

	public void arrowButtonRightActionTouch(){
		Debug.Log ("BOTAO DIREITA");
		Character.setAxisHTouch (true);
		StartCoroutine (delayedResetButton ());
	}

	private IEnumerator delayedResetButton(){
		yield return new WaitForSeconds (0.0001f);
		Character.resetAxis ();
	}

	public void arrowButtonLeftActionTouch(){
		Debug.Log ("BOTAO Esquerda");
		Character.setAxisHTouch (false);
		StartCoroutine (delayedResetButton ());
	}

	public void TouchJump(){
		Character.InputTouchJump();
	}

	public void setDistance(int dist){
		float value = (float) dist / (Level.DistanceToGenerateWin+220);
		ProgressBar.value = value;
	}

	public void setOrbs(int orbs){
		Orbs.text = orbs.ToString();
	}

	public void setMultiplier(int mult){
		Multiplier.text = mult + "x";
		Multiplier.color = getMultiplierTextColor (mult);
	}

	public Color getMultiplierTextColor(int mult){
		switch (mult) {
		case 1:
			return new Color32(188, 255, 0, 255);
			break;
		case 2:
			return new Color32(0, 160, 255, 255);
			break;
		case 4:
			return new Color32(30, 0, 255, 255);
			break;
		case 6:
			return new Color32(199, 0, 255, 255);
			break;
		case 8:
			return new Color32(255, 0, 153, 255);
			break;
		default:
			return new Color32(188, 255, 0, 255);
			break;
		}
	}

	public void setScore(int score){
		Score.text = score+"";
	}

	private void ShowDeadPanel(){
		DeadPanel.SetActive (true);
		DeadPanel.GetComponent<DeadPanelBehaviour> ().ShowDeadPanel ();
	}

	public void ShowWinningPanel(){
		WinningPanel.SetActive (true);
	}
	
	private void HideWinningPanel(){
		WinningPanel.SetActive (false);
	}

	public void TryAgain(){
		Application.LoadLevel (1);
	}
	
	// Update is called once per frame
	void Update () {
		if (Character.characterState == CharacterController2D.CharacterStates.DEAD) {
			ShowDeadPanel();
		}
	}
}
