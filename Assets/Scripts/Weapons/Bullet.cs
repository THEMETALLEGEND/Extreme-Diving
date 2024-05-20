using UnityEngine;

public class Bullet : MonoBehaviour
{
	public int damage = 10; // ����, ��������� �����

	public float lifeTime = 2f;

	private void Start()
	{
		// ���������� ���� �� ���������� ��������� �������
		Destroy(gameObject, lifeTime);
	}



	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("Enemy")) // ���������, �������� �� ������ ������
		{
			FishHealth fishHealth = coll.gameObject.GetComponent<FishHealth>(); // �������� ��������� HealthSystem � ������� �����
			if (fishHealth != null)
			{
				fishHealth.TakeDamage(damage); // �������� ����� TakeDamage ��� ��������� ����� ������� �����
			}
		}

		Destroy(gameObject); // ���������� ���� ����� ������������
	}
}