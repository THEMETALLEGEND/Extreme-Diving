using UnityEngine;

public class FishHealth : MonoBehaviour
{
	public int maxHealth = 100; // ������������ ���������� �������� ������
	private int currentHealth; // ������� ���������� �������� ������
	public GameObject corpsePrefab;

	private void Start()
	{
		currentHealth = maxHealth; // ������������� ������� �������� � ������������ �������� ��� ������
	}

	// ����� ��������� �����
	public void TakeDamage(int damage)
	{
		currentHealth -= damage; // ��������� ������� �������� �� ���������� ����������� �����

		if (currentHealth <= 0)
		{
			Die(); // ���� �������� �������� ���� ��� ������, �������� ����� ������
		}
	}

	// ����� ������
	private void Die()
	{
		if (corpsePrefab != null)
			Instantiate(corpsePrefab, transform.position, transform.rotation);

		Destroy(gameObject);
	}
}