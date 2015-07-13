using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {
	public GameObject BegTile;
	public GameObject[] MidTiles;
	public GameObject EndTile;
	public int PlatformId;
	public PlatformType PlatformTp;

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

	public enum PlatformType{
		DIRT,
		ICE,
		DESERT,
		HELL
	}

	private string getPlatformTypeName(){
		switch (PlatformTp) {
			case PlatformType.DIRT :
				return "";
			break;
				case PlatformType.ICE :
			return "Ice";
				break;
			case PlatformType.DESERT :
				return "Desert";
			break;
			case PlatformType.HELL :
				return "Hell";
			break;
			default :
				return "";
			break;
		}
	}

	public void setSize(int size){

		for (int i = 1; i < size; i++) {
			if(MidTiles[i] == null)
				MidTiles[i] = Instantiate(Resources.Load("MID" + getPlatformTypeName())) as GameObject;
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
