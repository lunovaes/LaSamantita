using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	private GameObject[] mPlatforms;
	public CharacterController2D Character;
	private int mPlatformIndex;
	public float GapSize;
	private int mPlatformHeight;
	public int DistanceToGeneratePlatform;

	public GameObject EnemyPrefab;

	// Use this for initialization
	void Start () {
		mPlatformIndex = 0;
		mPlatforms = new GameObject[10];
		GapSize += PlatformController.SIZE_FACTOR;
		mPlatformHeight = 0;
		mDistanceCounter = 0;
		StartCoroutine (GenerateFirstPlatforms ());
	}
	
	// Update is called once per frame
	void Update () {
		if(Character.Distance+1 > mDistanceCounter + DistanceToGeneratePlatform){
			mDistanceCounter = Character.Distance;
			addPlatform();
		}
	}

	private bool mFirstPlayformsCreated;
	private int mDistanceCounter;

	public IEnumerator GenerateFirstPlatforms(){
		mFirstPlayformsCreated = false;
		while (!mFirstPlayformsCreated) {
			addPlatform();
			yield return new WaitForSeconds(0.4f);
		}
	}

	public void addPlatform(){
		if (mPlatforms [mPlatformIndex] != null){
			if(!mFirstPlayformsCreated){
				mFirstPlayformsCreated = true;
				return;
			}else{
				Destroy (mPlatforms [mPlatformIndex]);
			}
		}
		mPlatforms [mPlatformIndex] = initPlatform(Random.Range(5,10));
		if(mPlatformIndex == 0){
			if(mPlatforms[mPlatforms.Length - 1] != null){
				mPlatforms [mPlatformIndex].transform.position = mPlatforms[mPlatforms.Length - 1].transform.position +
					new Vector3((mPlatforms[mPlatforms.Length - 1].GetComponent<PlatformController>().EndTile.transform.localPosition.x + GapSize), calculatePlatformY(), 0);
			}
		}
		else{
			mPlatforms [mPlatformIndex].transform.position = mPlatforms[mPlatformIndex - 1].transform.position +
				new Vector3((mPlatforms[mPlatformIndex - 1].GetComponent<PlatformController>().EndTile.transform.localPosition.x + GapSize), calculatePlatformY(), 0);
		}

		generateEnemies();

		mPlatformIndex++;
		if (mPlatformIndex >= mPlatforms.Length)
			mPlatformIndex = 0;
	}

	private float calculatePlatformY(){
		float y = 0;

		int type = Random.Range (1, 4);

		if (type == 1){
			if (mPlatformHeight < 3){
				y = y + 1;
				mPlatformHeight++;
			}
		}
		else if (type == 2){
			if (mPlatformHeight > -1){
				y = y - 1;
				mPlatformHeight--;
			}
		}

		return y;
	}

	private void generateEnemies(){
		GameObject e = Instantiate(EnemyPrefab,
		            		mPlatforms [mPlatformIndex].GetComponent<PlatformController>().MidTiles[Mathf.RoundToInt(( mPlatforms [mPlatformIndex].GetComponent<PlatformController>().getSize()+1)/2)].transform.position + new Vector3(-0.21f, 1, 0),
		            		Quaternion.Euler(0, -180, 0)) as GameObject;
		e.GetComponent<EnemyBehaviour>().maxPath = (mPlatforms[mPlatformIndex].GetComponent<PlatformController>().getSize())/4;
	}

	public GameObject initPlatform(int size){
		GameObject container = Instantiate(Resources.Load("Platform")) as GameObject;
		container.GetComponent<PlatformController> ().setSize(size);
		return container;
	}
}
