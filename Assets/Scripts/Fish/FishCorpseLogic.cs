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
		// ��������� �������� ��� ��������� BoxCollider2D ����� ����������
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
			// ������ �������� �������
			StartCoroutine(BlinkRoutine());
			isBlinking = true;
		}
	}

	public void StopBlinking()
	{
		if (isBlinking)
		{
			// ��������� �������, ���� ��� ��� ��������
			StopCoroutine(BlinkRoutine());
			model.SetActive(true); // ���������, ��� ������ ����� ����� ��������� �������
			isBlinking = false;
		}
	}

	IEnumerator BlinkRoutine()
	{
		while (true)
		{
			// ��������� ������
			model.SetActive(false);
			// ����� ��������� �����
			yield return new WaitForSeconds(blinkShortInterval);
			// �������� ������
			model.SetActive(true);
			// ����� �����
			yield return new WaitForSeconds(blinkLongInterval);
		}
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		// ���������, ����������� �� � �������
		if (other.gameObject.tag == "Player")
		{
			// �������� ��������� ��������� ������
			PlayerInventory playerInventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();

			// ��������� ��������������� ���� � ��������� ������ � ����������� �� ���������� ���� ����
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

			// ���������� ���� ����� ����, ��� ��� ��������� � ��������� ������
			Destroy(gameObject);
		}
	}
}