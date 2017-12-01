using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletScript : MonoBehaviour {

	public float velX = 0f;
	float velY = 0f;
	Rigidbody2D rb;
	public float speed = 2;
	public LayerMask doNotHit;
	public float dmg = 50;




	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D>();

	}

	// Update is called once per frame
	void Update () {

		rb.velocity = new Vector2(velX, velY) * speed;
		Destroy(gameObject, 3f);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("walls")){
			Destroy(gameObject);
		}
		if (other.gameObject.CompareTag ("enemy1")) {
			Destroy(gameObject);
		}
		if (other.gameObject.CompareTag ("enemy3")) {
			Destroy(gameObject);
		}

		if (other.gameObject.CompareTag("player")){
			Destroy(gameObject);
		}
		if (other.gameObject.CompareTag("boss")){
			Destroy(gameObject);
		}
	}

}



