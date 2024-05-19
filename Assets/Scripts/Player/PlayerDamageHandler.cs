using System.Collections;
using UnityEngine;

public class PlayerDamageHandler : MonoBehaviour
{
	public float bounceForce = 10f; // Сила отскока игрока
	public float targetBounceDuration = 0.8f; // Длительность отскока
	public float scatterForce = 5f; // Сила, с которой рыба разбрасывается вокруг

	public bool isDead = false;

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
			if (IsInventoryEmpty())
			{
				isDead = true;
				ReceiveDamage(collision.gameObject, true);
			}
			else
			{
				ReceiveDamage(collision.gameObject, false);
				ScatterFish();
			}
		}
	}

	void ReceiveDamage(GameObject col, bool isDead)
	{
		// Получаем направление отскока
		Vector2 bounceDirection = transform.position - col.transform.position;
		bounceDirection.Normalize();

		// Применяем силу отскока к Rigidbody2D игрока
		rb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);

		// Отключаем скрипт передвижения игрока
		movementScript.enabled = false;

		if (!isDead)
			StartCoroutine(SlowDownAndEnableMovement(false));
		else
		{
			StartCoroutine(SlowDownAndEnableMovement(true));
			Die();
		}

	}

	IEnumerator SlowDownAndEnableMovement(bool isDead)
	{
		float elapsedTime = 0f;
		Vector2 initialVelocity = rb.velocity;
		float currentBounceDuration = isDead ? targetBounceDuration + .5f : targetBounceDuration;

		while (elapsedTime < currentBounceDuration)
		{
			float t = elapsedTime / currentBounceDuration;
			rb.velocity = Vector2.Lerp(initialVelocity, Vector2.zero, t);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		rb.velocity = Vector2.zero;
		movementScript.currentVelocity = Vector2.zero;
		movementScript.targetVelocity = Vector2.zero;

		if (!isDead)
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
				float scatterRandomForce = Random.Range(-4, 4);
				Vector2 scatterDirection = Random.insideUnitCircle.normalized;
				fishRb.AddForce(scatterDirection * (scatterForce + scatterRandomForce), ForceMode2D.Impulse);
			}
		}

		inventory.UpdateInventory();
	}

	bool IsInventoryEmpty()
	{
		return inventory.smallestFishAvailable == 0 &&
			   inventory.smallFishAvailable == 0 &&
			   inventory.normalFishAvailable == 0 &&
			   inventory.bigFishAvailable == 0 &&
			   inventory.swordFishAvailable == 0 &&
			   inventory.sharkAvailable == 0;
	}

	void Die()
	{
		// Здесь вы можете добавить логику для смерти игрока, например, проигрывание анимации смерти, перезапуск уровня и т.д.
		Debug.Log("Player has died.");
		// Пример перезапуска уровня:
		// UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}
}