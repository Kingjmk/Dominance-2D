using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    
    public AudioSource [] AD;
	public AudioClip [] AudioClips;
	private AudioSource AudN;
	private AudioSource AudL;

	void Start () {
		AD = this.gameObject.transform.GetChild(2).transform.GetComponents<AudioSource>();
		AudL = AD[0];
		AudN = AD[1];
	}

	// Update is called once per frame
	void Update () {

	}
	public void PlayOnce(int ac, float vol) {
		AudN.PlayOneShot(AudioClips[ac] , vol / (10 * 1.0f));
	}
	public void PlaySound(int ac) {
		AudL.clip = AudioClips[ac];
		AudL.loop = true;
		if(!AudL.isPlaying)
		AudL.Play();
	}
	public void StopSound() {
        AudL.Stop();
	}
}
