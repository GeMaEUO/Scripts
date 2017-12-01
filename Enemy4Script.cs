using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Script : MonoBehaviour {

	Vector2 bulletPos;
	public GameObject bulletDown;
	public float fireRate = 10.0f;
	float nextFire = 1.0f;
	float shootTimer = 10f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {

		shootTimer += .01f;

		if (shootTimer > nextFire)
		{
			nextFire = shootTimer + fireRate;
			Fire();


			}


	}


	void Fire()
	{
		var shoot = 100000;

		while (shoot > 0) {
			shoot--;
		}
		bulletPos = transform.position;
		if (shoot <= 0)
		{
			bulletPos -= new Vector2(0, 1);
			Instantiate(bulletDown, bulletPos, Quaternion.identity);

		}

	}
}
