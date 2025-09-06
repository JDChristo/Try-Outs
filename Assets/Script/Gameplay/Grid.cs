using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private readonly Vector2 r_center = Vector2.one / 2;

    [Header("Layout Settings")]
    [SerializeField] private Vector2 m_cardSpacing = new Vector2(10f, 10f);
    [SerializeField] private RectTransform m_parent;

    public void PlaceInLayout(int rows, int columns, List<Card> cards)
    {
        int totalCells = rows * columns;
        if (cards == null || cards.Count < totalCells)
        {
            Debug.LogWarning("Not enough cards to fill the grid!");
            return;
        }

        // Calculate available area inside parent
        float availableWidth = m_parent.rect.width;
        float availableHeight = m_parent.rect.height;

        // Calculate optimal cell size
        float cellWidth = (availableWidth - (m_cardSpacing.x * (columns - 1))) / columns;
        float cellHeight = (availableHeight - (m_cardSpacing.y * (rows - 1))) / rows;
        float cellSize = Mathf.Min(cellWidth, cellHeight);
        Vector2 finalSize = new Vector2(cellSize, cellSize);

        // Calculate total grid dimensions
        float gridWidth = (columns * cellSize) + ((columns - 1) * m_cardSpacing.x);
        float gridHeight = (rows * cellSize) + ((rows - 1) * m_cardSpacing.y);

        // Calculate anchor offset
        float startX = -availableWidth * 0.5f + (availableWidth - gridWidth) * 0.5f + (cellSize * 0.5f);
        float startY = availableHeight * 0.5f - (availableHeight - gridHeight) * 0.5f - (cellSize * 0.5f);

        // Place each card
        for (int i = 0; i < totalCells; i++)
        {
            int rowIndex = i / columns;
            int colIndex = i % columns;

            float x = startX + colIndex * (cellSize + m_cardSpacing.x);
            float y = startY - rowIndex * (cellSize + m_cardSpacing.y);

            cards[i].SetAnchoredPosition(finalSize, new Vector2(x, y));
        }
    }
}
