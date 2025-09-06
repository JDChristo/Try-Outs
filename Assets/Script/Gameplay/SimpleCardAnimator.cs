using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SimpleCardAnimator : BaseCardAnimator
{
    [Header("Card Visuals")]
    [SerializeField] private Image m_cardImage;
    [SerializeField] private Image m_cardShadowImage;
    [SerializeField] private Image m_iconImage;
    [SerializeField] private Sprite m_backFaceSprite;
    [SerializeField] private Sprite m_frontFaceSprite;

    [Header("Flip Settings")]
    [SerializeField] private float m_moveHeight = 30f;
    [SerializeField] private float m_flipDuration = 0.4f;
    [SerializeField] private AnimationCurve m_flipAnimationCurve;


    [Header("Disable")]
    [SerializeField] private Color m_disabledColor = Color.gray;

    private Coroutine m_flipCoroutine;
    private Vector3 fromScale = Vector3.one;
    private Vector3 fromPos = Vector2.zero;
    private Vector3 fromShadowPos = Vector2.zero;

    void Start()
    {
        fromScale = transform.localScale;
        fromPos = m_cardImage.transform.localPosition;
        fromShadowPos = m_cardShadowImage.transform.localPosition;
    }

    public override void Hide(Action onComplete)
    {
        StartFlip(m_backFaceSprite, () =>
        {
            m_iconImage.gameObject.SetActive(false);
        }, onComplete);
    }

    public override void Show(Action onComplete = null)
    {
        StartFlip(m_frontFaceSprite, () =>
        {
            m_iconImage.gameObject.SetActive(true);
        }, onComplete);
    }

    public override void Matched()
    {
        if (m_cardImage != null)
        {
            m_cardImage.color = m_disabledColor;
        }

        // transform.localScale = fromScale;
        // m_cardImage.transform.localPosition = fromPos;
        // m_cardShadowImage.transform.localPosition = fromShadowPos;
    }

    private void StartFlip(Sprite finalSprite, Action onMid, Action onComplete)
    {
        if (m_flipCoroutine != null)
        {
            StopCoroutine(m_flipCoroutine);
        }

        m_flipCoroutine = StartCoroutine(FlipRoutine(finalSprite, onMid, () =>
        {
            onComplete?.Invoke();
            m_flipCoroutine = null;
        }));
    }

    private IEnumerator FlipRoutine(Sprite finalSprite, Action onMid, Action onComplete)
    {
        float halfDuration = m_flipDuration / 2f;

        Vector3 toScale = new Vector3(0f, 1f, 1f);

        Vector3 toPos = fromPos + Vector3.up * m_moveHeight;

        Vector3 toShadowPos = fromShadowPos + Vector3.down * m_moveHeight;

        yield return AnimateFlip(fromScale, toScale, fromPos, toPos, fromShadowPos, toShadowPos, halfDuration);

        if (m_cardImage != null)
        {
            m_cardImage.sprite = finalSprite;
        }

        onMid?.Invoke();

        yield return AnimateFlip(toScale, fromScale, toPos, fromPos, toShadowPos, fromShadowPos, halfDuration);
        onComplete?.Invoke();
    }

    private IEnumerator AnimateFlip(Vector3 from, Vector3 toScale, Vector3 fromPos, Vector3 toPos, Vector3 fromShadowPos, Vector3 toShadowPos, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float curveT = m_flipAnimationCurve.Evaluate(t);

            transform.localScale = Vector3.LerpUnclamped(from, toScale, curveT);
            m_cardImage.transform.localPosition = Vector3.LerpUnclamped(fromPos, toPos, curveT);
            m_cardShadowImage.transform.localPosition = Vector3.LerpUnclamped(fromShadowPos, toShadowPos, curveT);

            yield return null;
        }

        transform.localScale = toScale;
        m_cardImage.transform.localPosition = toPos;
        m_cardShadowImage.transform.localPosition = toShadowPos;
    }
}
