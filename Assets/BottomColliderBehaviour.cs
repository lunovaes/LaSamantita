using UnityEngine;
using System.Collections;

public class BottomColliderBehaviour : MonoBehaviour {

	public CharacterController2D Character;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(Character.CharacterState == CharacterController2D.CharacterStates.DEAD)
			return;
		if (coll.gameObject.name == "Enemy(Clone)"){
			Debug.Log(coll.gameObject.name);
			Character.Jump ();
			Destroy(coll.gameObject);

		}

		if (coll.gameObject.name == "Platform(Clone)"){
			Character.SetJumpRelativeSpeed(0);
			Character.CharacterState = CharacterController2D.CharacterStates.GROUNDED;
		}
	}
	void OnCollisionExit2D(Collision2D coll) {
		if(Character.CharacterState == CharacterController2D.CharacterStates.DEAD)
			return;
		if (coll.gameObject.name == "Platform(Clone)"){
			Character.CharacterState = CharacterController2D.CharacterStates.FALLING;
		}
	}
}
