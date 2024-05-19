using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ExitShopTrigger : MonoBehaviour
{
	private CinemachineVirtualCamera cm;

	private GameObject player;
	private GameObject gunController;

	// Start is called before the first frame update
	void Start()
	{
		cm = GameObject.Find("CM Surface Camera").GetComponent<CinemachineVirtualCamera>();

		player = GameObject.Find("Player");
	}

	public void ExitShop()
	{
		cm.Priority = 0;

		SpriteRenderer spriteRenderer = player.transform.GetChild(0).GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = true;

		PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
		playerMovement.enabled = true;

		PlayerO2 playerO2 = player.GetComponent<PlayerO2>();
		playerO2.EnterWater();

		gunController = FindInactiveObjectByName("GunController");
		gunController.SetActive(true);

		player.transform.position = new Vector3(0, 0, 0);
	}

	private GameObject FindInactiveObjectByName(string objectName)
	{
		// FindObjectsOfTypeAll returns all objects in the project, including inactive ones
		UnityEngine.Object[] objects = Resources.FindObjectsOfTypeAll<GameObject>();

		// Iterate through all objects with the specified name
		foreach (UnityEngine.Object obj in objects)
		{
			if (obj is GameObject && obj.name == objectName && !((GameObject)obj).activeSelf)
			{
				return (GameObject)obj;
			}
		}

		return null;
	}
}
