using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UIRelativeScale : MonoBehaviour {
    public float setupScreenWidth;
    public float setupScreenHeight;
    public float setupWidthScale;
    public float setupHeightScale;

    public bool removeDebug = false;

    [Button("SET DATA TO BRICKS"), BoxGroup("BRICK DATA")]
    public void GetRelativity() {
        GetRelativeScaleOfBricks();

    }

    [Button("TEST"), BoxGroup("BRICK DATA")]
    public void Test() {
        SetRelativeScaleOfBricks();
    }

    // Update is called once per frame
    void Update() {
        if (removeDebug) return;
        if (setupScreenHeight == 0) return;
        SetRelativeScaleOfBricks();
    }

    #region Relative Scale
    public void GetRelativeScaleOfBricks() {
        //setupScreenHeight = ScreenSize.GetScreenToWorldHeight;
        //setupScreenWidth = ScreenSize.GetScreenToWorldWidth;

        //setupScreenHeight = Screen.height;
        //setupScreenWidth = Screen.width;

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
