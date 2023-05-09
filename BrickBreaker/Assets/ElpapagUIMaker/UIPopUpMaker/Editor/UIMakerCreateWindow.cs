using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEditorInternal;
using UnityEditor.SceneManagement;
using UnityEngine.UI;

public class UIMakerCreateWindow : EditorWindow {
    private string uiID = "";
    private string uiName = "";
    private GameObject canvasParent;
    private string canvasParentID;
    private int selected = 0;
    private int selectedUIType = 0;
    private string creatingType;

    private bool isDirty = false;
    private int counter = 0;

    private void OnGUI() {
        UIEditor();
    }

    private void UIEditor() {
        CreateHeaderBox("UI POP-UP EDITOR", new Color32(51, 145, 255, 255));

        EditorGUI.BeginDisabledGroup(isDirty);

        uiID = EditorGUI.TextField(new Rect(10, 25, position.width - 20, 20), "Popup ID:", uiID);
        uiName = EditorGUI.TextField(new Rect(10, 50, position.width - 20, 20), "Popup Name:", uiName);

        string[] uiTypeOptions;
        //UITYPE
        if (UITypeConfig.Instance.UITypePrefabs.Length <= 0) {
            GUIStyle lStyle = new GUIStyle();
            lStyle.normal.textColor = Color.red;
            uiTypeOptions = new string[] { "There are no objects on UITypePrefabs!" };
            selectedUIType = EditorGUI.Popup(new Rect(10, 75, position.width - 20, 20), "Type:", selectedUIType, uiTypeOptions, lStyle);
        } else {
            uiTypeOptions = new string[UITypeConfig.Instance.UITypePrefabs.Length];
            for (int i = 0; i < UITypeConfig.Instance.UITypePrefabs.Length; i++) {
                uiTypeOptions[i] = UITypeConfig.Instance.UITypePrefabs[i].name;
            }
            selectedUIType = EditorGUI.Popup(new Rect(10, 75, position.width - 20, 20), "Type:", selectedUIType, uiTypeOptions);
        }

        string dataScriptsPath = string.Format("{0}/ElpapagUIMaker/UIPopUpMaker/Data/{1}.cs", Application.dataPath, uiID);

        if (!File.Exists(dataScriptsPath)) {
            //PARENTING
            EditorGUI.DrawRect(new Rect(5, 95, position.width - 10, 55), new Color32(167, 167, 167, 255));
            CanvasID[] canvasObjects = FindObjectsOfType<CanvasID>();
            string[] options = new string[canvasObjects.Length + 1];
            options[0] = "None";
            for (int i = 1; i < options.Length; i++) {
                options[i] = canvasObjects[i - 1].gameObject.name;
            }

            selected = EditorGUI.Popup(new Rect(10, 100, position.width - 20, 20), "Parent Canvas", selected, options);
            if (selected > 0) {
                canvasParent = (GameObject)EditorGUI.ObjectField(new Rect(10, 125, position.width - 20, 20), "Parent Canvas", canvasObjects[selected - 1].gameObject, typeof(GameObject), true);
                canvasParentID = UIParentConfig.Instance.GetCanvasID(canvasParent);
            } else {
                GUIStyle dStyle = new GUIStyle();
                dStyle.normal.textColor = new Color32(99, 152, 174, 255);
                dStyle.alignment = TextAnchor.MiddleCenter;
                //EditorGUI.DropShadowLabel(new Rect(70, 115, position.width, 20), "This will create its own Parent Canvas!", dStyle);
                EditorGUI.HelpBox(new Rect(160, 120, position.width - 170, 20), "This will create its own Parent Canvas!", MessageType.Info);
            }
        }

        //Adding Component Loading
        if (isDirty) {
            //Check
            if (!EditorApplication.isCompiling && File.Exists(dataScriptsPath) == true && (Resources.Load(string.Format("Elpapag/{0}/{1}", creatingType, uiID)) as GameObject != null)) {
                if (File.Exists(dataScriptsPath)) AddUIComponent();
                isDirty = false;

                //Close this open MainMenu
                UIMakerCreateWindow window = (UIMakerCreateWindow)EditorWindow.GetWindow(typeof(UIMakerCreateWindow), false, "UI POPUP MAKER");
                window.Close();
                UIMakerMainMenuWindow windowMainMenu = (UIMakerMainMenuWindow)EditorWindow.GetWindow(typeof(UIMakerMainMenuWindow), false, "UI POPUP MAKER");

                if (uiTypeOptions[selectedUIType] == "Popup") UpdateUIEnumList();
            }

            string editButtonText = (isDirty) ? "Please Wait" : "Edit";
            if (GUI.Button(new Rect(10, 170, position.width - 20, 20), editButtonText)) {
            }
        }

        if (!File.Exists(dataScriptsPath)) {
            if (GUI.Button(new Rect(10, 170, position.width - 20, 20), "Create")) {
                switch (uiTypeOptions[selectedUIType]) {
                    case "Popup":
                    CreateUIPopupComponent();
                    creatingType = "UIPrefabs";
                    isDirty = true;
                    break;
                    case "Screen":
                    CreateUIScreenPopupComponent();
                    creatingType = "UIScreenPrefabs";
                    isDirty = true;
                    break;
                }
            }
        }

        if (GUI.Button(new Rect(10, 200, position.width - 20, 20), "Back")) {
            UIMakerCreateWindow window = (UIMakerCreateWindow)EditorWindow.GetWindow(typeof(UIMakerCreateWindow), false, "UI POPUP MAKER");
            window.Close();
            UIMakerMainMenuWindow windowMainMenu = (UIMakerMainMenuWindow)EditorWindow.GetWindow(typeof(UIMakerMainMenuWindow), false, "UI POPUP MAKER");
        }
        EditorGUI.EndDisabledGroup();
    }

    private void CreatePopUpPrefab() {
        AssetDatabase.CopyAsset(string.Format("Assets/ElpapagUIMaker/Resources/Elpapag/UITypePrefabs/Popup.prefab"), string.Format("Assets/ElpapagUIMaker/Resources/Elpapag/UIPrefabs/{0}.prefab", uiID));
        GameObject go = Resources.Load(string.Format("Elpapag/UIPrefabs/{0}", uiID)) as GameObject;
        EditorGUIUtility.PingObject(go);
    }

    private void CreateScreenPopUpPrefab() {
        AssetDatabase.CopyAsset(string.Format("Assets/ElpapagUIMaker/Resources/Elpapag/UITypePrefabs/Screen.prefab"), string.Format("Assets/ElpapagUIMaker/Resources/Elpapag/UIScreenPrefabs/{0}.prefab", uiID));
        GameObject go = Resources.Load(string.Format("Elpapag/UIScreenPrefabs/{0}", uiID)) as GameObject;
        EditorGUIUtility.PingObject(go);
    }

    private void AddUIComponent() {
        GameObject go = Resources.Load(string.Format("Elpapag/{0}/{1}", creatingType, uiID)) as GameObject;
        if (go == null) return;
        if (go.GetComponent(uiID) != null) return;
        go.AddComponentExt(uiID);
        //Set UIIDs
        if (go.GetComponent<UIPopup>() != null) go.GetComponent<UIPopup>().uiID = uiID;
        if (go.GetComponent<ScreenLocker>() != null) go.GetComponent<ScreenLocker>().uiID = uiID;

        if (canvasParent != null) {
            UIParentConfig.Instance.AddParent(uiID, canvasParentID);
            EditorUtility.SetDirty(UIParentConfig.Instance);
        }
        //PrefabUtility.ReplacePrefab(go, Resources.Load(string.Format("Elpapag/{0}/{1}", creatingType, go.name)) as GameObject, ReplacePrefabOptions.ConnectToPrefab);
    }

    private object ConvertStringToType(string inputString) {
        Debug.Log(Type.GetType(inputString));
        return System.Activator.CreateInstance(Type.GetType(inputString));
    }

    private void CreateUIPopupComponent() {
        string dataScriptsPath = string.Format("{0}/ElpapagUIMaker/UIPopUpMaker/Data/{1}.cs", Application.dataPath, uiID);
        string scriptTemplateTexts = string.Format("{0}/ElpapagUIMaker/UIPopUpMaker/ScriptTemplateTexts/PopupTemplate.txt", Application.dataPath);
        using (StreamWriter writer = new StreamWriter(dataScriptsPath)) {
            string uiEnumListScript;

            using (TextReader reader = File.OpenText(scriptTemplateTexts)) {
                uiEnumListScript = reader.ReadToEnd();
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(uiID);

            uiEnumListScript = uiEnumListScript.Replace("<popUpID>", builder.ToString());
            writer.Write(uiEnumListScript);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        CreatePopUpPrefab();
    }

    private void CreateUIScreenPopupComponent() {
        string dataScriptsPath = string.Format("{0}/ElpapagUIMaker/UIPopUpMaker/Data/{1}.cs", Application.dataPath, uiID);
        string scriptTemplateTexts = string.Format("{0}/ElpapagUIMaker/UIPopUpMaker/ScriptTemplateTexts/ScreenTemplate.txt", Application.dataPath);
        using (StreamWriter writer = new StreamWriter(dataScriptsPath)) {
            string uiEnumListScript;

            using (TextReader reader = File.OpenText(scriptTemplateTexts)) {
                uiEnumListScript = reader.ReadToEnd();
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(uiID);

            uiEnumListScript = uiEnumListScript.Replace("<popUpID>", builder.ToString());
            writer.Write(uiEnumListScript);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        CreateScreenPopUpPrefab();
    }

    private void UpdateUIEnumList() {
        string dataScriptsPath = string.Format("{0}/ElpapagUIMaker/UIPopUpMaker/Data/UIList.cs", Application.dataPath);
        string scriptTemplateTexts = string.Format("{0}/ElpapagUIMaker/UIPopUpMaker/ScriptTemplateTexts/UIListTemplate.txt", Application.dataPath);
        using (StreamWriter writer = new StreamWriter(dataScriptsPath)) {
            string uiEnumListScript;

            using (TextReader reader = File.OpenText(scriptTemplateTexts)) {
                uiEnumListScript = reader.ReadToEnd();
            }

            StringBuilder builder = new StringBuilder();

            GameObject[] uiPrefabs = Resources.LoadAll<GameObject>("Elpapag/UIPrefabs");
            for (int i = 0; i < uiPrefabs.Length; i++) {
                string lastIndexComma = "";
                if (i < (uiPrefabs.Length - 1)) {
                    lastIndexComma = ",";
                }
                builder.Append(string.Format("\t\t {0}{1} \n", uiPrefabs[i].name, lastIndexComma));
            }

            uiEnumListScript = uiEnumListScript.Replace("// ITEMS", builder.ToString());
            writer.Write(uiEnumListScript);
        }

        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
    }

    private void CreateHeaderBox(string headerText, Color32 color) {
        GUIStyle currentStyle = null;
        currentStyle = new GUIStyle(GUI.skin.box);
        currentStyle.normal.background = MakeTex(10, 10, color);
        currentStyle.normal.textColor = Color.white;
        currentStyle.fontStyle = FontStyle.Bold;
        GUILayout.Box(headerText, currentStyle, GUILayout.Width(position.width - 10));
    }

    private Texture2D MakeTex(int width, int height, Color col) {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i) {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

}
