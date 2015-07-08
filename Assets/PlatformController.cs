using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {
	public GameObject BegTile;
	public GameObject[] MidTiles;
	public GameObject EndTile;
	public int PlatformId;

	public int MaxSize;

	public const float SIZE_FACTOR = 0.42f;

	private int mSize;
	
	// Use this for initialization
	void Start () {
		mSize = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setSize(int size){

		for (int i = 1; i < size; i++) {
			if(MidTiles[i] == null)
				MidTiles[i] = Instantiate(Resources.Load("MID")) as GameObject;
			MidTiles[i].transform.position = MidTiles[i-1].transform.position + new Vector3(SIZE_FACTOR, 0, 0);
			MidTiles[i].transform.parent = this.transform;
			EndTile.transform.position = MidTiles[i].transform.position + new Vector3(SIZE_FACTOR, 0, 0);
			this.GetComponent<BoxCollider2D>().size += new Vector2(0.42f,0);
			this.GetComponent<BoxCollider2D>().offset += new Vector2(0.21f,0);
			mSize++;
		}
	}

	public int getSize(){
		return mSize;
	}
}
