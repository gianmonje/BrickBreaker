using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager> {

    public bool IsGameStarted { get; set; } = false;
    public int Lives { get; set; }

    static public bool IsGameOver {
        get {
            return isGameOver;
        }
        set {
            isGameOver = value;
            if (!isGameOver) Time.timeScale = 1;
        }
    }
    static private bool isGameOver;

    private bool isTimeStoppedCompletely = false;
    private float defaultGixedDeltaTime;

    private void OnEnable() {
        //DEBUGGING LEVEL
        if (LevelsManager.Instance.isDebug) Game.SelectedLevel = LevelConfig.Instance.GetLevel(LevelsManager.Instance.debugLevelID);

        Debug.Log(Time.fixedDeltaTime);
        StartCoroutine(StartGame(0.1f));
        //Time.fixedDeltaTime = defaultGixedDeltaTime;
    }

    private void OnDisable() {
        BallManager.Instance.OnBallKilled.RemoveAllListeners();
    }

    public override void Initialize() {
        defaultGixedDeltaTime = Time.fixedDeltaTime;
    }

    private IEnumerator StartGame(float delay) {
        yield return new WaitForSeconds(delay);
        StartNewGame();
    }

    public void StartNewGame() {
        Lives = GameConfig.Instance.playerLives;
        GameplayManager.Instance.IsGameStarted = false;
        LevelsManager.Instance.LoadSelectedLevel(Game.SelectedLevel);
        IsGameOver = false;
        #region EVENT LISTENERS
        BallManager.Instance.OnBallKilled.AddListener(OnABallKilled);
        #endregion
    }

    public void SetGameOver(GameResult gameResult) {
        IsGameOver = true;
        ShowGameOverScreen(gameResult);
    }

    public void ShowGameOverScreen(GameResult gameResult) {
        if (gameResult == GameResult.Win) {
            GameOverScreen.Instance.ShowScreen(0.7f);
        } else {
            GameLostScreen.Instance.ShowScreen(0);
        }
    }

    private void Update() {
        if (isTimeStoppedCompletely) return;
        if (IsGameOver && !isTimeStoppedCompletely) DoSlowMotionTimeStop();
    }

    private void DoSlowMotionTimeStop() {
        Time.timeScale = Mathf.Lerp(Time.timeScale, 0, GameConfig.Instance.deathSlowmoInSeconds * Time.deltaTime);
        //Time.fixedDeltaTime = 0.02F * Time.timeScale;
        if (Time.timeScale <= 0.2f) {
            //Time.fixedDeltaTime = 0.02F;
            Time.timeScale = 1;
        }
    }

    private void OnABallKilled(Ball ball) {
        Lives--;
        IsGameStarted = false;

        if (Lives <= 0) {
            SetGameOver(GameResult.Lose);
        }
    }

}
