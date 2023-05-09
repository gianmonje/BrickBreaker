using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class ResourcesBugFixer {
    static string ReloadedAssetsStateName = "ResourcesBugFixer.ReloadedAssets";

    static ResourcesBugFixer() {
        EditorApplication.delayCall += ReloadAssets;
    }

    static void ReloadAssets() {
        if (!SessionState.GetBool(ReloadedAssetsStateName, false)) {
            SessionState.SetBool(ReloadedAssetsStateName, true);
            var guid = AssetDatabase.FindAssets("t:Script")[0];
            AssetDatabase.ImportAsset(AssetDatabase.GUIDToAssetPath(guid));
            Debug.LogFormat("Refreshed asset database");
        }
    }
}