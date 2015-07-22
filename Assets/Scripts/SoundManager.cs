using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip[] PlayerJumpClips;
	public AudioClip DeadClip;
	public AudioClip WinningClip;
	private AudioSource mSource;

	// Use this for initialization
	void Start () {
		mSource = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayDeadClip(){
		mSource.Stop ();
		mSource.PlayOneShot (DeadClip);
	}

	public void PlayWinningClip(){
		mSource.Stop ();
		mSource.PlayOneShot (WinningClip);
	}
}
