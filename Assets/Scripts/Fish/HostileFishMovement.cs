using System.Collections;
using UnityEngine;

public class HostileFishMovement : MonoBehaviour
{
	public float moveSpeedX = 2f; // �������� �������� ���� �� X
	public float moveSpeedY = 1f; // �������� �������� ���� �� Y
	public float lungeSpeed = 5f; // �������� ������
	public float lungeDuration = 0.5f; // ������������ ������
	public float cooldownDuration = 1f; // ����� ����� ��������
	public float lungeDistance = 5f; // ���������� �� ������ ��� ������ ������
	public LayerMask levelBoundaryMask; // ����� ��� ����������� �������� ������
	public Transform player; // ������ �� ������

	private Rigidbody2D rb;
	private float leftBound;
	private float rightBound;

	private bool isMovingRight;
	private bool isLunging;
	private bool isOnCooldown;

	public int fishSize = 1;
	public float smoothTime = 0.3f;

	private Vector2 targetVelocityX;
	private float targetX;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		// ���������� ������� �������� ���� � ������� ���������
		CalculateLevelBounds();

		player = GameObject.Find("Player").transform;

		// �������� �������� � ��������� �������
		isMovingRight = Random.Range(0, 2) == 0;

		targetVelocityX = new Vector2(isMovingRight ? moveSpeedX : -moveSpeedX, 0);
	}

	private void Update()
	{
		if (!isLunging && !isOnCooldown)
		{
			float distanceToPlayer = Vector2.Distance(transform.position, player.position);

			if (distanceToPlayer <= lungeDistance)
			{
				StartCoroutine(LungeAttack());
			}
			else
			{
				MoveHorizontally();
				MoveVertically();
			}
		}

		Debug.Log("IsMovingRight " + isMovingRight + ", Speed " + targetVelocityX + ", Target X is " + targetX);
	}

	/*private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "LeftBound")
		{
			TargetChange();
		}
		else if (other.gameObject.name == "RightBound")
		{
			TargetChange();
		}
	}*/

	private IEnumerator LungeAttack()
	{
		isLunging = true;

		// ����������� �������� � ������
		Vector2 predictedPosition = PredictPlayerPosition();
		Vector2 directionToPlayer = (predictedPosition - (Vector2)transform.position).normalized;
		Vector2 lungeVelocity = directionToPlayer * lungeSpeed;

		float elapsedTime = 0f;

		while (elapsedTime < lungeDuration)
		{
			rb.velocity = Vector2.Lerp(lungeVelocity, Vector2.zero, elapsedTime / lungeDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		rb.velocity = Vector2.zero;

		// ���������� ������ ������ ����� ������
		//CalculateLevelBounds();

		isLunging = false;
		isOnCooldown = true;
		yield return new WaitForSeconds(cooldownDuration);
		isOnCooldown = false;
	}

	private Vector2 PredictPlayerPosition()
	{
		Vector2 playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
		float playerSpeed = playerVelocity.magnitude;
		Vector2 playerDirection = playerVelocity.normalized;

		// �����, �� ������� ���� ��������� ������
		float timeToReachPlayer = Vector2.Distance(transform.position, player.position) / lungeSpeed;

		// ������������� ������� ������
		Vector2 predictedPosition = (Vector2)player.position + playerDirection * playerSpeed * timeToReachPlayer;

		return predictedPosition;
	}

	private void MoveHorizontally()
	{
		// ��������� ����������� �������� � ���������� ������� �����
		targetX = isMovingRight ? rightBound : leftBound;

		// ���� ���� �������� ������� �����, ������ ����������� ��������
		if ((isMovingRight && transform.position.x >= rightBound) || (!isMovingRight && transform.position.x <= leftBound))
		{
			TargetChange();
		}

		// ������ ���������� ������� �������� � ������� � �������������� �������� ������������
		rb.velocity = new Vector2(Vector2.Lerp(new Vector2(rb.velocity.x, 0), targetVelocityX, Time.deltaTime * smoothTime).x, rb.velocity.y);
	}

	void TargetChange()
	{
		isMovingRight = !isMovingRight;
		// ������������� ����� ������� ��������
		targetVelocityX = new Vector2(isMovingRight ? moveSpeedX : -moveSpeedX, 0);
	}

	private void MoveVertically()
	{
		if (player == null)
			return;

		// ����������� �������� � ������
		Vector2 directionToPlayer = (player.position - transform.position).normalized;

		// ������� �������� �� Y ���������� � ������
		Vector2 targetVelocityY = new Vector2(rb.velocity.x, directionToPlayer.y * moveSpeedY);

		// ������ ���������� ������� �������� � ������� � �������������� �������� ������������
		rb.velocity = new Vector2(rb.velocity.x, Vector2.Lerp(new Vector2(0, rb.velocity.y), targetVelocityY, Time.deltaTime * smoothTime).y);
	}

	private void CalculateLevelBounds()
	{
		// ���������� ����� ������� ������
		RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, levelBoundaryMask);
		if (leftHit.collider != null)
		{
			leftBound = leftHit.point.x + fishSize * 2;
			Debug.Log("left bound found " + leftBound);
		}
		else
		{
			leftBound = transform.position.x; // ��������� ��������� ��������, ���� ����� ������� �� ����������
			Debug.Log("left bound not found " + leftBound);
		}

		// ���������� ������ ������� ������
		RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, levelBoundaryMask);
		if (rightHit.collider != null)
		{
			rightBound = rightHit.point.x - fishSize * 2;
			Debug.Log("right bound found " + rightBound);
		}
		else
		{
			rightBound = transform.position.x; // ��������� ��������� ��������, ���� ������ ������� �� ����������
			Debug.Log("right bound not found " + rightBound);
		}
	}
}