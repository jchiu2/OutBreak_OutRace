﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private GameController gameController;
	public Blood bloodPrefab;

	private float gameoverTime = 2f;
	private float gameoverTimer;
	public bool hasBeenHit = false;

	public float playerSpeed;
	private float playerAcceleration;
	private float playerBreak;
	
	void Start ()
	{
		gameController = GameObject.FindObjectOfType<GameController> ();

		//sets initial values
		playerAcceleration = 5 * Time.deltaTime;
		playerBreak = 10 * Time.deltaTime;
		playerSpeed = 0f;
	}
	
	void Update ()
	{
		//this line will always move the car based on its "current" speed
		transform.position += transform.right * playerSpeed * Time.deltaTime;

		//if the Right Arrow is pressed, and the speed is lower than 5
		//speed will be set to 5, gives some instant acceleration
		if (playerSpeed < 5 && Input.GetKeyDown (KeyCode.RightArrow))
		{
			playerSpeed = 5;
		}

		//handles speed increase/decrease based on Right Arrow input
		if (Input.GetKey (KeyCode.RightArrow))
		{
			SpeedUp ();
		}
		else
		{
			SpeedDown ();
		}

		//handles breaking based on Left Arrow input
		if (Input.GetKey (KeyCode.LeftArrow))
		{
			SpeedBreak ();
		}

		//prevents player moving backwards while no key is being held
		if (playerSpeed < 0 && !Input.GetKey (KeyCode.RightArrow) && !Input.GetKey (KeyCode.LeftArrow))
		{
			playerSpeed = 0;
		}

		//left over from what this prototype was before this
		//keep as it may be useful to reset the car if it gets stuck
		if (gameoverTimer > 0)
		{
			gameoverTimer -= Time.deltaTime;
			if (gameoverTimer <= 0)
			{
				gameController.GameOver ();
			}
		}
	}

	//handles increasing the player speed over time
	void SpeedUp ()
	{
		if (playerSpeed < 15f)
		{
			playerSpeed += playerAcceleration;
		}
	}

	//handles decreasing the player speed over time
	void SpeedDown ()
	{
		if (playerSpeed > 0f)
		{
			playerSpeed -= playerAcceleration;
		}
	}

	//handles decreasing the player speed over time, faster than slowdown
	void SpeedBreak ()
	{
		if (playerSpeed > 5f)
		{
			playerSpeed -= playerBreak;
		}
	}

	//current hit function, just ends the game after spawning some 'blood'
	//expand for each type of hit, see "Car" script for more details
	void Hit ()
	{
		//blood can be used on the zombies
		Blood newBlood = GameObject.Instantiate (bloodPrefab) as Blood;
		newBlood.transform.position = transform.position;

		//hit check is used for gameover and to 'turn off' some functions
		//may be useful for  other things, or remove when other stuff is working
		if (hasBeenHit == false)
		{
			gameoverTimer = gameoverTime;
			hasBeenHit = true;
		}
	}

	//USE the functions below for the pickup messages

	void PickupArmour ()
	{
		//something here to add armour pickup
	}

	void PickupSpikes ()
	{
		//something here to add spikes pickup
	}

	void PickupNitro ()
	{
		//something here to add nitro pickup
	}

	void PickupSupplies ()
	{
		//something here to add supplie score
	}

	void ObstacleSoloZombie ()
	{
		//something here to run single zombie hit - score, damage
	}

	void ObstacleHordeZombie ()
	{
		//something here to add horde zombie hit - score, damage
	}

	void ObstacleBarricade ()
	{
		//something here to add barricade hit - slow speed
	}
}