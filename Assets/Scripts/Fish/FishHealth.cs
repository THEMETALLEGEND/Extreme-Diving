using UnityEngine;

public class FishHealth : MonoBehaviour
{
	public int maxHealth = 100; // ћаксимальное количество здоровь€ агента
	private int currentHealth; // “екущее количество здоровь€ агента
	public GameObject corpsePrefab;

	private void Start()
	{
		currentHealth = maxHealth; // ”станавливаем текущее здоровье в максимальное значение при старте
	}

	// ћетод получени€ урона
	public void TakeDamage(int damage)
	{
		currentHealth -= damage; // ”меньшаем текущее здоровье на количество полученного урона

		if (currentHealth <= 0)
		{
			Die(); // ≈сли здоровье достигло нул€ или меньше, вызываем метод смерти
		}
	}

	// ћетод смерти
	private void Die()
	{
		if (corpsePrefab != null)
			Instantiate(corpsePrefab, transform.position, transform.rotation);

		Destroy(gameObject);
	}
}