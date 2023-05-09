using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : Singleton<LevelsManager> {
    public Camera mainCam;
    public float defaultCamSize = 5.244444f;

    [BoxGroup("NoTitle", false)]
    public SpriteRenderer background;

    [BoxGroup("LEVEL", centerLabel: true)]
    public LevelController currentLevel;
    [BoxGroup("LEVEL")]
    public LevelController[] levelControllerCollection;

    [Button("Get All Levels"), BoxGroup("LEVEL")]
    public void GetAllLevels() {
        SetVisibilityChildren(true);
        levelControllerCollection = GetComponentsInChildren<LevelController>();
        SetVisibilityChildren(false);
    }

    [BoxGroup("DEBUG", centerLabel: true)]
    public bool isDebug;
    [ShowIf("isDebug"), BoxGroup("DEBUG")]
    public int debugLevelID;

    private Action<BrickData> BrickReducedHitpoints;

    private void Start() {
        for (int i = 0; i < levelControllerCollection.Length; i++) {
            levelControllerCollection[i].transform.position = new Vector2(0, -(defaultCamSize - mainCam.orthographicSize));
        }
    }

    public event Action<BrickData> OnBrickReducedHitpoints {
        add { BrickReducedHitpoints += value; }
        remove { BrickReducedHitpoints -= value; }
    }

    public void TriggerOnBrickReducedHitPoints(BrickData brickData) {
        if (BrickReducedHitpoints != null) BrickReducedHitpoints(brickData);
    }

    private void SetVisibilityChildren(bool isVisible) {
        foreach (Transform child in transform) {
            if (child.GetComponent<LevelController>() != null) child.gameObject.SetActive(isVisible);
        }
    }

    public void LoadSelectedLevel(Level level) {
        if (currentLevel != null) currentLevel.gameObject.SetActive(false);

        for (int i = 0; i < levelControllerCollection.Length; i++) {
            if (level.levelID == levelControllerCollection[i].levelID) {
                currentLevel = levelControllerCollection[i];
            }
        }

        if (currentLevel == null) return;
        UpdateBackground(level);
        currentLevel.gameObject.SetActive(true);
        currentLevel.ResetLevel();
    }

    private void UpdateBackground(Level level) {
        background.sprite = level.levelBackground;
    }
}
