using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(CustomButton))]
public class UIButtonOnClickEditor : Editor {
    private Vector2 scrollPos;
    void OnSceneGUI() {
        Handles.BeginGUI();
        if (Selection.activeGameObject == null) return;
        Vector2 offset = new Vector2((Selection.activeGameObject.GetComponent<RectTransform>().rect.width/5.5f), (Selection.activeGameObject.GetComponent<RectTransform>().rect.height / 5.5f));
        Vector2 offsetSize = new Vector2((SceneView.lastActiveSceneView.camera.pixelRect.width / 3f), (SceneView.lastActiveSceneView.camera.pixelRect.height / 3f));

        var screen = SceneView.lastActiveSceneView.camera.WorldToScreenPoint(Selection.activeGameObject.transform.position);
        Vector2 screenSceneView = new Vector2(screen.x, screen.y);
        Vector2 offsetSceneView = new Vector2(SceneView.lastActiveSceneView.camera.pixelRect.width, SceneView.lastActiveSceneView.camera.pixelRect.height);

        ////now you can set the position of the ui element
        GUILayout.BeginArea(new Rect((screenSceneView.x) + offset.x, (-screenSceneView.y + offsetSceneView.y) + offset.y, offsetSize.x, offsetSize.y));
            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(offsetSize.x), GUILayout.Height(offsetSize.y));
            {
                this.serializedObject.Update();
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onClickEvent"), true);
                this.serializedObject.ApplyModifiedProperties();
            }
            GUILayout.EndScrollView();
        GUILayout.EndArea();
       
        Handles.EndGUI();
    }
 
}
