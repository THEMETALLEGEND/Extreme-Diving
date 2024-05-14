using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float swimSpeed = 7f;  // ������������� �������� ��������
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
		// �������� ���� �� ������
		moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	}

	private void FixedUpdate()
	{
		// ������������� ������� �������� � ������������ � ������
		targetVelocity = moveInput.normalized * swimSpeed;

		// ������ �������� �������� � ������� �������� � ������� ������������
		currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, interpolationAmount);

		// ������������� �������� Rigidbody
		rb.velocity = currentVelocity;
	}
}
