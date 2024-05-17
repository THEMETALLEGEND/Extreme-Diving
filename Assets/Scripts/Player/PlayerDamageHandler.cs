using System.Collections;
using UnityEngine;

public class PlayerDamageHandler : MonoBehaviour
{
	public float bounceForce = 10f; // Сила отскока игрока
	public float bounceDuration = 0.8f; // Длительность отскока
	public float scatterForce = 5f; // Сила, с которой рыба разбрасывается вокруг

	private Rigidbody2D rb;
	private PlayerMovement movementScript;
	private PlayerInventory inventory;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		movementScript = GetComponent<PlayerMovement>();
		inventory = PlayerInventory.Instance;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			// Получаем направление отскока
			Vector2 bounceDirection = transform.position - collision.transform.position;
			bounceDirection.Normalize();

			// Применяем силу отскока к Rigidbody2D игрока
			rb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);

			// Отключаем скрипт передвижения игрока
			movementScript.enabled = false;

			// Запускаем корутину для замедления игрока и повторного включения скрипта через некоторое время
			StartCoroutine(SlowDownAndEnableMovement());

			// Разбрасываем рыбу из инвентаря
			ScatterFish();
		}
	}

	IEnumerator SlowDownAndEnableMovement()
	{
		float elapsedTime = 0f;
		Vector2 initialVelocity = rb.velocity;

		while (elapsedTime < bounceDuration)
		{
			float t = elapsedTime / bounceDuration;
			rb.velocity = Vector2.Lerp(initialVelocity, Vector2.zero, t);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		rb.velocity = Vector2.zero;
		movementScript.currentVelocity = Vector2.zero;
		movementScript.targetVelocity = Vector2.zero;
		movementScript.enabled = true;
	}

	void ScatterFish()
	{
		ScatterFishType(inventory.smallestFishPrefab, ref inventory.smallestFishAvailable);
		ScatterFishType(inventory.smallFishPrefab, ref inventory.smallFishAvailable);
		ScatterFishType(inventory.normalFishPrefab, ref inventory.normalFishAvailable);
		ScatterFishType(inventory.bigFishPrefab, ref inventory.bigFishAvailable);
		ScatterFishType(inventory.swordFishPrefab, ref inventory.swordFishAvailable);
		ScatterFishType(inventory.sharkPrefab, ref inventory.sharkAvailable);
	}

	void ScatterFishType(GameObject fishPrefab, ref int count)
	{
		if (fishPrefab == null || count <= 0)
		{
			return;
		}

		int fishToScatter = count;
		count = 0; // Сбрасываем количество рыбы в инвентаре

		for (int i = 0; i < fishToScatter; i++)
		{
			GameObject fish = Instantiate(fishPrefab, transform.position, Quaternion.identity);
			fish.GetComponent<FishCorpseLogic>().isScattered = true;
			Rigidbody2D fishRb = fish.GetComponent<Rigidbody2D>();
			if (fishRb != null)
			{
				Vector2 scatterDirection = Random.insideUnitCircle.normalized;
				fishRb.AddForce(scatterDirection * scatterForce, ForceMode2D.Impulse);
			}
		}
	}
}