using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        // Получаем RectTransform компонента
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Получаем позицию мыши
        Vector2 mousePosition = Input.mousePosition;

        // Преобразуем позицию мыши из экранных координат в координаты Canvas
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            mousePosition,
            null,
            out anchoredPosition);

        // Обновляем позицию RectTransform
        rectTransform.anchoredPosition = anchoredPosition;
    }
}