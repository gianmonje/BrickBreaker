using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupStarForce : StarForce {
    public void Stop() {
        gameObject.SetActive(false);
    }
}
