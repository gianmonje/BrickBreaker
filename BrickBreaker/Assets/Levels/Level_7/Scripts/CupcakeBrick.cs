using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupcakeBrick : Brick {
    public CustomLevel7LevelController customLevel7LevelController;

    public override void ReduceHitpoints(int count) {
        //Check first if its the correct flavor the alien is showing if not return else contiune to reduce hitpoints
        if (!customLevel7LevelController.IsCorrectFlavor(GetComponent<CupCakeItem>().cupCakeFlavor)) return;

        brickData.currentHitpoints -= count;
        LevelsManager.Instance.TriggerOnBrickReducedHitPoints(brickData);

        if (OnLooseHitpoinTrigger != null) OnLooseHitpoinTrigger.Invoke(count);

        if (brickData.currentHitpoints <= 0) {
            //Do Death Here
            if (!brickData.dontDestroyColliderOnZeroHealth) SetCollider(false);
            DoDeathEffect();
            if (OnZeroHealthTrigger != null) OnZeroHealthTrigger.Invoke();
        } else {
            HandleBrickSprite();
        }
    }

    public override void DoDeathEffect() {
        HideAllBrickSprites();
        if (brickDeathEffect != null) {
            brickDeathEffect.gameObject.SetActive(true);
            if (brickDeathEffect.GetComponent<Animator>() != null) brickDeathEffect.GetComponent<Animator>().SetTrigger("play");
        }
        if (OnDeathTrigger != null) OnDeathTrigger.Invoke();
        if (customLevel7LevelController.IsClearedCondition) {
            //BallManager.Instance.ActiveBalls[0].ShowFirework();
        }
    }

    public override void DestroyBrick() {
        if (OnDestroyTrigger != null) OnDestroyTrigger.Invoke();
        customLevel7LevelController.CheckClearCondition();
        if (brickData.destroyWhenNoHealthLeft) gameObject.SetActive(false);
    }
}
