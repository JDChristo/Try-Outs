using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SimpleCardAnimator : BaseCardAnimator
{
    [Header("Card Visuals")]
    [SerializeField] private Image m_cardImage;
    [SerializeField] private Image m_iconImage;
    [SerializeField] private Sprite m_backFaceSprite;
    [SerializeField] private Sprite m_frontFaceSprite;

    [Header("Flip Settings")]
    [SerializeField] private float m_flipDuration = 0.4f;
    [SerializeField] private AnimationCurve m_flipAnimationCurve;

    [Header("Disable")]
    [SerializeField] private Color m_disabledColor = Color.gray;

    private Coroutine m_flipCoroutine;

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
            m_cardImage.color = m_disabledColor;
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
        Transform cardTransform = m_cardImage.transform;

        yield return AnimateScale(cardTransform, Vector3.one, new Vector3(0f, 1f, 1f), halfDuration);

        if (m_cardImage != null)
        {
            m_cardImage.sprite = finalSprite;
        }

        onMid?.Invoke();

        yield return AnimateScale(cardTransform, new Vector3(0f, 1f, 1f), Vector3.one, halfDuration);

        onComplete?.Invoke();
    }

    private IEnumerator AnimateScale(Transform target, Vector3 from, Vector3 to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float curveT = m_flipAnimationCurve.Evaluate(t);

            if (target != null)
            {
                target.localScale = Vector3.LerpUnclamped(from, to, curveT);
            }

            yield return null;
        }

        if (target != null)
        {
            target.localScale = to;
        }
    }
}
