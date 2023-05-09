using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasID : MonoBehaviour {
    [ReadOnly]
    public string ID;

    public void GenerateRandomID() {
        ID = GetInstanceID().ToString();
    }
    
}
