﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class portal2 : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {



	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("player")){

			Application.LoadLevel ("mgRoom2");
		}

	}

}
