using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBrick : Brick {
    public MaskHitHandler maskHitHandler;

    public override void SetCollision(Collision2D collision) {
        if (!IsAlive) return;
        if (brickData.isInvulnerable) return;
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null) {
            ApplyCollisionLogic(ball);

            if (brickData.currentHitpoints > 0) {
                if (OnHitTrigger != null) OnHitTrigger.Invoke();
            } else {
                GetComponent<Animator>().SetTrigger("start");
                maskHitHandler.ShowAStar();
            }
        }
    }
}
