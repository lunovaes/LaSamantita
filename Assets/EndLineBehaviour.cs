using UnityEngine;
using System.Collections;

public class EndLineBehaviour : MonoBehaviour {
	public GameObject Character;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.transform.position;
		pos.x = Character.transform.position.x;
		this.transform.position = pos;
	}
}
