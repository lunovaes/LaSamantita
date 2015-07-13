using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {
	public GameObject[] Views;
	private int mViewIndex;
	public LevelManager LevelManager;
	public int DistanceToStackBackground;
	private int mDistanceCounter;
	// Use this for initialization
	void Start () {
		mViewIndex = 0;
		if (LevelManager == null) {
			LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		}
	}

	
	// Update is called once per frame
	void Update () {
		if(LevelManager.Character.Distance > mDistanceCounter + DistanceToStackBackground){
			mDistanceCounter = LevelManager.Character.Distance;
			stackView();
		}
	}

	public int GetViewIndex(){
		return mViewIndex;
	}

	public void stackView(){
		Views [mViewIndex].transform.localPosition += new Vector3 (33 * 3, 0, 0);
		mViewIndex++;
		if (mViewIndex >= Views.Length)

			mViewIndex = 0;
	}
}
