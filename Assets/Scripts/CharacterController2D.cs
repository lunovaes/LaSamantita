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
	public int Score;
	public int ComboMultiplier;
	private bool mIsCombo;
	public GameObject BottomObject;

	public CharacterStates CharacterState;

	public GUIController GUI;

	public SoundManager SoundController;

	private float axisH = 0;
	private float axisV = 0;
	private bool mInputTouchJump = false;
	
	private float InputGetAxis(string axis){
		float v = Input.GetAxis(axis);
		if (Mathf.Abs(v) > 0.005) return v;
		if (axis=="Horizontal") return axisH;
		if (axis=="Vertical") return axisV;
		return v;
	}

	public void resetAxis(){
		axisV = axisH = 0;
	}

	public void setAxisH(bool signal){
		if (signal)
			axisH = 1;
		else
			axisH = -1;
	}

	public void setAxisHTouch(bool signal){
		if (signal)
			axisH = 0.5f;
		else
			axisH = -0.5f;
	}

	public void setAxisV(bool signal){
		if (signal)
			axisV = 1;
		else
			axisV = -1;
	}

	// Use this for initialization
	void Start () {
		Checkpoint = 0;
		ChangeState(CharacterStates.FALLING);
		ComboMultiplier = 1;
		Score = 0;
		mIsCombo = false;
	}

	public enum CharacterStates{
		FALLING,
		GROUNDED,
		DEAD
	}

	void Update(){

		if (CharacterState == CharacterStates.DEAD) {
			setDeadAnimationState ();
			return;
		}
		if (Input.GetKeyDown ("space") || mInputTouchJump) {
			mInputTouchJump = false;
			if(CharacterState == CharacterStates.GROUNDED)
				Jump();
		}
		updateGUI ();
	}

	public void updateGUI(){
		GUI.setDistance(Distance);
		GUI.setScore (Score);
		GUI.setMultiplier (ComboMultiplier);
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

		#if UNITY_EDITOR
		if(Input.GetAxis("Horizontal") != 0)
			x = Walk(Input.GetAxis("Horizontal"));
		else
			ChangeAnimationState("main_idle");
		#else

		if(InputGetAxis("Horizontal") != 0)
			x = Walk(InputGetAxis("Horizontal"));
		else
			ChangeAnimationState("main_idle");

		#endif

		Distance += Mathf.RoundToInt(x*10);

		this.transform.position += new Vector3(x, y, 0);
	}

	private void playJumpSound (){
		this.GetComponent<AudioSource>().PlayOneShot(SoundController.PlayerJumpClips[ComboMultiplier-1]);
	}

	public void Jump(){
		playJumpSound ();
		ChangeAnimationState("main_jump");
		ChangeState(CharacterStates.FALLING);
		JumpRelativeSpeed = InitialJumpSpeed;
	}

	private float Walk(float input){
		float x = input * speed;
		if(CharacterState == CharacterStates.GROUNDED){
			ChangeAnimationState("main_walking");
		}
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

	private void addComboMultiplier(){
		if(ComboMultiplier < 4)
		   ComboMultiplier = 2*ComboMultiplier;
		else {
			ComboMultiplier += 2;
		}
	}
	public GameObject PointObject;
	private void setScore(int points){
		Score += points;
		GameObject point = Instantiate (Resources.Load ("Point")) as GameObject;
		point.transform.parent = this.transform;
		point.GetComponent<PointComponentController> ().setPoint (points);
		if(ComboMultiplier < 8)
			addComboMultiplier();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Collider2D bottomCollider = coll.contacts[1].otherCollider;
		Debug.Log (coll.gameObject.name);

		if (coll.gameObject.tag == "Platform"){
			if (bottomCollider.name == "BottomCollider"){
				SetJumpRelativeSpeed(0);
				ComboMultiplier = 1;
			}
			ChangeState(CharacterStates.GROUNDED);
		}

		if (coll.gameObject.name == "Enemy(Clone)"){
			if (bottomCollider.name == "BottomCollider"){
				//;
				Jump ();
				setScore(coll.gameObject.GetComponent<EnemyBehaviour>().points * ComboMultiplier);

				Destroy(coll.gameObject);	
			}
			else{
				if(CharacterState != CharacterStates.DEAD)
					Die ();
			}
		}

		if (coll.gameObject.name == "Checkpoint"){
			Checkpoint++;
			Debug.Log(Checkpoint);
		}

		if (coll.gameObject.name == "End"){
			Die();
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		Collider2D bottomCollider = coll.contacts[1].otherCollider;

		if (coll.gameObject.name == "Enemy(Clone)"){
			if(CharacterState == CharacterStates.DEAD)
				Bleed ();
		}
		if (coll.gameObject.tag == "Platform"){
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
			Destroy(coll.gameObject);
			Debug.Log(Checkpoint);
		}

		if (coll.gameObject.tag == "FinishSpot"){
			Application.LoadLevel(0);
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Platform"){
			ChangeState(CharacterStates.FALLING);
		}

		if (coll.gameObject.name == "Enemy(Clone)"){
			StopBleeding ();
		}
	}

	public void SetJumpRelativeSpeed(float speed){
		JumpRelativeSpeed = speed;
	}

	public void Die(){
		ChangeState(CharacterStates.DEAD);
		BottomObject.SetActive (false);
		Vector2 v = this.GetComponent<Collider2D> ().offset;
		v.y = -0.11f;
		this.GetComponent<Collider2D> ().offset = v;
		GameObject exp = Instantiate (Resources.Load ("ExplosionAnimation"), 
		                              this.transform.position ,
		                              this.transform.rotation) as GameObject;
		exp.transform.parent = this.transform;
		setDeadAnimationState ();
	}

	private void setDeadAnimationState(){
		ChangeAnimationState("main_dead");
	}

	public void ChangeState(CharacterStates state){
		//Debug.Log("Changing state from: " + CharacterState.ToString() + " to: " + state.ToString());
		if(CharacterState == CharacterStates.DEAD)
			return;
		CharacterState = state;
	}

	public void Bleed(){
		BleedingObject.GetComponent<EllipsoidParticleEmitter>().emit = true;
	}

	public void StopBleeding(){
		BleedingObject.GetComponent<EllipsoidParticleEmitter>().emit = false;
	}

	public void InputTouchJump(){
		mInputTouchJump = true;	
	}	                       
}
