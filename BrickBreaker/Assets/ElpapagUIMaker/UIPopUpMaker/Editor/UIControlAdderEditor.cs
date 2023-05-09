using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScreenLocker))]
public class UIControlAdderEditor : Editor {
    bool showPosition = false;
    float yPosButton = 30;
    float ySpacing = 21;
    float buttonWidth = 75;

    void OnSceneGUI() {
        Handles.BeginGUI();
        GUIStyle labelHeaderStyle = new GUIStyle();
        labelHeaderStyle.fontStyle = FontStyle.Bold;
        labelHeaderStyle.normal.textColor = Color.white;
        labelHeaderStyle.padding = new RectOffset(20, 0,0,0);

        Rect transparentRect = new Rect(16, 7, 100, 22);
        if (!showPosition) {
            transparentRect  = new Rect(16, 7, 100, 22);
        } else {
            transparentRect = new Rect(16, 7, 100, 236.5f);
        }
        Texture transparent = Resources.Load("Elpapag/Textures/transparent") as Texture;
        GUI.DrawTexture(transparentRect, transparent, ScaleMode.StretchToFill, true, 0);

        GUIStyle addIconStyle = new GUIStyle();
        addIconStyle.padding = new RectOffset(0, 0, 0, 0);
        Texture addIcon = Resources.Load("Elpapag/Textures/add_control") as Texture;
        if (GUI.Button(new Rect(20, 10, 15, 15), addIcon, addIconStyle)) {
            showPosition = showPosition ? false : true;
        }
       
        GUI.Label(new Rect(18, 11, 100, 20), "CONTROLS", labelHeaderStyle);

        if (showPosition) {
            yPosButton = 30;

            AddButton("Button", ()=> {
                AddControl("Button");
            });

            AddButton("Image", () => {
                AddControl("Image");
            });

            AddButton("RawImage", () => {
                AddControl("RawImage");
            });

            AddButton("Text", () => {
                AddControl("Text");
            });

            AddButton("Panel", () => {
                AddControl("Panel");
            });

            AddButton("Canvas", () => {
                AddControl("Canvas");
            });

            AddButton("ScrollView", () => {
                AddControl("ScrollView");
            });

            AddButton("Dropdown", () => {
                AddControl("Dropdown");
            });

            AddButton("Slider", () => {
                AddControl("Slider");
            });

            AddButton("Toggle", () => {
                AddControl("Toggle");
            });
        }
        
        Handles.EndGUI();
    }

    public void AddButton(string text, Action action) {
        GUI.backgroundColor = Color.yellow;
        if (GUI.Button(new Rect(30, yPosButton, buttonWidth, 18), text)) {
            action.Invoke();
        }
        yPosButton += ySpacing;
    }

    private void AddControl(string prefabName) {
        GameObject holder = Selection.activeGameObject.gameObject.transform.Find("Holder").gameObject;
        GameObject layout = holder.gameObject.transform.Find("Layout").gameObject;
        GameObject prefab = Instantiate(Resources.Load(string.Format("Elpapag/ControlPrefabs/{0}", prefabName)) as GameObject, layout.transform);
    }

}
