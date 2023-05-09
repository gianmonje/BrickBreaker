using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class LevelSelectionPopup : UIPopup {
    [SerializeField]
    private TextMeshProUGUI headerText;
    [SerializeField]
    private Image levelImage;

    private new void Awake() {
        base.Awake();
    }

    public void Setup(Level level) {
        headerText.text = string.Format("{0}", level.levelName);
        levelImage.sprite = level.levelPreview;
    }

    public void OnClickCampaign() {
        GameController.Instance.ShowGameplay();
        LevelMap.Instance.Hide();
        LevelMapHUD.Instance.Hide();
    }

    public void OnClickNormal() {
        GameController.Instance.ShowGameplay();
        LevelMap.Instance.Hide();
        LevelMapHUD.Instance.Hide();
    }
}
