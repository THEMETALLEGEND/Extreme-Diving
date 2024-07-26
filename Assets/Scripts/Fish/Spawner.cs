using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject fish;
	public bool hasSpawned = false;
	[HideInInspector] public GameObject spawnedFish;

	// Start is called before the first frame update
	void Start()
	{
		GameObject model = transform.GetChild(0).gameObject;
		model.SetActive(false);
	}

	public void SpawnFish()
	{
		GameObject model = transform.GetChild(0).gameObject;
		model.SetActive(false);
		spawnedFish = Instantiate(fish, transform.position, Quaternion.identity);
		hasSpawned = true;
	}

	public void DespawnFish()
	{
		Destroy(spawnedFish);
		hasSpawned = false;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Collided with " + other.gameObject);
		if (other.tag == "Spawner" && !hasSpawned)
		{
			SpawnFish();
		}
		//else
		//Debug.Log("collided with something else");
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Spawner" && hasSpawned)
		{
			DespawnFish();
		}
	}
}
