using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;

public static class Game {
    static private Level selectedLevel;
    static public Level SelectedLevel {
        get {
            if (selectedLevel == null) {
                Level level = new Level(1, "Level1");
                return level;
            }
            return selectedLevel;
        }
        set {
            selectedLevel = value;
        }
    }
}

public class Paddle {
    public PaddleType paddleType;
    public Sprite smallSizeSprite;
    public Sprite mediumSizeSprite;
    public Sprite longSizeSprite;
}

[System.Serializable]
public class Level {
    public int levelID;
    public string levelName;
    public Sprite levelButtonSprite;
    public Sprite levelPreview;
    public Sprite levelBackground;

    [EnumPaging]
    public LockStatus lockStatus;

    public void UnlockLevel() {
        LevelLockStatus = LockStatus.Unlocked;
    }

    public LockStatus LevelLockStatus {
        get {
            lockStatus = EnumParser.ParseEnum<LockStatus>(PlayerPrefs.GetString(string.Format("Level_{0}", levelID), LockStatus.Locked.ToString()));
            return lockStatus;
        }
        set {
            lockStatus = value;
            PlayerPrefs.SetString(string.Format("Level_{0}", levelID), lockStatus.ToString());
            PlayerPrefs.Save();
        }
    }

    public Level(int levelID, string levelName) {
        this.levelID = levelID;
        this.levelName = levelName;
    }
}

[System.Serializable]
public class BrickHitpointSprite {
    public int hitpoint;
    public GameObject spriteGameObject;
}

[System.Serializable]
public class OnBallKilledEvent : UnityEvent<Ball> {
}

[Serializable]
public class BrickData {
    [VerticalGroup("Data"), LabelWidth(220)]
    public bool destroyWhenNoHealthLeft = true;
    [VerticalGroup("Data"), LabelWidth(220)]
    public bool isInvulnerable;
    [VerticalGroup("Data"), LabelWidth(220)]
    public int hitpoints;
    [VerticalGroup("Data"), LabelWidth(220)]
    public bool dontDestroyColliderOnZeroHealth;
    [VerticalGroup("Data"), LabelWidth(220)]
    public bool enableOnReset = true;

    [HideInInspector]
    public int currentHitpoints;

    public float HitPointPercentage {
        get {
            return ((float)currentHitpoints / (float)hitpoints);
        }
    }

    public void ResetHitpoints() {
        currentHitpoints = hitpoints;
    }

}