using UnityEngine;

public class GunController : MonoBehaviour
{
	public Transform playerTransform;
	public float rotationSpeed = 5f;

	private void Update()
	{
		playerTransform = GameObject.Find("Player").transform;
		// ���������� �� �������
		transform.position = playerTransform.position;

		// ������� � ������� ����
		RotateTowardsMouse();
	}

	private void RotateTowardsMouse()
	{
		// �������� ������� ���� � ������� �����������
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0f;

		// ������� ����������� ������� �� ������� ������
		Vector3 direction = mousePosition - transform.position;

		// ������� ���� ����� ������� ������������ ������ � ������������ �������
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		// ������� ���������� �������� � ����������� �������
		Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

		// ������ ������������ ������ � ������� �������
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}
}