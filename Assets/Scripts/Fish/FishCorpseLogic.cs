using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishType
{
	SmallestFish,
	SmallFish,
	NormalFish,
	BigFish,
	Swordfish,
	Shark
}

public class FishCorpseLogic : MonoBehaviour
{
	public FishType fishType;
	public bool isScattered = false;
	private BoxCollider2D boxCollider;

	private float blinkShortInterval = .04f;
	private float blinkLongInterval = .1f;
	private bool isBlinking = false;
	private GameObject model;

	public float colliderEnableDelay = .5f;
	public float standartDestoyTimer = 4f;
	public float scatterDestroyTimer = 3f;

	private void Awake()
	{
		boxCollider = GetComponent<BoxCollider2D>();
		model = transform.GetChild(0).gameObject;
	}

	private void Start()
	{
		// Запускаем корутину для включения BoxCollider2D через полсекунды
		StartCoroutine(EnableColliderAfterDelay(colliderEnableDelay));

		if (!isScattered)
		{
			StartCoroutine(StandartDestroyTimer(standartDestoyTimer));
			Debug.Log("Not scattered");
		}
		else
		{
			Debug.Log("Scattered");
			StartCoroutine(ScatterDestroyTimer(scatterDestroyTimer));
		}
	}

	private IEnumerator EnableColliderAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		if (boxCollider != null)
		{
			boxCollider.enabled = true;
		}
	}

	private IEnumerator StandartDestroyTimer(float delay)
	{
		yield return new WaitForSeconds(delay);
		StartCoroutine(ScatterDestroyTimer(scatterDestroyTimer));
	}

	private IEnumerator ScatterDestroyTimer(float delay)
	{
		StartBlinking();
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}


	public void StartBlinking()
	{
		if (!isBlinking)
		{
			// Запуск корутины мигания
			StartCoroutine(BlinkRoutine());
			isBlinking = true;
		}
	}

	public void StopBlinking()
	{
		if (isBlinking)
		{
			// Остановка мигания, если оно уже запущено
			StopCoroutine(BlinkRoutine());
			model.SetActive(true); // Убедиться, что объект видим после остановки мигания
			isBlinking = false;
		}
	}

	IEnumerator BlinkRoutine()
	{
		while (true)
		{
			// Выключить модель
			model.SetActive(false);
			// Ждать некоторое время
			yield return new WaitForSeconds(blinkShortInterval);
			// Включить модель
			model.SetActive(true);
			// Ждать снова
			yield return new WaitForSeconds(blinkLongInterval);
		}
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		// Проверяем, столкнулись ли с игроком
		if (other.gameObject.tag == "Player")
		{
			// Получаем компонент инвентаря игрока
			PlayerInventory playerInventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();

			// Добавляем соответствующую рыбу в инвентарь игрока в зависимости от выбранного типа рыбы
			switch (fishType)
			{
				case FishType.SmallestFish:
					playerInventory.smallestFishAvailable++;
					break;
				case FishType.SmallFish:
					playerInventory.smallFishAvailable++;
					break;
				case FishType.NormalFish:
					playerInventory.normalFishAvailable++;
					break;
				case FishType.BigFish:
					playerInventory.bigFishAvailable++;
					break;
				case FishType.Swordfish:
					playerInventory.swordFishAvailable++;
					break;
				case FishType.Shark:
					playerInventory.sharkAvailable++;
					break;
				default:
					Debug.LogWarning("Unhandled fish type: " + fishType);
					break;
			}

			playerInventory.UpdateInventory();

			// Уничтожаем рыбу после того, как она добавлена в инвентарь игрока
			Destroy(gameObject);
		}
	}
}