using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelLayout", menuName = "TryOuts/LevelLayout", order = 2)]
public class LevelLayout : ScriptableObject
{
    [SerializeField] private List<LevelData> m_levels;

    public IReadOnlyList<LevelData> Levels => m_levels;

    public LevelData GetLevelData(int index)
    {
        index = Mathf.Max(0, index);
        index %= m_levels.Count;
        return m_levels[index];
    }
}