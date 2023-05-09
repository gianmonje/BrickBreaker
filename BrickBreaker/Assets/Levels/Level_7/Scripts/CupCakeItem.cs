using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupCakeItem : MonoBehaviour {
    public CupCakeFlavor cupCakeFlavor;
    public SpriteRenderer cupcakeSprite;
    public SpriteRenderer cupcakeDeathEffectSprite;

    [HideInInspector]
    public Vector2 initialPosition;

    private ConveyerBelt conveyerBelt { get { return this.transform.parent.GetComponent<ConveyerBelt>(); } }
    private List<string> checkListToDestroy { get { return conveyerBelt.Level7LevelController.checkListToDestroy; } }

    private void Awake() {
        initialPosition = transform.localPosition;
    }

    public void Cook(CupCakeFlavor cupCakeFlavor, Sprite cupCakeSprite) {
        this.cupCakeFlavor = cupCakeFlavor;
        cupcakeSprite.sprite = cupcakeDeathEffectSprite.sprite = cupCakeSprite;
    }

    public void CheckIfCorrectItem() {
        if (checkListToDestroy.Count <= 0) return;
        if (checkListToDestroy[0] == cupCakeFlavor.ToString()) {
            conveyerBelt.Level7LevelController.RemoveFromChecklist(cupCakeFlavor.ToString());
        }
    }
}
