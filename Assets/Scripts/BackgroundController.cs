using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {
	public float speed;
	public GameObject backgroundQuad;

	public void Scroll(float relativeSpeed){
		Vector2 offset = new Vector2(relativeSpeed * Time.deltaTime * speed, 0);

			backgroundQuad.GetComponent<Renderer>().material.mainTextureOffset += offset ;

	}
}
