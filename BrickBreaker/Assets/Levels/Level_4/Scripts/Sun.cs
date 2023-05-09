using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
    public SpriteRenderer sunFace;
    public Sprite smile;
    public Sprite sad;
    [SerializeField]
    private float sadDuration;
    private LevelController levelController;

    private void OnEnable() {
        levelController = transform.parent.GetComponent<LevelController>();
        Smile();

    }

    private void Update() {
        Vector2 targetPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        transform.position = new Vector2(targetPos.x, transform.position.y);

        if (levelController.IsClearedCondition && sunFace.sprite != sad) sunFace.sprite = sad;
    }

    public void Smile() {
        sunFace.sprite = smile;
    }

    private IEnumerator SadDuration() {
        sunFace.sprite = sad;
        yield return new WaitForSeconds(sadDuration);
        if (!levelController.IsClearedCondition) Smile();
    }

    public void Sad() {
        StartCoroutine(SadDuration());
    }
}
