using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerScript : MonoBehaviour {
	public static SoundPlayerScript main;
	public AudioClip[] sounds;
	private bool isOn = true;

	// Use this for initialization
	void Start () {
		main = this;
		PlaySound(Random.Range(0, 1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySound(int index)
	{
		if (isOn)
		{
			AudioSource.PlayClipAtPoint(sounds[index], transform.position);
		}
	}

	public void PlayPickCardSound()
	{
		PlaySound(Random.Range(4, 8));
	}

	public void PlayScrambleSound()
	{
		PlaySound(3);
	}

	public void ToggleSound()
	{
		isOn = !isOn;
	}
}
