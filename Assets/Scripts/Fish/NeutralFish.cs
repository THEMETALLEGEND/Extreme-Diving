using UnityEngine;

public class NeutralFish : MonoBehaviour
{
	public float moveSpeed = 2f; // Скорость движения рыбы
	public LayerMask levelBoundaryMask; // Маска для определения пределов уровня

	private float leftBound;
	private float rightBound;
	private bool isMovingRight;
	public int fishSize = 1;

	private void Start()
	{
		// Определяем границы движения рыбы с помощью рейкастов
		CalculateLevelBounds();

		// Начинаем движение в случайную сторону
		isMovingRight = Random.Range(0, 2) == 0;
	}

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		// Проверяем направление движения и определяем целевую точку
		float targetX = isMovingRight ? rightBound : leftBound;

		// Перемещаем рыбу к целевой точке
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);

		// Если рыба достигла целевой точки, меняем направление движения
		if (Mathf.Abs(transform.position.x - targetX) <= 0.1f)
		{
			isMovingRight = !isMovingRight;
		}
	}

	private void CalculateLevelBounds()
	{
		// Определяем левую границу уровня
		RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, levelBoundaryMask);
		if (leftHit.collider != null)
		{
			leftBound = leftHit.point.x + fishSize;
		}
		else
		{
			leftBound = transform.position.x - 10f; // Установим дефолтное значение, если левая граница не определена
		}

		// Определяем правую границу уровня
		RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, levelBoundaryMask);
		if (rightHit.collider != null)
		{
			rightBound = rightHit.point.x - fishSize;
		}
		else
		{
			rightBound = transform.position.x + 10f; // Установим дефолтное значение, если правая граница не определена
		}
	}
}