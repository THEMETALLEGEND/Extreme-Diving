using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
	private Button button;
	private PlayerInventory playerInventory;

	// Start is called before the first frame update
	void Start()
	{
		// �������� ��������� Button, ������������� � ����� �������
		button = GetComponent<Button>();

		// ����������� ����� OnButtonClick �� ������� onClick ������
		button.onClick.AddListener(OnButtonClick);
	}

	// �����, ������� ����� ������ ��� ������� ������
	void OnButtonClick()
	{
		// ���� ������ ��������� ������
		playerInventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
		playerInventory.ReloadScene();

	}

	// Update is called once per frame
	void Update()
	{

	}
}