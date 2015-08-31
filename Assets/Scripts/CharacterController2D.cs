using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {

	#region PRIVATE VAR
	private bool 	onGround;
	private bool 	onJump;
	private float 	jumpRelativeSpeed;
	private bool 	mIsCombo;
	private float 	axisH 				= 0;
	private float 	axisV 				= 0;
	private bool 	inputTouchJump		= false;
	private bool 	WIN 				= false;
	#endregion

	#region PUBLIC VAR
	public float 		speed;
	public float 		initialJumpSpeed;
	public float 		jumpSpeed;
	public float 		gravity;
	public int 			distance;
	public int 			checkpoint;
	public int 			score;
	public int 			orbs;
	public int 			comboMultiplier;
	public GameObject 	bottomObject;
	public GameObject 	pointObject;
	public GameObject 	coinContainer;
	#endregion

	#region MANAGER VAR
	public CharacterStates 			characterState;
	public GUIController 			GUI;
	public SoundManager 			soundController;
	public BackgroundController[] 	backgroundComponents;
	#endregion

	#region ENUM
	public enum CharacterStates{
		FALLING,
		GROUNDED,
		DEAD
	}
	#endregion

	#region INIT
	void Start () {
		checkpoint = 0;
		ChangeState(CharacterStates.FALLING);
		comboMultiplier = 1;
		score = 0;
		orbs = 0;
		mIsCombo = false;
	}
	#endregion

	#region FRAME UPDATE
	void Update(){
		if (WIN)
			return;
		
		if (characterState == CharacterStates.DEAD) {
			setDeadAnimationState ();
			return;
		}
		if (Input.GetKeyDown ("space") || inputTouchJump) {
			inputTouchJump = false;
			if(characterState == CharacterStates.GROUNDED)
				Jump();
		}
		updateGUI ();
	}
	
	public void updateGUI(){
		GUI.setDistance(distance);
		GUI.setOrbs(orbs);
		GUI.setScore (score);
		GUI.setMultiplier (comboMultiplier);
	}

	public void updateBackground(float x){
		foreach(BackgroundController b in backgroundComponents){
			b.Scroll(x);
		}
	}

	// Update is called once per frameW
	void FixedUpdate () {
		if (WIN)
			return;
		
		if(characterState == CharacterStates.DEAD)
			return;
		float x = 0.0f;
		float y = 0.0f;
		
		
		if(characterState == CharacterStates.FALLING){
			jumpRelativeSpeed -= gravity * Time.fixedDeltaTime;
			y = jumpRelativeSpeed * Time.fixedDeltaTime;
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
		
		distance += Mathf.RoundToInt(x*10);
		updateBackground(x);
		
		this.transform.position += new Vector3(x, y, 0);
	}
	#endregion

	#region MOVEMENT
	public void Jump(){
		playJumpSound ();
		ChangeAnimationState("main_jump");
		ChangeState(CharacterStates.FALLING);
		jumpRelativeSpeed = initialJumpSpeed;
	}
	
	private float Walk(float input){
		float x = input * speed;
		if(characterState == CharacterStates.GROUNDED){
			ChangeAnimationState("main_walking");
		}
		if(x < 0){
			this.transform.FindChild("Sprite").rotation = Quaternion.Euler(0, -180, 0);
		}else{
			this.transform.FindChild("Sprite").rotation = Quaternion.Euler(0, 0, 0);
		}
		return x;
	}

	public void SetJumpRelativeSpeed(float speed){
		jumpRelativeSpeed = speed;
	}

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
	#endregion

	#region SOUND
	private void playJumpSound (){
		this.GetComponent<AudioSource>().PlayOneShot(soundController.PlayerJumpClips[comboMultiplier-1]);
	}
	#endregion

	#region CHARACTER STATE
	private void ChangeAnimationState(string state){
		this.transform.FindChild("Sprite").GetComponent<Animator>().Play(state);
	}

	public void Win(){
		soundController.PlayWinningClip ();
		GUI.ShowWinningPanel ();
		WIN = true;
	}
	
	public void Die(){
		ChangeState(CharacterStates.DEAD);
		bottomObject.SetActive (false);
		Vector2 v = this.GetComponent<Collider2D> ().offset;
		v.y = -0.11f;
		this.GetComponent<Collider2D> ().offset = v;
		soundController.PlayDeadClip ();
	}

	private void setDeadAnimationState(){
		ChangeAnimationState("main_dead");
	}
	public void ChangeState(CharacterStates state){
		//Debug.Log("Changing state from: " + CharacterState.ToString() + " to: " + state.ToString());
		if(characterState == CharacterStates.DEAD)
			return;
		characterState = state;
	}
	#endregion

	#region SCORE
	private void addComboMultiplier(){
		if(comboMultiplier < 4)
		   comboMultiplier = 2*comboMultiplier;
		else {
			comboMultiplier += 2;
		}
	}

	private void setScore(int points){
		score += points;
		GameObject point = Instantiate (Resources.Load ("Point")) as GameObject;
		point.transform.parent = this.transform;
		point.GetComponent<PointComponentController> ().setPoint (points);
		if(comboMultiplier < 8)
			addComboMultiplier();
	}
	#endregion

	#region COLLISION
	void OnCollisionEnter2D(Collision2D coll) {
		Collider2D bottomCollider = coll.contacts[1].otherCollider;
		Debug.Log (coll.gameObject.name);

		if (coll.gameObject.tag == "Platform"){
			if (bottomCollider.name == "BottomCollider"){
				SetJumpRelativeSpeed(0);
				comboMultiplier = 1;
			}
			ChangeState(CharacterStates.GROUNDED);
		}

		if (coll.gameObject.name == "Enemy(Clone)"){
			if (bottomCollider.name == "BottomCollider"){
				//;
				Jump ();
				setScore(coll.gameObject.GetComponent<EnemyBehaviour>().points * comboMultiplier);

				Destroy(coll.gameObject);	
			}
			else{
				if(characterState != CharacterStates.DEAD)
					Die ();
			}
		}

		if (coll.gameObject.name == "Checkpoint"){
			checkpoint++;
			Debug.Log(checkpoint);
		}

		if (coll.gameObject.name == "End"){
			Die();
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		Collider2D bottomCollider = coll.contacts[1].otherCollider;

		if (coll.gameObject.tag == "Platform"){
			if (bottomCollider.name == "BottomCollider"){
				SetJumpRelativeSpeed(0);
			}
			ChangeState(CharacterStates.GROUNDED);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.name == "Checkpoint"){
			checkpoint++;
			coll.gameObject.GetComponentInParent<PlatformController>().PlatformId = checkpoint;
			Destroy(coll.gameObject);
			Debug.Log(checkpoint);
		}

		if (coll.gameObject.tag == "FinishSpot"){
			Win ();
		}

		if (coll.gameObject.tag == "Coin"){
			Coin c = coll.gameObject.GetComponent<Coin>();
			setScore(c.points * comboMultiplier);
			c.gameObject.transform.parent = this.transform;
			c.Gotten();
			orbs++;
			updateGUI();
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Platform"){
			ChangeState(CharacterStates.FALLING);
		}
	}
	#endregion

	#region INPUT
	public void InputTouchJump(){
		inputTouchJump = true;	
	}
	#endregion
}
