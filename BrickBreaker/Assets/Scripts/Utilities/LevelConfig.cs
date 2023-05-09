using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Custom/Level Config")]
public class LevelConfig : ScriptableObject {
    private static LevelConfig instance;
    public static LevelConfig Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<LevelConfig>("Configs/LevelConfig");
            }
            return instance;
        }
    }

    public Level[] levelCollection;

    #region EVENTS
    private Action<Level> UnlockedALevel;
    public event Action<Level> OnUnlockedALevel {
        add { UnlockedALevel += value; }
        remove { UnlockedALevel -= value; }
    }
    #endregion

    public Level GetLevel(int levelID) {
        for (int i = 0; i < levelCollection.Length; i++) {
            if (levelCollection[i].levelID == levelID) return levelCollection[i];
        }
        return null;
    }

    public void UnlockLevel(Level level) {
        for (int i = 0; i < levelCollection.Length; i++) {
            if (levelCollection[i].levelID == level.levelID) {
                level.UnlockLevel();
                if (UnlockedALevel != null) UnlockedALevel(level);
            }
        }
        Debug.Log(string.Format("No Level with LevelID: {0} LevelName: {1}", level.levelID, level.levelName));
    }

    public void UnlockNextLevel() {
        Level levelToUnlock = GetLevel(Game.SelectedLevel.levelID + 1);
        if (levelToUnlock != null) UnlockLevel(levelToUnlock);
    }
}
