using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopFunctions : MonoBehaviour
{
	// Массивы для хранения значений апгрейдов для каждого уровня
	[HideInInspector] public int[] damageUpgrades = new int[10];
	[HideInInspector] public float[] reloadSpeedUpgrades = new float[10];
	[HideInInspector] public int[] magazineCapacityUpgrades = new int[10];
	[HideInInspector] public float[] oxygenTankCapacityUpgrades = new float[10];
	[HideInInspector] public int[] upgradeCosts = new int[10]; // Массив для хранения стоимости апгрейдов

	public bool isAutomatic = false;

	public TextMeshProUGUI damageLevel;
	public TextMeshProUGUI reloadLevel;
	public TextMeshProUGUI magLevel;
	public TextMeshProUGUI oxygenLevel;

	public TextMeshProUGUI damageUpgradeCosts;
	public TextMeshProUGUI reloadUpgradeCosts;
	public TextMeshProUGUI magUpgradeCosts;
	public TextMeshProUGUI oxygenUpgradeCosts;

	// Текущие уровни апгрейдов
	private int currentDamageLevel = 0;
	private int currentReloadSpeedLevel = 0;
	private int currentMagazineCapacityLevel = 0;
	private int currentOxygenTankCapacityLevel = 0;

	private PlayerInventory playerInventory;

	private Pistol pistol;
	private PlayerO2 playerOxygen;

	// Start is called before the first frame update
	void Start()
	{
		playerInventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
		playerInventory.UpdateInventory();

		// Пример инициализации значений для апгрейдов и их стоимости
		for (int i = 0; i < 10; i++)
		{
			damageUpgrades[i] = 1 + i * 1;  // Урон увеличивается на 1 единицу с каждым уровнем
			reloadSpeedUpgrades[i] = 2f - i * 0.1f;  // Скорость перезарядки уменьшается на 0.1 секунды с каждым уровнем
			magazineCapacityUpgrades[i] = 1 + i * 2;  // Вместимость магазина увеличивается на 2 патрона с каждым уровнем
			oxygenTankCapacityUpgrades[i] = 15f + i * 5f;  // Вместимость баллона с кислородом увеличивается на 5 единиц с каждым уровнем
			upgradeCosts[i] = 10 + i * 10;  // Стоимость апгрейда увеличивается на 10 единиц с каждым уровнем
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	// Публичные методы для повышения уровня апгрейдов
	public void UpgradeDamage()
	{
		if (currentDamageLevel < damageUpgrades.Length - 1 && playerInventory.moneyCount >= upgradeCosts[currentDamageLevel])
		{
			playerInventory.moneyCount -= upgradeCosts[currentDamageLevel];
			currentDamageLevel++;
			damageLevel.text = "Lv: " + (currentDamageLevel + 1);
			damageUpgradeCosts.text = upgradeCosts[currentDamageLevel] + "$";
			pistol = FindInactiveObjectByName("GunController").transform.GetChild(0).GetComponent<Pistol>();
			pistol.bulletCurrentDamage = GetDamageUpgrade(currentDamageLevel);
			playerInventory.UpdateInventory();
			Debug.Log("Damage upgraded to level: " + currentDamageLevel);
			Debug.Log("New damage: " + GetDamageUpgrade(currentDamageLevel));
		}
		else
		{
			Debug.Log("Not enough money or max damage level reached.");
		}
	}

	public void UpgradeReloadSpeed()
	{
		if (currentReloadSpeedLevel < reloadSpeedUpgrades.Length - 1 && playerInventory.moneyCount >= upgradeCosts[currentReloadSpeedLevel])
		{
			playerInventory.moneyCount -= upgradeCosts[currentReloadSpeedLevel];
			currentReloadSpeedLevel++;
			reloadLevel.text = "Lv: " + (currentReloadSpeedLevel + 1);
			reloadUpgradeCosts.text = upgradeCosts[currentReloadSpeedLevel] + "$";
			pistol = FindInactiveObjectByName("GunController").transform.GetChild(0).GetComponent<Pistol>();
			pistol.reloadTime = GetReloadSpeedUpgrade(currentReloadSpeedLevel);
			playerInventory.UpdateInventory();
			Debug.Log("Reload speed upgraded to level: " + currentReloadSpeedLevel);
			Debug.Log("New reload speed: " + GetReloadSpeedUpgrade(currentReloadSpeedLevel));
		}
		else
		{
			Debug.Log("Not enough money or max reload speed level reached.");
		}
	}

	public void UpgradeMagazineCapacity()
	{
		if (currentMagazineCapacityLevel < magazineCapacityUpgrades.Length - 1 && playerInventory.moneyCount >= upgradeCosts[currentMagazineCapacityLevel])
		{
			playerInventory.moneyCount -= upgradeCosts[currentMagazineCapacityLevel];
			currentMagazineCapacityLevel++;
			magLevel.text = "Lv: " + (currentMagazineCapacityLevel + 1);
			magUpgradeCosts.text = upgradeCosts[currentMagazineCapacityLevel] + "$";
			pistol = FindInactiveObjectByName("GunController").transform.GetChild(0).GetComponent<Pistol>();
			pistol.maxShotsBeforeReload = GetMagazineCapacityUpgrade(currentMagazineCapacityLevel);
			playerInventory.UpdateInventory();
			Debug.Log("Magazine capacity upgraded to level: " + currentMagazineCapacityLevel);
			Debug.Log("New magazine capacity: " + GetMagazineCapacityUpgrade(currentMagazineCapacityLevel));
		}
		else
		{
			Debug.Log("Not enough money or max magazine capacity level reached.");
		}
	}

	public void UpgradeOxygenTankCapacity()
	{
		if (currentOxygenTankCapacityLevel < oxygenTankCapacityUpgrades.Length - 1 && playerInventory.moneyCount >= upgradeCosts[currentOxygenTankCapacityLevel])
		{
			playerInventory.moneyCount -= upgradeCosts[currentOxygenTankCapacityLevel];
			currentOxygenTankCapacityLevel++;
			oxygenLevel.text = "Lv: " + (currentOxygenTankCapacityLevel + 1);
			oxygenUpgradeCosts.text = upgradeCosts[currentOxygenTankCapacityLevel] + "$";
			playerOxygen = GameObject.Find("Player").GetComponent<PlayerO2>();
			playerOxygen.maxOxygen = GetOxygenTankCapacityUpgrade(currentOxygenTankCapacityLevel);
			playerInventory.UpdateInventory();
			Debug.Log("Oxygen tank capacity upgraded to level: " + currentOxygenTankCapacityLevel);
			Debug.Log("New oxygen tank capacity: " + GetOxygenTankCapacityUpgrade(currentOxygenTankCapacityLevel));
		}
		else
		{
			Debug.Log("Not enough money or max oxygen tank capacity level reached.");
		}
	}

	// Методы для получения значений апгрейдов
	public int GetDamageUpgrade(int level)
	{
		if (level < 0 || level >= damageUpgrades.Length)
			throw new System.ArgumentOutOfRangeException("Level out of range");
		return damageUpgrades[level];
	}

	public float GetReloadSpeedUpgrade(int level)
	{
		if (level < 0 || level >= reloadSpeedUpgrades.Length)
			throw new System.ArgumentOutOfRangeException("Level out of range");
		return reloadSpeedUpgrades[level];
	}

	public int GetMagazineCapacityUpgrade(int level)
	{
		if (level < 0 || level >= magazineCapacityUpgrades.Length)
			throw new System.ArgumentOutOfRangeException("Level out of range");
		return magazineCapacityUpgrades[level];
	}

	public float GetOxygenTankCapacityUpgrade(int level)
	{
		if (level < 0 || level >= oxygenTankCapacityUpgrades.Length)
			throw new System.ArgumentOutOfRangeException("Level out of range");
		return oxygenTankCapacityUpgrades[level];
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