using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridConfig {
    [SerializeField] private int m_row;
    [SerializeField] private int m_col;
    public int Row => m_row;
    public int Column => m_col;
}

[Serializable]
public class CardAssets {
    [SerializeField] private Sprite m_frontFace;
    [SerializeField] private Sprite m_backFace;
    [SerializeField] private List<Sprite> m_icons;

    public Sprite FrontFace => m_frontFace;
    public Sprite BackFace => m_backFace;
    public IReadOnlyList<Sprite> Icons => m_icons;
}

[Serializable]
public class LevelData {
    [SerializeField] private GridConfig m_grid;
    [SerializeField] private CardAssets m_cards;
    
    public GridConfig Grid => m_grid;
    public CardAssets Cards => m_cards;
}
