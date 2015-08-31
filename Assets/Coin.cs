using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public int points;
	public Vector3 localPos;

	// Use this for initialization
	void Start () {
		localPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Gotten(){
		Animator anim = this.GetComponent<Animator>();
		anim.Play("collectable_collected", 1);
	}
	
	public void selfDestroy(){
		StopAllCoroutines();
		Destroy(this.gameObject);
	}
}
