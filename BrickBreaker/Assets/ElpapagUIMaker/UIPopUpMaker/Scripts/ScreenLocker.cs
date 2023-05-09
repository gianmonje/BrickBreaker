using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLocker : MonoBehaviour {
    [ReadOnly]
    public bool isLocked;
    [HideInInspector]
    public string uiID;
    [HideInInspector]
    public GameObject temporaryCanvas;
}
