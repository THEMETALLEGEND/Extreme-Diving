using UnityEngine;

public class Bullet : MonoBehaviour
{
	public int damage = 10; // Урон, наносимый пулей

	public float lifeTime = 2f;

	private void Start()
	{
		// Уничтожаем пулю по прошествии заданного времени
		Destroy(gameObject, lifeTime);
	}



	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("Enemy")) // Проверяем, является ли объект врагом
		{
			FishHealth fishHealth = coll.gameObject.GetComponent<FishHealth>(); // Получаем компонент HealthSystem с объекта врага
			if (fishHealth != null)
			{
				fishHealth.TakeDamage(damage); // Вызываем метод TakeDamage для нанесения урона объекту врага
			}
		}

		Destroy(gameObject); // Уничтожаем пулю после столкновения
	}
}