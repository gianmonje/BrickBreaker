using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PaddleConfig", menuName = "Custom/Paddle Config")]
public class PaddleConfig : ScriptableObject {
    private static PaddleConfig instance;
    public static PaddleConfig Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<PaddleConfig>("Configs/PaddleConfig");
            }
            return instance;
        }
    }

}
