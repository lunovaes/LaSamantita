using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	public GameObject Character;
	public float Offset_x;
	public float Offset_y;
	public float MinY;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float y = 0;
		if (Character.transform.position.y < MinY)
			y = MinY;
		else
			y = Character.transform.position.y;
		this.transform.position = new Vector3 (Character.transform.position.x + Offset_x, 
		                                       y + Offset_y,
		                                      -10);
	}
}
