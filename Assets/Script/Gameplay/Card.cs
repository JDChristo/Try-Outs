using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform m_rect;
    [SerializeField] private Image m_image;
    [SerializeField] private Sprite m_backFaceSprite;

    private event Action<Card> m_onCardClick;

    private Sprite m_iconSprite;

    private bool m_isShown;
    private int m_id;

    public int Id => m_id;

    public void Init(int id, Sprite icon, Action<Card> m_onClick)
    {
        m_id = id;
        m_iconSprite = icon;

        m_onCardClick = m_onClick;
        Show();
        Invoke(nameof(Hide), 1f);
    }

    public void Show()
    {
        m_image.sprite = m_iconSprite;
        m_isShown = true;
    }

    public void Hide()
    {
        m_image.sprite = m_backFaceSprite;
        m_isShown = false;
    }

    public bool IsCardActiveAndSelected()
    {
        return m_isShown;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_onCardClick?.Invoke(this);
    }
}
