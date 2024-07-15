using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScreen : MonoBehaviour
{
	private GameObject player;

	private void Start()
	{
		player = GameObject.Find("Player");
	}

	private void Update()
	{
		transform.position = player.transform.position;
	}

	/*private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Spawner Hitbox")
		{
			Debug.Log("Spawner detected: " + other.gameObject.name); // Добавьте вывод для отладки

			Spawner spawner = other.GetComponentInParent<Spawner>();

			if (spawner != null)
			{
				Debug.Log("Spawner component found in parent: " + spawner.gameObject.name); // Добавьте вывод для отладки

				if (!spawner.hasSpawned)
				{
					spawner.SpawnFish();
				}
			}
			else
			{
				Debug.LogWarning("Spawner component not found in parent of: " + other.gameObject.name); // Добавьте вывод для отладки
			}
		}
	}*/
}