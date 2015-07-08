using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	public GameObject Character;
	public float Offset_x;
	public float Offset_y;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (Character.transform.position.x, 
		                                       Character.transform.position.y,
		                                      -10);
	}
}
