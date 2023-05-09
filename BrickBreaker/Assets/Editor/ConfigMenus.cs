using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConfigMenus {
    [MenuItem("Configs/Ball Config")]
    static void PingBallConfig() {
        ScriptableObject scriptableObject = BallConfig.Instance;
        Selection.activeObject = scriptableObject;
        EditorGUIUtility.PingObject(BallConfig.Instance);
    }
    [MenuItem("Configs/Game Config")]
    static void PingGameConfig() {
        ScriptableObject scriptableObject = GameConfig.Instance;
        Selection.activeObject = scriptableObject;
        EditorGUIUtility.PingObject(GameConfig.Instance);
    }
    [MenuItem("Configs/Level Config")]
    static void PingLevelConfig() {
        ScriptableObject scriptableObject = LevelConfig.Instance;
        Selection.activeObject = scriptableObject;
        EditorGUIUtility.PingObject(LevelConfig.Instance);
    }

}
