using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    private int _level;

    public LevelData (LevelCounter levelCounter)
    {
        _level = levelCounter.CurrentLevelNumber;
    }
}