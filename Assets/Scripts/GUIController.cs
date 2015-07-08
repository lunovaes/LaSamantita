using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {
	public Text Score;
	// Use this for initialization
	void Start () {
	
	}

	public void setScore(int score){
		Score.text = score.ToString();
	}

	public int getScore(){
		return int.Parse(Score.text);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
