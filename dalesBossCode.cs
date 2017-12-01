using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dalesBossCode : MonoBehaviour {

	public float Boss_health = 100, currentHealth, flashingCounter = 0, flashingLength = .5f, stateTimer = 0;
	Vector2 bulletPos;
	public GameObject bulletLeft;
	public GameObject bulletright;
	bool fireBullet;
	bool damaged, isDead = false, invuln = false, flashingActive = false;
	private Animator Boss;

	private Animator bossAnim;
	SpriteRenderer bossSprite;

	// Use this for initialization
	void Start () {

		stateTimer = 100f;
		bossAnim = GetComponent<Animator>();
		fireBullet = false;
		currentHealth = Boss_health;
		bossSprite = GetComponent<SpriteRenderer>();



	}

	// Update is called once per frame
	void Update () {

		if(flashingCounter > flashingLength *.66f) {

			bossSprite.color = new Color (bossSprite.color.r, bossSprite.color.g, bossSprite.color.b, 0f);
		}else if(flashingCounter > flashingLength*.33f){
			bossSprite.color = new Color (bossSprite.color.r, bossSprite.color.g, bossSprite.color.b, 1f);
		}	else if (flashingCounter > 0){
			bossSprite.color = new Color(bossSprite.color.r, bossSprite.color.g, bossSprite.color.b, 0f);
		} else {

			bossSprite.color = new Color (bossSprite.color.r, bossSprite.color.g, bossSprite.color.b, 1f);
			flashingActive = false;

		}

		flashingCounter -=.09f;


		stateTimer -= .5f;

		if (currentHealth > 0)
		{

			bossAnim.SetBool("isIdle",true);

			int attack = Random.Range (0, 10);
			switch (attack){
			case 1:
				bossAnim.SetBool ("isDashing", true);
				//if at point 1 then move to point 2
				//else if at point 2 then move to point 1
				//flip image
				//yield return null;

				bossAnim.SetBool ("isDashing", false);
				break;

			case 2:
				bossAnim.SetBool("isJumping", true);
				//if at point 1 then move to point 4 then to point 2
				//else if at point 2 then move to point 3 then to point 1
				//flip image
				bossAnim.SetBool("isJumping", false);
				break;

			case 3:
				bossAnim.SetBool("isShooting", true);
				//charging sound
				//shoot gun
				bossAnim.SetBool("isShooting", false);
				break;

			}
		}
	}


	void fireLeft(int numberOfShots)
	{

		bulletPos = transform.position;
		if (numberOfShots == 1) {

			bulletPos -= new Vector2 (1, 0);
			Instantiate (bulletLeft, bulletPos, Quaternion.identity);
			numberOfShots = 0;

		}



	}
}