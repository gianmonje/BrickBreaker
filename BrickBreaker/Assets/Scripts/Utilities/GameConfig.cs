using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Custom/Game Config")]
public class GameConfig : ScriptableObject {
    private static GameConfig instance;
    public static GameConfig Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<GameConfig>("Configs/GameConfig");
            }
            return instance;
        }
    }


    public int playerLives;

    public float deathSlowmoInSeconds;
}
