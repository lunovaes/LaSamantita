using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {
	public Text Distance;
	public Text Multiplier;
	public Text Score;
	// Use this for initialization
	void Start () {
	
	}

	public void setDistance(int score){
		Distance.text = score.ToString();
	}

	public int getDistance(){
		return int.Parse(Distance.text);
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
