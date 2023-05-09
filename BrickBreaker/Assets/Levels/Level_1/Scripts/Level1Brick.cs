using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Brick : Brick {

    public void OnBrickEffectDone() {
        DestroyBrick();
    }
}
