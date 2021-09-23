using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Serializable]
    public struct LevelData
    {
        public string levelID;
        public PlayerData playerData;
        public MonsterData monsterData;
    }

    public static LevelManager instance;

    public List<LevelData> levels;
    [HideInInspector] public string levelIDToLoad;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLevelToLoad(string _levelID)
    {
        levelIDToLoad = _levelID;
    }
}
