using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public int smallestFishAvailable = 0;
	public int smallFishAvailable = 0;
	public int normalFishAvailable = 0;
	public int bigFishAvailable = 0;
	public int swordFishAvailable = 0;
	public int sharkAvailable = 0;

	public GameObject smallestFishPrefab;
	public GameObject smallFishPrefab;
	public GameObject normalFishPrefab;
	public GameObject bigFishPrefab;
	public GameObject swordFishPrefab;
	public GameObject sharkPrefab;

	private static PlayerInventory instance;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public static PlayerInventory Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<PlayerInventory>();
				if (instance == null)
				{
					GameObject obj = new GameObject("PlayerInventory");
					instance = obj.AddComponent<PlayerInventory>();
				}
			}
			return instance;
		}
	}
}