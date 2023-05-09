using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLevel7LevelController : LevelController {
    public ConveyerBelt conveyerBelt;

    public override void ResetLevel() {
        base.ResetLevel();
        GameplayHUD.Instance.alienCupcakepAnimator.gameObject.SetActive(true);
        ShuffleList.Shuffle(checkListToDestroy);
        CupCakeFlavor initialCupCakeFlavor = EnumParser.ParseEnum<CupCakeFlavor>(checkListToDestroy[0]);
        Sprite cupcakeSprite = conveyerBelt.GetCupcakeSprite(initialCupCakeFlavor);
        if (cupcakeSprite != null) GameplayHUD.Instance.ShowCupcake(cupcakeSprite);
    }

    public override void RemoveFromChecklist(string item) {
        base.RemoveFromChecklist(item);
        if (checkListToDestroy.Count > 0) {
            Sprite cupcakeSprite = conveyerBelt.GetCupcakeSprite(EnumParser.ParseEnum<CupCakeFlavor>(checkListToDestroy[0]));
            GameplayHUD.Instance.AlienGetNewCupCake(cupcakeSprite);
        }
    }

    public bool IsCorrectFlavor(CupCakeFlavor cupCakeFlavor) {
        return (EnumParser.ParseEnum<CupCakeFlavor>(checkListToDestroy[0]) == cupCakeFlavor);
    }
}
