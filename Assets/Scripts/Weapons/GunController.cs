using UnityEngine;

public class GunController : MonoBehaviour
{
	public Transform playerTransform;
	public float rotationSpeed = 5f;

	private void Update()
	{
		playerTransform = GameObject.Find("Player").transform;
		// —ледование за игроком
		transform.position = playerTransform.position;

		// ѕоворот в сторону мыши
		RotateTowardsMouse();
	}

	private void RotateTowardsMouse()
	{
		// ѕолучаем позицию мыши в мировых координатах
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0f;

		// Ќаходим направление курсора от позиции оружи€
		Vector3 direction = mousePosition - transform.position;

		// Ќаходим угол между текущим направлением оружи€ и направлением курсора
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		// —оздаем кватернион поворота в направлении курсора
		Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

		// ѕлавно поворачиваем оружие в сторону курсора
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}
}