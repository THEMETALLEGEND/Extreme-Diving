using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
	private Button button;
	private PlayerInventory playerInventory;

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
		playerInventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
		playerInventory.ReloadScene();

	}

	// Update is called once per frame
	void Update()
	{

	}
}