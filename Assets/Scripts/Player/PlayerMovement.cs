using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float swimSpeed = 7f;  // Пульсационное значение скорости
	public float interpolationAmount = .1f;

	private Rigidbody2D rb;
	private Vector2 moveInput;
	private Vector2 currentVelocity;
	private Vector2 targetVelocity;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		// Получаем ввод от игрока
		moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	}

	private void FixedUpdate()
	{
		// Устанавливаем целевую скорость в соответствии с вводом
		targetVelocity = moveInput.normalized * swimSpeed;

		// Плавно изменяем скорость к целевой скорости с помощью интерполяции
		currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, interpolationAmount);

		// Устанавливаем скорость Rigidbody
		rb.velocity = currentVelocity;
	}
}
