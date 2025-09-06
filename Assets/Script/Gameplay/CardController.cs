using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Card m_cardPrefab;
    [SerializeField] private Transform m_grid;
    [SerializeField] private Sprite[] m_sprites;

    [Header("Timings")]
    [SerializeField] private float m_hideAfter = 1.5f;
    [SerializeField] private float m_mismatchHideDelay = 0.5f;

    private Card m_firstSelectedCard;
    private List<Card> m_activeCards;

    private void Start()
    {
        m_activeCards = new List<Card>();

        List<int> spriteIndices = GenerateSpriteIndices();
        ShuffleCards(spriteIndices);
        SpawnCards(spriteIndices);
        StartCoroutine(HideAllCards());
    }

    private List<int> GenerateSpriteIndices()
    {
        List<int> spriteIndices = new List<int>();

        for (int i = 0; i < m_sprites.Length; i++)
        {
            spriteIndices.Add(i);
            spriteIndices.Add(i);
        }

        return spriteIndices;
    }

    private void ShuffleCards(List<int> spriteIndices)
    {
        for (int i = 0; i < spriteIndices.Count; i++)
        {
            int randomIndex = Random.Range(i, spriteIndices.Count);
            int temp = spriteIndices[i];
            spriteIndices[i] = spriteIndices[randomIndex];
            spriteIndices[randomIndex] = temp;
        }
    }

    private void SpawnCards(List<int> spriteIndices)
    {
        for (int i = 0; i < spriteIndices.Count; i++)
        {
            int index = spriteIndices[i];
            Card card = Instantiate(m_cardPrefab, m_grid);
            card.Init(index, m_sprites[index], OnCardSelected);
            card.Show(null); // show front initially
            m_activeCards.Add(card);
        }
    }

    private IEnumerator HideAllCards()
    {
        yield return new WaitForSeconds(m_hideAfter);

        for (int i = 0; i < m_activeCards.Count; i++)
        {
            m_activeCards[i].Hide();
        }
    }

    private void OnCardSelected(Card card)
    {
        if (!card.IsCardActive())
            return;

        card.Show(() => ValidateMatch(card));
    }

    private void ValidateMatch(Card card)
    {
        if (m_firstSelectedCard == null)
        {
            m_firstSelectedCard = card;
            return;
        }

        if (card.Id == m_firstSelectedCard.Id)
        {
            card.Matched();
            m_firstSelectedCard.Matched();
        }
        else
        {
            StartCoroutine(HideMismatchedCards(card, m_firstSelectedCard));
        }

        m_firstSelectedCard = null;
    }

    private IEnumerator HideMismatchedCards(Card cardA, Card cardB)
    {
        yield return new WaitForSeconds(m_mismatchHideDelay);
        cardA.Hide();
        cardB.Hide();
    }
}
