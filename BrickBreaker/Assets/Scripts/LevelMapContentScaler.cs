using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapContentScaler : MonoBehaviour {
    public RectTransform[] childToGetSize;
    public float sizeOffsetHorizontal = 565f;

    private void Start() {
        SetContentSizeToChildren();
    }

    [Button("Set Size"), BoxGroup("NoTitle", false)]
    public void SetContentSizeToChildren() {
        float sizeHorizontal = 0;
        for (int i = 0; i < childToGetSize.Length; i++) {
            sizeHorizontal += childToGetSize[i].rect.size.x;
        }
        sizeHorizontal -= sizeOffsetHorizontal;
        GetComponent<RectTransform>().sizeDelta = new Vector2(sizeHorizontal, 1920);
    }
}
