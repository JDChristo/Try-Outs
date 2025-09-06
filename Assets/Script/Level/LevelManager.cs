using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelLayout m_levelLayout;

    private int m_currentLevelIndex;
    private LevelData m_currentLevel;

    public override void Init()
    {
        LoadLevel(2);
    }

    private void LoadLevel(int index)
    {
        m_currentLevelIndex = index;
        m_currentLevel = m_levelLayout.GetLevelData(m_currentLevelIndex);
    }

    public int GetRow() => m_currentLevel.Grid.Row;
    public int GetColumn() => m_currentLevel.Grid.Column;
    public (Sprite front, Sprite back) GetCardSprite() => (m_currentLevel.Cards.FrontFace, m_currentLevel.Cards.BackFace);
    public IReadOnlyList<Sprite> GetCardIcons() => m_currentLevel.Cards.Icons;
}
