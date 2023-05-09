using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
    [SerializeField]
    private int levelID;
    [SerializeField]
    private Image levelImage;
    private Level LevelSelected { get { return LevelConfig.Instance.GetLevel(levelID); } }

    private void Start() {
        if (levelImage.sprite == null) SetButtonImage();
        LevelConfig.Instance.OnUnlockedALevel += OnUnlockedALevel;
    }

    private void OnDestroy() {
        LevelConfig.Instance.OnUnlockedALevel -= OnUnlockedALevel;
    }

    private void OnUnlockedALevel(Level level) {
        Setup();
    }

    private void OnEnable() {
        Setup();
    }

    private void Setup() {
        transform.Find("LockImage").gameObject.SetActive(LevelSelected.LevelLockStatus == LockStatus.Locked);
    }

    [Button("Set Button Image"), BoxGroup("NoTitle", false)]
    public void SetButtonImage() {
        levelImage.sprite = LevelSelected.levelButtonSprite;
    }

    public void OnClickLevel() {
        if (LevelSelected.LevelLockStatus == LockStatus.Unlocked) {
            LevelSelectionPopup levelSelectionPopup = (LevelSelectionPopup)UIPopupMaker.Instance.Popup(UIList.LevelSelectionPopup, GameController.Instance.GameCanvas);
            levelSelectionPopup.Setup(LevelSelected);
            Game.SelectedLevel = LevelSelected;
        } else {
            LevelSelectionUnlock levelSelectionUnlock = (LevelSelectionUnlock)UIPopupMaker.Instance.Popup(UIList.LevelSelectionUnlock, GameController.Instance.GameCanvas);
            levelSelectionUnlock.Setup(LevelSelected);
            Game.SelectedLevel = LevelSelected;
        }
    }
}
