using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {

	private bool onGround;
	private bool onJump;

	public GameObject BleedingObject;

	public float speed;
	public float InitialJumpSpeed;
	public float JumpSpeed;
	public float Gravity;
	private float JumpRelativeSpeed;
	public int Distance;
	public int Checkpoint;

	public CharacterStates CharacterState;

	public GUIController GUI;

	public SoundManager SoundController;


	// Use this for initialization
	void Start () {
		Checkpoint = 0;
		ChangeState(CharacterStates.FALLING);
		StopBleeding();
	}

	public enum CharacterStates{
		FALLING,
		GROUNDED,
		DEAD
	}

	void Update(){
		if(CharacterState == CharacterStates.DEAD)
			return;
		if (Input.GetKeyDown ("space")) {
			Jump ();
		}
	}

	public void updateGUI(){
		
	}
	// Update is called once per frameW
	void FixedUpdate () {
		if(CharacterState == CharacterStates.DEAD)
			return;
		float x = 0.0f;
		float y = 0.0f;


		if(CharacterState == CharacterStates.FALLING){
			JumpRelativeSpeed -= Gravity * Time.fixedDeltaTime;
			y = JumpRelativeSpeed * Time.fixedDeltaTime;
		}

		if(Input.GetAxis("Horizontal") != 0)
			x = Walk(Input.GetAxis("Horizontal"));
		else
			ChangeAnimationState("main_idle");

		Distance += Mathf.RoundToInt(x*100);
		GUI.setScore(GUI.getScore() + Mathf.RoundToInt(x*100));
		this.transform.position += new Vector3(x, y, 0);
	}

	public void Jump(){
		this.GetComponent<AudioSource>().PlayOneShot(SoundController.PlayerJumpClip);
		Debug.Log(this.GetComponent<AudioSource>().clip);
		ChangeAnimationState("main_jump");
		ChangeState(CharacterStates.FALLING);
		JumpRelativeSpeed = InitialJumpSpeed;
	}

	private float Walk(float input){
		float x = input * speed;
		if(CharacterState == CharacterStates.GROUNDED)
			ChangeAnimationState("main_walking");
		if(x < 0){
			this.transform.rotation = Quaternion.Euler(0, -180, 0);
		}else{
			this.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		return x;
	}

	private void ChangeAnimationState(string state){
		GetComponent<Animator>().Play(state);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Collider2D bottomCollider = coll.contacts[1].otherCollider;


		if (coll.gameObject.name == "Enemy(Clone)"){
			if (bottomCollider.name == "BottomCollider"){
				//Debug.Log(coll.gameObject.name);
				Jump ();
				Destroy(coll.gameObject);	
			}
			else{
				Die ();
			}
		}

		if (coll.gameObject.name == "Checkpoint"){
			Checkpoint++;
			Debug.Log(Checkpoint);
		}

		if (coll.gameObject.name == "End"){
			Application.LoadLevel(0);
		}

		if (coll.gameObject.name == "Platform(Clone)"){
			if (bottomCollider.name == "BottomCollider"){
				SetJumpRelativeSpeed(0);
			}

			ChangeState(CharacterStates.GROUNDED);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.name == "Checkpoint"){
			Checkpoint++;
			coll.gameObject.GetComponentInParent<PlatformController>().PlatformId = Checkpoint;
			Debug.Log(Checkpoint);
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.name == "Platform(Clone)"){
			ChangeState(CharacterStates.FALLING);
		}
	}

	public void SetJumpRelativeSpeed(float speed){
		JumpRelativeSpeed = speed;
	}

	public void Die(){
		Bleed();
		ChangeAnimationState("main_dead");
		ChangeState(CharacterStates.DEAD);

	}

	public void ChangeState(CharacterStates state){
		Debug.Log("Changing state from: " + CharacterState.ToString() + " to: " + state.ToString());
		if(CharacterState == CharacterStates.DEAD)
			return;
		CharacterState = state;
	}

	public void Bleed(){
		BleedingObject.gameObject.SetActive(true);
	}

	public void StopBleeding(){
		BleedingObject.gameObject.SetActive(false);
	}
}
