using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private Card m_cardPrefab;
    [SerializeField] private Transform m_grid;
    [SerializeField] private Sprite[] m_sprites;

    private Card m_fristSelectedCard;

    private List<Card> m_activeCards;

    private void Start()
    {
        m_activeCards = new();
        List<int> spriteIndex = new();

        for (int i = 0; i < m_sprites.Length; i++)
        {
            spriteIndex.Add(i);
            spriteIndex.Add(i);
        }

        for (int i = 0; i < spriteIndex.Count; i++)
        {
            int random = Random.Range(0, spriteIndex.Count);
            int temp = spriteIndex[i];
            spriteIndex[i] = spriteIndex[random];
            spriteIndex[random] = temp;
        }

        for (int i = 0; i < spriteIndex.Count; i++)
        {
            Card card = Instantiate(m_cardPrefab, m_grid);
            int index = spriteIndex[i];
            card.Init(index, m_sprites[index], OnCardSelected);
        }
    }

    private void OnCardSelected(Card card)
    {
        if (!card.IsCardActiveAndSelected())
        {
            card.Show();
            if (m_fristSelectedCard == null)
            {
                m_fristSelectedCard = card;
                return;
            }

            if (card.Id == m_fristSelectedCard.Id)
            {

            }
            else
            {
                card.Hide();
                m_fristSelectedCard.Hide();
            }

            m_fristSelectedCard = null;

        }
    }
}
