using UnityEngine;

public class NeutralFish : MonoBehaviour
{
	public float moveSpeed = 2f; // �������� �������� ����
	public LayerMask levelBoundaryMask; // ����� ��� ����������� �������� ������

	private float leftBound;
	private float rightBound;
	private bool isMovingRight;
	public int fishSize = 1;

	private void Start()
	{
		// ���������� ������� �������� ���� � ������� ���������
		CalculateLevelBounds();

		// �������� �������� � ��������� �������
		isMovingRight = Random.Range(0, 2) == 0;
	}

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		// ��������� ����������� �������� � ���������� ������� �����
		float targetX = isMovingRight ? rightBound : leftBound;

		// ���������� ���� � ������� �����
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);

		// ���� ���� �������� ������� �����, ������ ����������� ��������
		if (Mathf.Abs(transform.position.x - targetX) <= 0.1f)
		{
			isMovingRight = !isMovingRight;
		}
	}

	private void CalculateLevelBounds()
	{
		// ���������� ����� ������� ������
		RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, levelBoundaryMask);
		if (leftHit.collider != null)
		{
			leftBound = leftHit.point.x + fishSize;
		}
		else
		{
			leftBound = transform.position.x - 10f; // ��������� ��������� ��������, ���� ����� ������� �� ����������
		}

		// ���������� ������ ������� ������
		RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, levelBoundaryMask);
		if (rightHit.collider != null)
		{
			rightBound = rightHit.point.x - fishSize;
		}
		else
		{
			rightBound = transform.position.x + 10f; // ��������� ��������� ��������, ���� ������ ������� �� ����������
		}
	}
}