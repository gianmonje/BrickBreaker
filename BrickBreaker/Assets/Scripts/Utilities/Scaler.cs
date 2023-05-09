using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scaler : MonoBehaviour {

    // Use this for initialization
    void Update() {
        ResizeSpriteToScreen();
    }

    void ResizeSpriteToScreen() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector2(worldScreenWidth / width, worldScreenHeight / height);
    }
}