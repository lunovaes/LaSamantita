using UnityEngine;
using System.Collections;

public class PointComponentController : MonoBehaviour {
	void Update(){
		this.transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	public void selfDestroy(){
		Destroy (this.gameObject);
	}
	public void setPoint(int points){
		this.GetComponent<TextMesh> ().text = "" + points + "+";
		this.gameObject.SetActive (true);
	}
}
