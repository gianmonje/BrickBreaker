using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteTargetFollower : MonoBehaviour {
    public Transform target;
    private Vector2 initPos;
    private void Awake() {
        initPos = target.position;
    }

    private void OnDisable() {
        transform.position = initPos;
    }

    // Update is called once per frame
    private void Update() {
        if (target != null) this.transform.position = target.position;
    }
}
