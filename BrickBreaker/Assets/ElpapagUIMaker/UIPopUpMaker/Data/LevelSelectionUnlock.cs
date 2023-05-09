using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelSelectionUnlock : UIPopup {

    [SerializeField]
    public GameObject unlockHolder, unlockConfirmationHolder;
    private Level levelSelected;

    private new void Awake() {
        base.Awake();
    }

    public void Setup(Level levelSelected) {
        this.levelSelected = levelSelected;
    }

    public void SetVisibilityUnlockConfirmation(bool isActive) {
        unlockHolder.SetActive(!isActive);
        unlockConfirmationHolder.SetActive(isActive);
    }

    public void OnUnlockButtonClicked() {
        LevelConfig.Instance.UnlockLevel(levelSelected);
        Hide();
        //Show Level Selection
        LevelSelectionPopup levelSelectionPopup = (LevelSelectionPopup)UIPopupMaker.Instance.Popup(UIList.LevelSelectionPopup, GameController.Instance.GameCanvas);
        levelSelectionPopup.Setup(levelSelected);
        Game.SelectedLevel = levelSelected;
    }
}
