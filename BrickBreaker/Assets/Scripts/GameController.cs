using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController> {
    [SerializeField]
    private GameObject gameplay;
    [SerializeField]
    private Transform gameCanvas;
    public Transform GameCanvas { get { return gameCanvas; } }

    public void ShowGameplay() {
        StartCoroutine(ShowGameplayWithDelay(0.05f));
    }

    public void ResetGameplay() {
        gameplay.SetActive(false);
        ShowGameplay();
    }

    private IEnumerator ShowGameplayWithDelay(float delay) {
        yield return new WaitForSeconds(delay);
        gameplay.SetActive(true);
        GameplayHUD.Instance.ShowScreen();
    }

}
