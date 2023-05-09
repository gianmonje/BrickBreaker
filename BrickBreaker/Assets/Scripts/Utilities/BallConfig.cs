using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BallConfig", menuName = "Custom/Ball Config")]
public class BallConfig : ScriptableObject {
    private static BallConfig instance;
    public static BallConfig Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<BallConfig>("Configs/BallConfig");
            }
            return instance;
        }
    }

    public Ball ballPrefab;
}
