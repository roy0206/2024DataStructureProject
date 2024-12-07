using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LevelData
{
    public int number;
    public string levelId;
    public bool isCleared;
    public string sceneName;
}

public class OutGameManager : MonoBehaviour, ISavable
{
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    public List<LevelData> levels;

    public void LoadData(Database data)
    {
        foreach(KeyValuePair<string, bool> item in data.stage_isCleardPair)
        {
            var level = levels.Find(l => l.levelId == item.Key);
            if (level == null) continue;
            level.isCleared = item.Value;
        }
    }

    public void SaveData(ref Database data)
    {
        data.stage_isCleardPair.Clear();
        foreach(var level in levels)
        {
            data.stage_isCleardPair[level.levelId] = level.isCleared;
        }
    }

    public void LoadGame(string id)
    {
        SceneManager.LoadScene(levels.Find(i => i.levelId == id).sceneName);
    }

}
