using UnityEngine;

public class NeutralFishMovement : MonoBehaviour
{
	public float moveSpeedX = 2f; // Скорость движения рыбы
	public float moveSpeedY = 1f;
	public LayerMask levelBoundaryMask; // Маска для определения пределов уровня

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

		// Определяем границы движения рыбы с помощью рейкастов
		CalculateLevelBounds();

		GetVerticalPoints();

		// Начинаем движение в случайную сторону
		isMovingRight = Random.Range(0, 2) == 0;
		isMovingUp = Random.Range(0, 2) == 0;

		targetVelocityX = new Vector2(isMovingRight ? moveSpeedX : -moveSpeedX, 0);
		targetVelocityY = new Vector2(0, isMovingUp ? moveSpeedY : -moveSpeedY);

	}

	private void GetVerticalPoints()
	{
		// Получаем текущую позицию объекта
		Vector3 currentPosition = transform.position;
		float offset = Random.Range(0.2f, .5f);

		// Создаем точку выше объекта
		Vector3 pointAbove = new Vector3(currentPosition.x, currentPosition.y + offset, currentPosition.z);
		pointUp = pointAbove.y;

		// Создаем точку ниже объекта
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
		// Проверяем направление движения и определяем целевую точку
		float targetX = isMovingRight ? rightBound : leftBound;

		// Если рыба достигла целевой точки, меняем направление движения
		if (Mathf.Abs(transform.position.x - targetX) <= 0.1f)
		{
			isMovingRight = !isMovingRight;
			// Устанавливаем новую целевую скорость
			targetVelocityX = new Vector2(isMovingRight ? moveSpeedX : -moveSpeedX, 0);
		}

		// Плавно приближаем текущую скорость к целевой с использованием линейной интерполяции
		rb.velocity = Vector2.Lerp(rb.velocity, targetVelocityX, Time.deltaTime * smoothTime);
	}

	private void MoveVertically()
	{
		// Проверяем направление движения и определяем целевую точку
		float targetY = isMovingUp ? pointUp : pointDown;

		// Если рыба достигла целевой точки, меняем направление движения
		if (Mathf.Abs(transform.position.y - targetY) <= 0.1f)
		{
			isMovingUp = !isMovingUp;
			// Устанавливаем новую целевую скорость
			targetVelocityY = new Vector2(0, isMovingUp ? moveSpeedY : -moveSpeedY);
		}

		// Плавно приближаем текущую скорость к целевой с использованием линейной интерполяции
		rb.velocity = Vector2.Lerp(rb.velocity, targetVelocityY, Time.deltaTime * smoothTime);
	}

	private void CalculateLevelBounds()
	{
		// Определяем левую границу уровня
		RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, levelBoundaryMask);
		if (leftHit.collider != null)
		{
			leftBound = leftHit.point.x + fishSize * 2;
		}
		else
		{
			leftBound = transform.position.x - 10f; // Установим дефолтное значение, если левая граница не определена
		}

		// Определяем правую границу уровня
		RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, levelBoundaryMask);
		if (rightHit.collider != null)
		{
			rightBound = rightHit.point.x - fishSize * 2;
		}
		else
		{
			rightBound = transform.position.x + 10f; // Установим дефолтное значение, если правая граница не определена
		}
	}
}