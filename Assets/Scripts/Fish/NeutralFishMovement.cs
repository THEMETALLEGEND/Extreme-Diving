using UnityEngine;

public class NeutralFishMovement : MonoBehaviour
{
	public float moveSpeedX = 2f; // �������� �������� ����
	public float moveSpeedY = 1f;
	public LayerMask levelBoundaryMask; // ����� ��� ����������� �������� ������

	private Rigidbody2D rb;
	private float leftBound;
	private float rightBound;
	private float pointUp;
	private float pointDown;

	private bool isMovingRight;
	private bool isMovingUp;
	public int fishSize = 1;

	public float smoothTime = 0.3f;

	private Vector2 targetVelocityX;
	private Vector2 targetVelocityY;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		// ���������� ������� �������� ���� � ������� ���������
		CalculateLevelBounds();

		GetVerticalPoints();

		// �������� �������� � ��������� �������
		isMovingRight = Random.Range(0, 2) == 0;
		isMovingUp = Random.Range(0, 2) == 0;

		targetVelocityX = new Vector2(isMovingRight ? moveSpeedX : -moveSpeedX, 0);
		targetVelocityY = new Vector2(0, isMovingUp ? moveSpeedY : -moveSpeedY);

	}

	private void GetVerticalPoints()
	{
		// �������� ������� ������� �������
		Vector3 currentPosition = transform.position;
		float offset = Random.Range(0.2f, .5f);

		// ������� ����� ���� �������
		Vector3 pointAbove = new Vector3(currentPosition.x, currentPosition.y + offset, currentPosition.z);
		pointUp = pointAbove.y;

		// ������� ����� ���� �������
		Vector3 pointBelow = new Vector3(currentPosition.x, currentPosition.y - offset, currentPosition.z);
		pointDown = pointBelow.y;
	}

	private void Update()
	{
		MoveHorizontally();
		MoveVertically();
	}

	private void MoveHorizontally()
	{
		// ��������� ����������� �������� � ���������� ������� �����
		float targetX = isMovingRight ? rightBound : leftBound;

		// ���� ���� �������� ������� �����, ������ ����������� ��������
		if (Mathf.Abs(transform.position.x - targetX) <= 0.1f)
		{
			isMovingRight = !isMovingRight;
			// ������������� ����� ������� ��������
			targetVelocityX = new Vector2(isMovingRight ? moveSpeedX : -moveSpeedX, 0);
		}

		// ������ ���������� ������� �������� � ������� � �������������� �������� ������������
		rb.velocity = Vector2.Lerp(rb.velocity, targetVelocityX, Time.deltaTime * smoothTime);
	}

	private void MoveVertically()
	{
		// ��������� ����������� �������� � ���������� ������� �����
		float targetY = isMovingUp ? pointUp : pointDown;

		// ���� ���� �������� ������� �����, ������ ����������� ��������
		if (Mathf.Abs(transform.position.y - targetY) <= 0.1f)
		{
			isMovingUp = !isMovingUp;
			// ������������� ����� ������� ��������
			targetVelocityY = new Vector2(0, isMovingUp ? moveSpeedY : -moveSpeedY);
		}

		// ������ ���������� ������� �������� � ������� � �������������� �������� ������������
		rb.velocity = Vector2.Lerp(rb.velocity, targetVelocityY, Time.deltaTime * smoothTime);
	}

	private void CalculateLevelBounds()
	{
		// ���������� ����� ������� ������
		RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, levelBoundaryMask);
		if (leftHit.collider != null)
		{
			leftBound = leftHit.point.x + fishSize * 2;
		}
		else
		{
			leftBound = transform.position.x - 10f; // ��������� ��������� ��������, ���� ����� ������� �� ����������
		}

		// ���������� ������ ������� ������
		RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, levelBoundaryMask);
		if (rightHit.collider != null)
		{
			rightBound = rightHit.point.x - fishSize * 2;
		}
		else
		{
			rightBound = transform.position.x + 10f; // ��������� ��������� ��������, ���� ������ ������� �� ����������
		}
	}
}