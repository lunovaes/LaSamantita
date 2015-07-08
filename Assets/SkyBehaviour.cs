using UnityEngine;
using System.Collections;

public class SkyBehaviour : MonoBehaviour {

	public GameObject Character;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(Character.transform.position.x, this.transform.position.y, this.transform.position.z);
	}
}
