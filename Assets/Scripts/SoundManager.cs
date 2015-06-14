using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioSource seSource;
	public AudioSource bgSource;

	public void PlayClip (AudioClip clip) {
		seSource.clip = clip;	
		seSource.Play ();
	}

	public void PlayBgClip (AudioClip clip) {
		bgSource.clip = clip;	
		bgSource.Play ();
	}

	public void StopBgClip () {
		bgSource.Stop ();
	}

}
