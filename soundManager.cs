using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class soundManager : MonoBehaviour {

	public static AudioClip enemyHit, shooting, playerHit, enemyDie;
	static AudioSource audioSrc;
	// Use this for initialization
	void Start () {

		audioSrc = GetComponent<AudioSource> ();
		playerHit = Resources.Load<AudioClip> ("playerHit");
		shooting = Resources.Load<AudioClip> ("shooting");
		enemyHit = Resources.Load<AudioClip> ("enemyHit");
		enemyDie = Resources.Load<AudioClip> ("enemyDie");

	
	}
	
	// Update is called once per frame
	void Update () {


	}

	public static void PlaySound(string clip){

		switch (clip) {
		case "playerHit":
			audioSrc.PlayOneShot(playerHit);
			break;
		case "shooting":
			audioSrc.PlayOneShot(shooting);
			break;
		case "enemyHit":
			audioSrc.PlayOneShot(enemyHit);
			break;
		case "enemyDie":
			audioSrc.PlayOneShot(enemyDie);
			break;
			

		}

	}
}
