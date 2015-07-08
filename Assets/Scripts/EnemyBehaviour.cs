using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float speed;
	public float maxPath;
	private float mDistanceCounter;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	bool mDirection = false;

	void Update () {
		float x = 0;

		if(mDirection)
			x = Time.deltaTime*speed;
		else
			x = -(Time.deltaTime*speed);

		mDistanceCounter += x;

		if(mDirection){
			this.transform.rotation = Quaternion.Euler(0,0,0);
		}
		else if(!mDirection){
			this.transform.rotation = Quaternion.Euler(0,-180,0);
		}

		this.transform.localPosition += new Vector3(x, 0, 0);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.name == "MinEnemyDistance"){
			mDirection = true;
		}

		if (coll.gameObject.name == "MaxEnemyDistance"){
			mDirection = false;
		}
	}
}
