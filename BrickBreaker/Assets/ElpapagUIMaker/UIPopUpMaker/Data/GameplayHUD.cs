using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameplayHUD : UIScreen<GameplayHUD> {
    public Animator healthAnimator;
    public Animator moonHelpAnimator;
    public Animator alienCupcakepAnimator;
    public Image alienCupcake;

    public float hitpointDebug;

    private Sprite cupcakeToGet;

    public override void ShowScreen(float delay = 0) {
        base.ShowScreen(delay);
        LevelsManager.Instance.OnBrickReducedHitpoints += OnBrickReducedHitpoints;
    }

    public override void Hide(bool destroyThis = false) {
        base.Hide(destroyThis);
        LevelsManager.Instance.OnBrickReducedHitpoints -= OnBrickReducedHitpoints;
    }

    public void ShowCupcake(Sprite cupcakeSprite) {
        alienCupcake.sprite = cupcakeSprite;
        alienCupcakepAnimator.SetTrigger("show");
    }

    public void AlienGetNewCupCake(Sprite cupcakeToGet) {
        this.cupcakeToGet = cupcakeToGet;
        alienCupcakepAnimator.SetTrigger("hide");
    }

    public void GetCupcakeSprite() {
        ShowCupcake(cupcakeToGet);
    }

    public void UpdateHealth(BrickData brickData) {
        healthAnimator.SetFloat("hitpoints", brickData.HitPointPercentage);
    }

    private void OnBrickReducedHitpoints(BrickData brickData) {
        Debug.Log("currentHitpoints: " + brickData.currentHitpoints);
        UpdateHealth(brickData);
    }
}