using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

	private int smallestFishCost = 1;
	private int smallFishCost = 5;
	private int normalFishCost = 25;
	private int bigFishCost = 50;
	private int swordFishCost = 10;
	private int sharkCost = 100;

	public TextMeshProUGUI fishCountText;
	public TextMeshProUGUI moneyCountText;

	private static PlayerInventory instance;

	[HideInInspector] public int fishCount;
	[HideInInspector] public int moneyCount;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(gameObject);
		UpdateInventory();
	}

	public void UpdateInventory()
	{
		// ќбновл€ем fishCount перед обновлением текста
		fishCount = smallestFishCost * smallestFishAvailable
			+ smallFishCost * smallFishAvailable
			+ normalFishCost * normalFishAvailable
			+ bigFishCost * bigFishAvailable
			+ swordFishCost * swordFishAvailable
			+ sharkCost * sharkAvailable;

		fishCountText.text = fishCount.ToString() + "$";
		moneyCountText.text = moneyCount.ToString() + "$";
	}

	public void SellFish()
	{
		// ѕрисваиваем moneyCount текущий fishCount и обновл€ем текст
		moneyCount += fishCount;
		moneyCountText.text = moneyCount.ToString() + "$";

		smallestFishAvailable = 0;
		smallFishAvailable = 0;
		normalFishAvailable = 0;
		bigFishAvailable = 0;
		swordFishAvailable = 0;
		sharkAvailable = 0;

		UpdateInventory();
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



	public void ReloadScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}
}