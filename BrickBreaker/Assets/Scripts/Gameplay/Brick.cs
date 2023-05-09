using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Brick : MonoBehaviour {

    #region PUBLIC FIELDS
    public BrickData brickData;
    public bool isOkToDoRelativePosScale = true;

    public int brickIndex;
    public Quaternion brickRotation;
    public float setupScreenWidth;
    public float setupScreenHeight;
    public float widthRelativePos;
    public float heightRelativePos;
    public float setupWidthScale;
    public float setupHeightScale;

    [SerializeField]
    public BrickHitpointSprite[] brickHitpointSprites;

    public bool IsAlive {
        get {
            return (brickData.currentHitpoints > 0);
        }
    }

    public BrickDeathEffect brickDeathEffect;
    public BrickHitpointSprite currentHealthSprite;
    private Vector2 initialPosition;
    #endregion

    #region PRIVATE FIELDS
    protected LevelController BrickLevelController {
        get {
            LevelController levelController = new LevelController();
            if (transform.parent.GetComponent<LevelController>() != null) {
                levelController = transform.parent.GetComponent<LevelController>();
            } else {
                levelController = transform.parent.parent.GetComponent<LevelController>();
            }
            return levelController;
        }
    }
    #endregion

    #region EVENTS
    public UnityEvent OnEnableBrick;
    public UnityEvent OnHitTrigger;
    public UnityEvent OnDeathTrigger;
    public UnityEvent OnDestroyTrigger;
    public UnityEvent<int> OnLooseHitpoinTrigger;
    public UnityEvent OnZeroHealthTrigger;
    public UnityEvent OnEnabledColliderTrigger;
    public UnityEvent OnDisabledColliderTrigger;
    #endregion

    private void Awake() {
        initialPosition = this.transform.position;
    }

    private void OnEnable() {
        if (OnEnableBrick != null) OnEnableBrick.Invoke();
        ResetBrick();
    }

    public virtual void ResetBrick() {
        if (brickHitpointSprites.Length > 0) {
            currentHealthSprite = GetDefaultBrickHitpintSprite(brickHitpointSprites);
            HideAllBrickSprites();
            currentHealthSprite.spriteGameObject.SetActive(true);
        }


        if (brickDeathEffect != null) {
            brickDeathEffect.gameObject.SetActive(false);
            if (brickDeathEffect.GetComponent<Animator>() != null) brickDeathEffect.GetComponent<Animator>().SetTrigger("stop");
        }
        SetCollider(true);
        StartCoroutine(DelaySetPos());
    }

    public void SetVulnerability(bool isInvulnerable) {
        brickData.isInvulnerable = isInvulnerable;
    }

    private IEnumerator DelaySetPos() {
        yield return new WaitForSeconds(0.1f);
        transform.position = initialPosition;
        brickData.currentHitpoints = brickData.hitpoints;

        SetRelativePositionsOfBricks();
        SetRelativeScaleOfBricks();

        isOktoUpdate = true;
    }

    private bool isOktoUpdate = false;
    private void Update() {
        if (!isOktoUpdate || !isOkToDoRelativePosScale) return;
        if (!IsAlive) return;
        SetRelativePositionsOfBricks();
        SetRelativeScaleOfBricks();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        SetCollision(collision);
    }

    public virtual void SetCollision(Collision2D collision) {
        Debug.Log("HIT");
        if (!IsAlive) return;
        if (brickData.isInvulnerable) return;
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null) {
            if (OnHitTrigger != null) OnHitTrigger.Invoke();
            ApplyCollisionLogic(ball);
        }
    }

    protected void ApplyCollisionLogic(Ball ball) {
        if (!brickData.isInvulnerable) ReduceHitpoints(1);
    }

    public virtual void ReduceHitpoints(int count) {
        if (brickData.currentHitpoints <= 0) return;
        brickData.currentHitpoints -= count;
        LevelsManager.Instance.TriggerOnBrickReducedHitPoints(brickData);

        if (OnLooseHitpoinTrigger != null) OnLooseHitpoinTrigger.Invoke(count);

        if (brickData.currentHitpoints <= 0) {
            //Do Death Here
            if (!brickData.dontDestroyColliderOnZeroHealth) SetCollider(false);
            DoDeathEffect();
            HideAllBrickSprites();
            if (OnZeroHealthTrigger != null) OnZeroHealthTrigger.Invoke();
        }
        HandleBrickSprite();

    }

    protected virtual void HandleBrickSprite() {
        for (int i = 0; i < brickHitpointSprites.Length; i++) {
            if (brickData.currentHitpoints == brickHitpointSprites[i].hitpoint) {
                HideAllBrickSprites();
                brickHitpointSprites[i].spriteGameObject.SetActive(true);
            }
        }
    }

    public void HideAllBrickSprites() {
        for (int i = 0; i < brickHitpointSprites.Length; i++) brickHitpointSprites[i].spriteGameObject.SetActive(false);
    }

    public void SetCollider(bool isOn) {
        if (GetComponent<BoxCollider2D>() != null) GetComponent<BoxCollider2D>().enabled = isOn;
        if (GetComponent<CircleCollider2D>() != null) GetComponent<CircleCollider2D>().enabled = isOn;
        if (GetComponent<PolygonCollider2D>() != null) GetComponent<PolygonCollider2D>().enabled = isOn;
        if (isOn) {
            if (OnEnabledColliderTrigger != null) OnEnabledColliderTrigger.Invoke();
        } else {
            if (OnDisabledColliderTrigger != null) OnDisabledColliderTrigger.Invoke();
        }
    }

    public virtual void DoDeathEffect() {
        if (brickDeathEffect != null) {
            HideAllBrickSprites();
            brickDeathEffect.gameObject.SetActive(true);
            if (brickDeathEffect.GetComponent<Animator>() != null) brickDeathEffect.GetComponent<Animator>().SetTrigger("play");
        }
        if (OnDeathTrigger != null) OnDeathTrigger.Invoke();
        if (BrickLevelController.IsClearedCondition) {
            //if (BrickLevelController != null) BallManager.Instance.ActiveBalls[0].ShowFirework();
        }
    }

    public virtual void DestroyBrick() {
        if (OnDestroyTrigger != null) OnDestroyTrigger.Invoke();
        BrickLevelController.CheckClearCondition();
        if (brickData.destroyWhenNoHealthLeft) gameObject.SetActive(false);
    }

    private BrickHitpointSprite GetDefaultBrickHitpintSprite(BrickHitpointSprite[] brickHitpointSprites) {
        BrickHitpointSprite brickHitpointSprite = brickHitpointSprites[0];
        for (var i = 1; i < brickHitpointSprites.Length; i++) {
            if (brickHitpointSprites[i].hitpoint > brickHitpointSprite.hitpoint) {
                brickHitpointSprite = brickHitpointSprites[i];
            }
        }
        return brickHitpointSprite;
    }

    [Button("SET DATA TO BRICKS"), BoxGroup("BRICK DATA")]
    public void SetRelativeData() {
        GetRelativePositionsOfBricks();
        GetRelativeScaleOfBricks();
    }

    #region Relative Position
    public void GetRelativePositionsOfBricks() {
        var screenHeightEditor = ScreenSize.GetScreenToWorldHeight;
        var screenWidthEditor = ScreenSize.GetScreenToWorldWidth;

        widthRelativePos = (transform.position.x / screenWidthEditor);
        heightRelativePos = (transform.position.y / screenHeightEditor);
        brickRotation = transform.rotation;
    }

    public void SetRelativePositionsOfBricks() {
        var screenHeightEditor = ScreenSize.GetScreenToWorldHeight;
        var screenWidthEditor = ScreenSize.GetScreenToWorldWidth;

        transform.position = new Vector3(screenWidthEditor * widthRelativePos, screenHeightEditor * heightRelativePos, 0);
        transform.rotation = brickRotation;
    }
    #endregion

    #region Relative Scale
    public void GetRelativeScaleOfBricks() {
        setupScreenHeight = ScreenSize.GetScreenToWorldHeight;
        setupScreenWidth = ScreenSize.GetScreenToWorldWidth;

        var numBricks = transform.childCount;

        setupWidthScale = transform.localScale.x;
        setupHeightScale = transform.localScale.y;
    }

    public void SetRelativeScaleOfBricks() {
        var currentScreenHeight = ScreenSize.GetScreenToWorldHeight;
        var currentScreenWidth = ScreenSize.GetScreenToWorldWidth;

        var heightScaleChangeFactor = (currentScreenHeight - setupScreenHeight) / setupScreenHeight;
        var widthScaleChangeFactor = (currentScreenWidth - setupScreenWidth) / setupScreenWidth;

        float scaleChangeFactor;

        if (heightScaleChangeFactor != widthScaleChangeFactor) {
            scaleChangeFactor = (heightScaleChangeFactor + widthScaleChangeFactor) / 2;
        } else {
            scaleChangeFactor = heightScaleChangeFactor;
        }

        var newXScale = setupWidthScale + (setupWidthScale * scaleChangeFactor);
        var newYScale = setupHeightScale + (setupHeightScale * scaleChangeFactor);

        transform.localScale = new Vector3(newXScale, newYScale, 1);
    }
    #endregion

}
