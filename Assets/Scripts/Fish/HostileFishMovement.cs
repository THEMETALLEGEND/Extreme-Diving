using System.Collections;
using UnityEngine;

public class HostileFishMovement : MonoBehaviour
{
	public float moveSpeedX = 2f; // Скорость движения рыбы по X
	public float moveSpeedY = 1f; // Скорость движения рыбы по Y
	public float lungeSpeed = 5f; // Скорость выпада
	public float lungeDuration = 0.5f; // Длительность выпада
	public float cooldownDuration = 1f; // Время между выпадами
	public float lungeDistance = 5f; // Расстояние до игрока для начала выпада
	public LayerMask levelBoundaryMask; // Маска для определения пределов уровня
	public Transform player; // Ссылка на игрока

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

		// Определяем границы движения рыбы с помощью рейкастов
		CalculateLevelBounds();

		player = GameObject.Find("Player").transform;

		// Начинаем движение в случайную сторону
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

		// Направление движения к игроку
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

		// Перерасчет границ уровня после выпада
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

		// Время, за которое рыба достигнет игрока
		float timeToReachPlayer = Vector2.Distance(transform.position, player.position) / lungeSpeed;

		// Предсказанная позиция игрока
		Vector2 predictedPosition = (Vector2)player.position + playerDirection * playerSpeed * timeToReachPlayer;

		return predictedPosition;
	}

	private void MoveHorizontally()
	{
		// Проверяем направление движения и определяем целевую точку
		targetX = isMovingRight ? rightBound : leftBound;

		// Если рыба достигла целевой точки, меняем направление движения
		if ((isMovingRight && transform.position.x >= rightBound) || (!isMovingRight && transform.position.x <= leftBound))
		{
			TargetChange();
		}

		// Плавно приближаем текущую скорость к целевой с использованием линейной интерполяции
		rb.velocity = new Vector2(Vector2.Lerp(new Vector2(rb.velocity.x, 0), targetVelocityX, Time.deltaTime * smoothTime).x, rb.velocity.y);
	}

	void TargetChange()
	{
		isMovingRight = !isMovingRight;
		// Устанавливаем новую целевую скорость
		targetVelocityX = new Vector2(isMovingRight ? moveSpeedX : -moveSpeedX, 0);
	}

	private void MoveVertically()
	{
		if (player == null)
			return;

		// Направление движения к игроку
		Vector2 directionToPlayer = (player.position - transform.position).normalized;

		// Целевая скорость по Y направлена к игроку
		Vector2 targetVelocityY = new Vector2(rb.velocity.x, directionToPlayer.y * moveSpeedY);

		// Плавно приближаем текущую скорость к целевой с использованием линейной интерполяции
		rb.velocity = new Vector2(rb.velocity.x, Vector2.Lerp(new Vector2(0, rb.velocity.y), targetVelocityY, Time.deltaTime * smoothTime).y);
	}

	private void CalculateLevelBounds()
	{
		// Определяем левую границу уровня
		RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, levelBoundaryMask);
		if (leftHit.collider != null)
		{
			leftBound = leftHit.point.x + fishSize * 2;
			Debug.Log("left bound found " + leftBound);
		}
		else
		{
			leftBound = transform.position.x; // Установим дефолтное значение, если левая граница не определена
			Debug.Log("left bound not found " + leftBound);
		}

		// Определяем правую границу уровня
		RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, levelBoundaryMask);
		if (rightHit.collider != null)
		{
			rightBound = rightHit.point.x - fishSize * 2;
			Debug.Log("right bound found " + rightBound);
		}
		else
		{
			rightBound = transform.position.x; // Установим дефолтное значение, если правая граница не определена
			Debug.Log("right bound not found " + rightBound);
		}
	}
}