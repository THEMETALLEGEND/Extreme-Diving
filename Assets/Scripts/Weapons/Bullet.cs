using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float lifeTime = 2f;
	public string[] destroyOnCollisionTags = { "Enemy", "Obstacle" };

	private void Start()
	{
		// Уничтожаем пулю по прошествии заданного времени
		Destroy(gameObject, lifeTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Проверяем столкновение с тегами, по которым должна быть уничтожена пуля
		foreach (string tag in destroyOnCollisionTags)
		{
			if (collision.CompareTag(tag))
			{
				Destroy(gameObject);
				break;
			}
		}
	}
}