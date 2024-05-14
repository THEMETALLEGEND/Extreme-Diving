using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float lifeTime = 2f;
	public string[] destroyOnCollisionTags = { "Enemy", "Obstacle" };

	private void Start()
	{
		// ���������� ���� �� ���������� ��������� �������
		Destroy(gameObject, lifeTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ��������� ������������ � ������, �� ������� ������ ���� ���������� ����
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