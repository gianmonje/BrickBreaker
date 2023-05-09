using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour {


    [BoxGroup("NoTitle", false)]
    public int levelID;

    [BoxGroup("NoTitle", false)]
    public List<Brick> bricksCollection;

    [BoxGroup("NoTitle", false)]
    public int overrideAllBrickHitpoints;

    [Button("SetAll Bricks HitPoints to @overrideAllBrickHitpoints"), BoxGroup("NoTitle", false)]
    private void SetAllBrickHitpoints() {
        for (int i = 0; i < brickDataList.Count; i++) {
            brickDataList[i].hitpoints = overrideAllBrickHitpoints;
        }
    }

    [BoxGroup("NoTitle", false), TableList(ShowIndexLabels = true)]
    public List<BrickData> brickDataList;

    [Button("Get Bricks"), BoxGroup("NoTitle", false)]
    private void GetAllBricks() {
        bricksCollection = GetComponentsInChildren<Brick>().ToList();

        brickDataList = new List<BrickData>();
        for (int i = 0; i < bricksCollection.Count; i++) {
            brickDataList.Add(bricksCollection[i].brickData);
        }
    }

    [Button("Remove All Bricks"), BoxGroup("NoTitle", false)]
    private void RemoveAllBricks() {
        bricksCollection = null;
        brickDataList.Clear();
    }

    [Button("SAVE CHANGES"), BoxGroup("NoTitle", false)]
    private void SaveChangesData() {
        for (int i = 0; i < bricksCollection.Count; i++) {
            bricksCollection[i].brickData = brickDataList[i];
        }
    }

    public LevelClearCondition levelClearCondition;

    private bool IsDestroyAllBricks {
        get {
            return levelClearCondition == LevelClearCondition.DestroyAllBricks;
        }
    }

    private bool IsDestroyBricksOnList {
        get {
            return levelClearCondition == LevelClearCondition.DestroyBricksOnList;
        }
    }
    [ShowIf("IsDestroyBricksOnList")]
    public Brick[] bricksToDestroy;

    private bool IsDestroyCheckList {
        get {
            return levelClearCondition == LevelClearCondition.DestroyCheckList;
        }
    }
    [ShowIf("IsDestroyCheckList")]
    public List<string> checkListToDestroy;

    public UnityEvent OnClearedGame;
    public UnityEvent OnReset;

    [SerializeField]
    private List<string> initialCheckListToDestroy;

    private void Awake() {
        if (checkListToDestroy == null) return;
        initialCheckListToDestroy = new List<string>();
        for (int i = 0; i < checkListToDestroy.Count; i++) {
            string item = checkListToDestroy[i].ToString();
            initialCheckListToDestroy.Add(item);
        }
    }

    private void OnEnable() {
        ResetLevel();
    }

    [Button("SET DATA TO ALL BRICKS"), BoxGroup("BRICK DATA")]
    public void SetBrickResponsiveness() {
        var screenHeightEditor = ScreenSize.GetScreenToWorldHeight;
        var screenWidthEditor = ScreenSize.GetScreenToWorldWidth;

        for (int i = 0; i < bricksCollection.Count; i++) {
            Brick brick = bricksCollection[i];
            brick.GetRelativePositionsOfBricks();
            brick.GetRelativeScaleOfBricks();
        }
    }

    public void AddToChecklist(string item) {
        if (checkListToDestroy == null) checkListToDestroy = new List<string>();
        checkListToDestroy.Add(item);
    }

    public virtual void RemoveFromChecklist(string item) {
        checkListToDestroy.Remove(item);
        CheckClearCondition();
    }

    public virtual void ResetLevel() {
        SaveChangesData();
        if (initialCheckListToDestroy != null) {
            checkListToDestroy.Clear();
            for (int i = 0; i < initialCheckListToDestroy.Count; i++) {
                string item = initialCheckListToDestroy[i].ToString();
                checkListToDestroy.Add(item);
            }
        }

        for (int i = 0; i < bricksCollection.Count; i++) {
            Brick brick = bricksCollection[i];
            if (!brick.IsAlive) {
                if (brick.brickData.enableOnReset) brick.gameObject.SetActive(true);
                if (brick.gameObject.activeInHierarchy) brick.ResetBrick();
            }
        }

        if (OnReset != null) OnReset.Invoke();
    }

    public void CheckClearCondition() {
        if (IsDestroyAllBricks) {
            if (NoMoreBricks) ShowGameOverWin();
        } else if (IsDestroyBricksOnList) {
            if (NoMoreBricksToDestroy) ShowGameOverWin();
        } else if (IsDestroyCheckList) {
            if (NoMoreChecklishtToDestroy) ShowGameOverWin();
        }
    }

    public void ShowGameOverWin() {
        LevelConfig.Instance.UnlockNextLevel();
        GameplayManager.Instance.SetGameOver(GameResult.Win);
        if (OnClearedGame != null) OnClearedGame.Invoke();
    }

    public bool IsClearedCondition {
        get {
            if (IsDestroyAllBricks) {
                if (NoMoreBricks) return true;
            } else if (IsDestroyBricksOnList) {
                if (NoMoreBricksToDestroy) return true;
            } else if (IsDestroyCheckList) {
                if (NoMoreChecklishtToDestroy) return true;
            }
            return false;
        }
    }

    private bool NoMoreBricks {
        get {
            for (int i = 0; i < bricksCollection.Count; i++) {
                Brick brick = bricksCollection[i];
                if (brick.IsAlive) {
                    return false;
                }
            }
            return true;
        }
    }

    private bool NoMoreBricksToDestroy {
        get {
            for (int i = 0; i < bricksToDestroy.Length; i++) {
                Brick brick = bricksToDestroy[i];
                if (brick.IsAlive) {
                    return false;
                }
            }
            return true;
        }
    }

    private bool NoMoreChecklishtToDestroy {
        get {
            return checkListToDestroy.Count <= 0;
        }
    }
}
