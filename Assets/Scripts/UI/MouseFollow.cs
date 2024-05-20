using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        // �������� RectTransform ����������
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // �������� ������� ����
        Vector2 mousePosition = Input.mousePosition;

        // ����������� ������� ���� �� �������� ��������� � ���������� Canvas
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            mousePosition,
            null,
            out anchoredPosition);

        // ��������� ������� RectTransform
        rectTransform.anchoredPosition = anchoredPosition;
    }
}