using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartLevel39 : MonoBehaviour {
    private void OnDisable() {
        Debug.Log("Destroyed Heart");
        Destroy(this.gameObject);
    }
}
