using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

public class UIPopupMakerMenuBar : Editor {
    [MenuItem("Tools/UIPopupMaker")]
    public static void OpenUIMaker() {
        UIMakerMainMenuWindow window = (UIMakerMainMenuWindow)EditorWindow.GetWindow(typeof(UIMakerMainMenuWindow), false, "UI POPUP MAKER");
    }
}
