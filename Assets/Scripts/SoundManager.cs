using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioSource seSource;

	public void PlayClip (AudioClip clip) {
		seSource.clip = clip;	
		seSource.Play ();
	}

}
