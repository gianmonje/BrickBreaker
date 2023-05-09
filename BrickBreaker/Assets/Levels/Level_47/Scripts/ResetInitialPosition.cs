using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetInitialPosition : MonoBehaviour {

    private Vector2 initPosition;
    // Start is called before the first frame update
    void Awake() {
        initPosition = transform.position;
    }

    public void ResetPosition() {
        transform.position = initPosition;
    }

}
