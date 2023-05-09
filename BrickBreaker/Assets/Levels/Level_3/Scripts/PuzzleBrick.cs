using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBrick : Brick {

    [System.Serializable]
    public class ColliderHit {
        public int hitpoint;
        public Vector2 offset;
        public Vector2 size;

        private BoxCollider2D boxCollider;
        private PuzzleBrick puzzleBrick;

        public void SetBoxCollider(BoxCollider2D boxCollider, PuzzleBrick puzzleBrick) {
            this.boxCollider = boxCollider;
            this.puzzleBrick = puzzleBrick;
        }

        [Button("Set To This Collider"), BoxGroup("NoTitle", false)]
        public void SetToThisColliderValue() {
            boxCollider.offset = offset;
            boxCollider.size = size;

            puzzleBrick.HideAllBrickSprites();
            puzzleBrick.brickHitpointSprites[0].spriteGameObject.SetActive(true);
        }
    }
    [SerializeField]
    private List<ColliderHit> colliderHitList;
    BoxCollider2D BoxCollider { get { return GetComponent<BoxCollider2D>(); } }

    [Button("Get Current Collider Value"), BoxGroup("NoTitle", false)]
    public void GetColliderValue() {
        ColliderHit colliderHit = new ColliderHit();
        colliderHit.offset = BoxCollider.offset;
        colliderHit.size = BoxCollider.size;
        colliderHit.SetBoxCollider(BoxCollider, this);
        colliderHitList.Add(colliderHit);
    }

    [Button("Set To Initial"), BoxGroup("NoTitle", false), ShowIf(@"ColliderHitListIsNotEmpty")]
    public void SetToInitial() {
        BoxCollider.offset = colliderHitList[0].offset;
        BoxCollider.size = colliderHitList[0].size;
        HideAllBrickSprites();
        brickHitpointSprites[0].spriteGameObject.SetActive(true);
    }

    private bool ColliderHitListIsNotEmpty {
        get {
            return this.colliderHitList.Count > 0;
        }
    }

    public override void ResetBrick() {
        base.ResetBrick();
        SetToInitial();
    }

    public void UpdateCollider() {
        for (int i = 0; i < colliderHitList.Count; i++) {
            if (brickData.currentHitpoints == colliderHitList[i].hitpoint) {
                BoxCollider.offset = colliderHitList[i].offset;
                BoxCollider.size = colliderHitList[i].size;
            }
        }
    }

    protected override void HandleBrickSprite() {
        for (int i = 0; i < brickHitpointSprites.Length; i++) {
            if (brickData.currentHitpoints == brickHitpointSprites[i].hitpoint) {
                HideAllBrickSprites();
                brickHitpointSprites[i].spriteGameObject.SetActive(true);
                brickHitpointSprites[i].spriteGameObject.GetComponent<Puzzle>().DoHitEffetct();
                UpdateCollider();
            }

        }
    }
}
