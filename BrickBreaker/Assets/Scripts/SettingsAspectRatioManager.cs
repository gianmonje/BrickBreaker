using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SettingsAspectRatioManager : MonoBehaviour {

    private bool Is34 {
        get {
            Vector2 aspectRatio = AspectRatio.GetAspectRatio(new Vector2(Screen.width, Screen.height), false);
            return aspectRatio.x == 3 && aspectRatio.y == 4;
        }
    }


    private void Update() {
        if (Is34) {
            transform.localScale = new Vector3(0.8261684f, 0.8261684f, 0.8261684f);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
