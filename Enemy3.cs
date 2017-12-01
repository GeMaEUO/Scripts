using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour {

	public float stateTimer = 0f, startingHealth = 100, currentHealth, flashingCounter = 0, flashingLength = .5f;
	public int statePicker = 0;
	private Animator enemy3Animator;
	Vector2 bulletPos;
	public GameObject bulletLeft;
	public GameObject bulletRight;
	bool fireBullet;
	bool damaged, isDead = false, invuln = false, flashingActive = false;
	SpriteRenderer enemy1Sprite;

	// Use this for initialization
	void Start () {

		stateTimer = 100f;
		enemy3Animator = GetComponent<Animator>();
		fireBullet = false;
		currentHealth = startingHealth;
		enemy1Sprite = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {

		if (flashingActive) {

			if (flashingCounter > flashingLength * .66f) {

				enemy1Sprite.color = new Color (enemy1Sprite.color.r, enemy1Sprite.color.g, enemy1Sprite.color.b, 0f);

			} else if (flashingCounter > flashingLength * .33f) {
				enemy1Sprite.color = new Color (enemy1Sprite.color.r, enemy1Sprite.color.g, enemy1Sprite.color.b, 1f);
			}	else if (flashingCounter > 0) {
				enemy1Sprite.color = new Color (enemy1Sprite.color.r, enemy1Sprite.color.g, enemy1Sprite.color.b, 0f);
			} else {

				enemy1Sprite.color = new Color(enemy1Sprite.color.r, enemy1Sprite.color.g, enemy1Sprite.color.b, 1f);
				flashingActive = false;

			}

			flashingCounter -= .09f;

		}

		stateTimer -= .5f;



		if (this.enemy3Animator.GetCurrentAnimatorStateInfo (0).IsName ("Up")) {
			enemy3Animator.SetBool ("Up", false);
			enemy3Animator.SetBool ("Shoot", true);
			fireBullet = true;
		} 
			if (this.enemy3Animator.GetCurrentAnimatorStateInfo (0).IsName ("Shoot")) {
			
				enemy3Animator.SetBool ("Shoot", false);
				enemy3Animator.SetBool ("Hit", true);
				if (fireBullet == true) {
				Fire(1);
				fireBullet = false;
				}



			}
			if (this.enemy3Animator.GetCurrentAnimatorStateInfo (0).IsName ("Hit")) {
				enemy3Animator.SetBool ("Hit", false);
				enemy3Animator.SetBool ("Down", true);



			}

			if (stateTimer <= 0) {


				statePicker = Random.Range (0, 10);

				if (statePicker <= 5) {

					enemy3Animator.SetBool ("Up", true);
				stateTimer = 100 * Random.Range (1, 3);
			

				}


			}


		
	}



	void Fire(int numberOfShots)
	{
		
		bulletPos = transform.position;
		if (numberOfShots == 1)
		{
			bulletPos -= new Vector2(1, 0);
			Instantiate(bulletLeft, bulletPos, Quaternion.identity);

			bulletPos += new Vector2(2, 0);
			Instantiate(bulletRight, bulletPos, Quaternion.identity);
			numberOfShots = 0;
		}

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("bullet") && !this.enemy3Animator.GetCurrentAnimatorStateInfo(0).IsName("Down")){
			enemy3Animator.SetBool("Shoot",false);
			enemy3Animator.SetBool("up",false);
			enemy3Animator.SetBool("hit",true);
			TakeDamage(34);
			
		}



	}

	public void TakeDamage (float amount)
	{
		if (!invuln && !isDead)
		{
			soundManager.PlaySound ("enemyHit");
			currentHealth -= amount;
			flashingActive = true;
			flashingCounter = flashingLength;
			invuln = Timer(120);
		}
		if (currentHealth <= 0 && !isDead)
		{
			Death();
		}
	}

	private bool Timer (int maxTimer)
	{
		var timer = maxTimer;
		var active = false;
		while (timer >= 0)
		{
			timer--;
		}
		if (timer > 0)
		{
			active = true;
		}
		else
		{
			active = false;
		}
		return active;
	}

	void Death ()
	{
		soundManager.PlaySound ("enemyDie");
		isDead = true;
		Destroy(gameObject);
	}

}
