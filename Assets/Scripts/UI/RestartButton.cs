using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
	private Button button;

	// Start is called before the first frame update
	void Start()
	{
		// Получаем компонент Button, прикрепленный к этому объекту
		button = GetComponent<Button>();

		// Подписываем метод OnButtonClick на событие onClick кнопки
		button.onClick.AddListener(OnButtonClick);
	}

	// Метод, который будет вызван при нажатии кнопки
	void OnButtonClick()
	{
		// Ищем объект инвентаря игрока
		PlayerDamageHandler playerDamageHandler = GameObject.Find("Player").GetComponent<PlayerDamageHandler>();
		playerDamageHandler.DestroyAllEnemies();
		playerDamageHandler.movementScript.enabled = true;
		playerDamageHandler.isDead = false;

		GameObject player = GameObject.Find("Player");
		player.transform.position = new Vector3(0, 0, 0);

		GameObject gameOverScreen = GameObject.Find("Game Over Screen");
		gameOverScreen.SetActive(false);
	}
}