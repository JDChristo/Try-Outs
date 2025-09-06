using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private RectTransform m_rect;
    [SerializeField] private Image m_iconImage;
    [SerializeField] private BaseCardAnimator m_animator;

    private Action<Card> m_onCardClick;
    private bool m_isMatched;
    private bool m_isFlipped;
    private int m_id;

    public int Id => m_id;
    public bool IsMatched => m_isMatched;
    public bool IsFlipped => m_isFlipped;

    public void Init(int id, Sprite icon, Action<Card> onClick)
    {
        m_id = id;
        m_iconImage.sprite = icon;
        m_onCardClick = onClick;
        m_isMatched = false;
        m_isFlipped = false;
    }

    public void Matched()
    {
        m_animator.Matched();
        m_isMatched = true;
    }

    public void Show(Action onComplete)
    {
        if (m_isMatched) return;

        m_animator.Show(onComplete);
        m_isFlipped = true;
    }

    public void Hide()
    {
        if (m_isMatched) return;

        m_animator.Hide();
        m_isFlipped = false;
    }

    public bool IsCardActive()
    {
        return !m_isFlipped && !m_isMatched;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsCardActive())
        {
            m_onCardClick?.Invoke(this);
        }
    }

    public void SetAnchoredPosition(Vector2 size, Vector2 pos)
    {
        m_rect.sizeDelta = size;
        m_rect.anchoredPosition = pos;
    }
}
