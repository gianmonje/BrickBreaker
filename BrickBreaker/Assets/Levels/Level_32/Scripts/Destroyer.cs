using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
    public void OnDisable() {
        Destroy();
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }
}
