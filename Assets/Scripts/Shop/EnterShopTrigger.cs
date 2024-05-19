using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnterShopTrigger : MonoBehaviour
{
	private CinemachineVirtualCamera cm;

	private GameObject player;
	private GameObject gunController;

	// Start is called before the first frame update
	void Start()
	{
		cm = GameObject.Find("CM Surface Camera").GetComponent<CinemachineVirtualCamera>();
		player = GameObject.Find("Player");
		gunController = GameObject.Find("GunController");
	}

	private void OnCollisionEnter2D(Collision2D other)
	{

		if (other.gameObject.tag == "Player")
		{

			PlayerDamageHandler playerDamageHandler = other.gameObject.GetComponent<PlayerDamageHandler>();

			if (!playerDamageHandler.isDead)
				EnterShop();


		}
	}

	void EnterShop()
	{
		cm.Priority = 2;

		SpriteRenderer spriteRenderer = player.transform.GetChild(0).GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = false;

		PlayerO2 playerO2 = player.GetComponent<PlayerO2>();
		playerO2.ExitWater();

		PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
		playerMovement.currentVelocity = Vector2.zero;
		playerMovement.targetVelocity = Vector2.zero;
		playerMovement.enabled = false;

		Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
		rb.velocity = Vector2.zero;

		gunController.SetActive(false);

		PlayerInventory playerInventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
		playerInventory.SellFish();
	}
}
