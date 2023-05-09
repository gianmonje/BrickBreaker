using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class LevelMap : UIScreen<LevelMap> {

    protected override void Initialize() {
        Level level1 = new Level(1, "Level1");
        level1.UnlockLevel();
    }

    [Button("Unlock All Levels"), BoxGroup("NoTitle", false)]
    public void UnlockAllLevels() {
        for (int i = 0; i < LevelConfig.Instance.levelCollection.Length; i++) {
            Level level = LevelConfig.Instance.levelCollection[i];
            LevelConfig.Instance.UnlockLevel(level);
        }

    }
}