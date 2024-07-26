using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public string fishTag; // Tag to identify which fish to spawn
	private GameObject spawnedFish = null;
	public bool hasSpawned = false;
	private ObjectPool objectPool;

	void Start()
	{
		GameObject model = transform.GetChild(0).gameObject;
		model.SetActive(false);
		objectPool = FindObjectOfType<ObjectPool>(); // Find the ObjectPool in the scene
	}

	public void SpawnFish()
	{
		if (objectPool != null)
		{
			GameObject model = transform.GetChild(0).gameObject;
			model.SetActive(false);
			spawnedFish = objectPool.GetObject(fishTag);
			spawnedFish.transform.position = transform.position;
			hasSpawned = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Collided with " + other.gameObject);
		if (other.tag == "Spawner" && !hasSpawned)
		{
			SpawnFish();
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("Stopped colliding with " + other.gameObject);
		if (other.tag == "Spawner" && hasSpawned)
		{
			if (objectPool != null)
			{
				objectPool.ReturnObject(spawnedFish);
				hasSpawned = false;
			}
		}
	}
}