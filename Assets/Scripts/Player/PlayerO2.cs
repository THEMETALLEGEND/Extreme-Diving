using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerO2 : MonoBehaviour
{
	public float maxOxygen = 15f; // Maximum amount of oxygen
	public float currentOxygen; // Current amount of oxygen
	public float oxygenDepletionRate = 1f; // Rate at which oxygen depletes per second
	public float oxygenReplenishRate = 20f; // Rate at which oxygen is replenished when above water
	public bool isUnderwater = false; // Flag to check if the player is underwater

	public Slider slider;

	void Start()
	{
		currentOxygen = maxOxygen; // Initialize current oxygen to the maximum value

		EnterWater();
	}

	void Update()
	{
		if (isUnderwater)
		{
			DepleteOxygen();
		}
		else
		{
			ReplenishOxygen();
		}

		// Check if oxygen has run out
		if (currentOxygen <= 0)
		{
			HandleOxygenDepletion();
		}

		slider.value = currentOxygen / maxOxygen;
	}

	void DepleteOxygen()
	{
		// Reduce the oxygen level over time
		currentOxygen -= oxygenDepletionRate * Time.deltaTime;
		currentOxygen = Mathf.Clamp(currentOxygen, 0, maxOxygen); // Clamp the value between 0 and maxOxygen
	}

	void ReplenishOxygen()
	{
		// Replenish the oxygen level over time
		currentOxygen += oxygenReplenishRate * Time.deltaTime;
		currentOxygen = Mathf.Clamp(currentOxygen, 0, maxOxygen); // Clamp the value between 0 and maxOxygen
	}

	void HandleOxygenDepletion()
	{
		// Handle what happens when oxygen runs out
		//Debug.Log("Player has run out of oxygen!");
		// Implement actions like reducing health, triggering a game over, etc.
	}

	// Call this method when the player enters water
	public void EnterWater()
	{
		isUnderwater = true;
	}

	// Call this method when the player exits water
	public void ExitWater()
	{
		isUnderwater = false;
	}
}