using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private bool firstTime;
	private int lastLevelUnlocked;
	private int lastScore;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
		StoreValue ("lastLevelUnlocked", 1);
		StoreValue ("lastScore", 0);
	}

	public int getLastLevelUnlocked(){
		lastLevelUnlocked = GetIntValue("lastLevelUnlocked");
		return lastLevelUnlocked;
	}

	public int getLastScore(){
		lastScore = GetIntValue("lastScore");
		return lastScore;
	}

	public void StoreValue (string key, float value){
		PlayerPrefs.SetFloat (key, value);
	}
	public void StoreValue (string key, string value){
		PlayerPrefs.SetString (key, value);
	}
	public void StoreValue (string key, int value){
		PlayerPrefs.SetInt (key, value);
	}
	
	public float GetFloatValue(string key){
		return PlayerPrefs.GetFloat (key);
	}

	public string GetStringValue(string key){
		return PlayerPrefs.GetString (key);
	}

	public int GetIntValue(string key){
		return PlayerPrefs.GetInt (key);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
