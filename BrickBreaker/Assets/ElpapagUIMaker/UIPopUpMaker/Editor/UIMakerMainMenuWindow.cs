using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;

public class UIMakerMainMenuWindow : EditorWindow {
    private UIPopup[] uiPopups;
    private ReorderableList popUpReordableList;

    private Object[] uiScreenPopups;
    private ReorderableList screenPopUpReordableList;

    private bool isDirty = false;

    private void OnEnable() {
        uiPopups = Resources.LoadAll<UIPopup>(string.Format("Elpapag/UIPrefabs/"));
        uiScreenPopups = Resources.LoadAll(string.Format("Elpapag/UIScreenPrefabs/"));

        PopupReordableListInit();
        ScreenPopupReordableListInit();
    }

    private void PopupReordableListInit() {
        uiPopups = Resources.LoadAll<UIPopup>(string.Format("Elpapag/UIPrefabs/"));

        popUpReordableList = new UnityEditorInternal.ReorderableList(uiPopups, typeof(UIPopup), true, true, true, true);
        popUpReordableList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "POP-UPS");
        };

        popUpReordableList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) => {
                rect.y += 2;
                if (!EditorApplication.isCompiling) {
                    EditorGUI.LabelField(new Rect(rect.x, rect.y, position.width - 30, EditorGUIUtility.singleLineHeight), string.Format("{0}", uiPopups[index].name));
                    EditButton(uiPopups[index].name, rect, "UIPrefabs");
                }
            };

        popUpReordableList.onAddCallback = (UnityEditorInternal.ReorderableList l) => {
            Canvas[] canvases = FindObjectsOfType<Canvas>();
            for (int i = 0; i < canvases.Length; i++) {
                if (canvases[i].GetComponent<CanvasID>() == null) {
                    canvases[i].gameObject.AddComponent<CanvasID>().GenerateRandomID();
                }
            }

            UIMakerCreateWindow window = (UIMakerCreateWindow)EditorWindow.GetWindow(typeof(UIMakerCreateWindow), false, "UI POPUP MAKER");
            UIMakerMainMenuWindow windowMainMenu = (UIMakerMainMenuWindow)EditorWindow.GetWindow(typeof(UIMakerMainMenuWindow), false, "UI POPUP MAKER");
            // windowMainMenu.Close();
        };


        popUpReordableList.onRemoveCallback = (ReorderableList l) => {
            if (EditorUtility.DisplayDialog("Warning!", "Are you sure you want to delete?", "Yes", "No")) {
                RemovePopup(l.index);
            }
        };
    }

    private void ScreenPopupReordableListInit() {
        uiScreenPopups = Resources.LoadAll(string.Format("Elpapag/UIScreenPrefabs/"));

        screenPopUpReordableList = new UnityEditorInternal.ReorderableList(uiScreenPopups, typeof(Object), true, true, true, true);
        screenPopUpReordableList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "SCREEN POP-UPS");
        };

        GUIStyle toggleStyle = new GUIStyle() { normal = new GUIStyleState() { background = EditorGUIUtility.Load("Icons/animationvisibilitytoggleoff.png") as Texture2D }, onNormal = new GUIStyleState() { background = EditorGUIUtility.Load("Icons/animationvisibilitytoggleon.png") as Texture2D }, fixedHeight = 11, fixedWidth = 13, border = new RectOffset(2, 2, 2, 2), overflow = new RectOffset(-1, 1, -2, 2), padding = new RectOffset(3, 3, 3, 3), richText = false, stretchHeight = false, stretchWidth = false, };


        screenPopUpReordableList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) => {
                rect.y += 2;
                if (!EditorApplication.isCompiling) {
                    GameObject screenObject = uiScreenPopups[index] as GameObject;

                    ScreenLocker[] screenLockers = FindObjectsOfType<ScreenLocker>();
                    for (int i = 0; i < screenLockers.Length; i++) {
                        if (screenLockers[i].uiID == screenObject.GetComponent<ScreenLocker>().uiID) {
                            screenLockers[i].transform.Find("Holder").gameObject.SetActive(EditorGUI.Toggle(new Rect(rect.x, rect.y, 20, EditorGUIUtility.singleLineHeight), screenLockers[i].transform.Find("Holder").gameObject.activeSelf, toggleStyle));
                        }
                    }

                    EditorGUI.LabelField(new Rect(rect.x + 20, rect.y, position.width - 30, EditorGUIUtility.singleLineHeight), string.Format("{0}", uiScreenPopups[index].name));
                    EditButton(uiScreenPopups[index].name, rect, "UIScreenPrefabs");
                }
            };

        screenPopUpReordableList.onAddCallback = (UnityEditorInternal.ReorderableList l) => {
            Canvas[] canvases = FindObjectsOfType<Canvas>();
            for (int i = 0; i < canvases.Length; i++) {
                if (canvases[i].GetComponent<CanvasID>() == null) {
                    canvases[i].gameObject.AddComponent<CanvasID>().GenerateRandomID();
                }
            }

            UIMakerCreateWindow window = (UIMakerCreateWindow)EditorWindow.GetWindow(typeof(UIMakerCreateWindow), false, "UI POPUP MAKER");
            UIMakerMainMenuWindow windowMainMenu = (UIMakerMainMenuWindow)EditorWindow.GetWindow(typeof(UIMakerMainMenuWindow), false, "UI POPUP MAKER");
            // windowMainMenu.Close();
        };


        screenPopUpReordableList.onRemoveCallback = (ReorderableList l) => {
            if (EditorUtility.DisplayDialog("Warning!", "Are you sure you want to delete?", "Yes", "No")) {
                RemoveScreenPopup(l.index);
            }
        };
    }

    private void RemovePopup(int index) {
        UIParentConfig.Instance.Remove(uiPopups[index].uiID);
        FileUtil.DeleteFileOrDirectory(string.Format("{0}/ElpapagUIMaker/Resources/Elpapag/UIPrefabs/{1}.prefab", Application.dataPath, uiPopups[index].name));
        FileUtil.DeleteFileOrDirectory(string.Format("{0}/ElpapagUIMaker/UIPopUpMaker/Data/{1}.cs", Application.dataPath, uiPopups[index].name));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        uiPopups = Resources.LoadAll<UIPopup>(string.Format("Elpapag/UIPrefabs/"));
        UpdateUIEnumList();
    }

    private void RemoveScreenPopup(int index) {
        GameObject goScreen = uiScreenPopups[index] as GameObject;
        UIParentConfig.Instance.Remove(goScreen.GetComponent<ScreenLocker>().uiID);
        FileUtil.DeleteFileOrDirectory(string.Format("{0}/ElpapagUIMaker/Resources/Elpapag/UIScreenPrefabs/{1}.prefab", Application.dataPath, uiScreenPopups[index].name));
        FileUtil.DeleteFileOrDirectory(string.Format("{0}/ElpapagUIMaker/UIPopUpMaker/Data/{1}.cs", Application.dataPath, uiScreenPopups[index].name));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        uiScreenPopups = Resources.LoadAll(string.Format("Elpapag/UIScreenPrefabs/"));
    }

    private void OnGUI() {
        if (EditorApplication.isCompiling) {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUI.LabelField(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Creating UI Please Wait...", style);
            return;
        }

        MainMenu();
    }

    private void EditButton(string uiID, Rect rect, string folderLocation) {
        GameObject go = null;
        if (GUI.Button(new Rect(rect.x + position.width - 70, rect.y, 40, EditorGUIUtility.singleLineHeight), "Edit")) {
            //Instantiate On Scene MiscPrefabs
            GameObject prefab = Resources.Load(string.Format("Elpapag/{0}/{1}", folderLocation, uiID)) as GameObject;
            go = DuplicatePrefab(prefab);
            go.name = uiID;

            if (UIParentConfig.Instance.GetParent(uiID) == null) {
                GameObject canvasGO = Instantiate(Resources.Load("Elpapag/MiscPrefabs/TemporaryCanvas") as GameObject);
                canvasGO.name = "TemporaryCanvas";
                go.transform.SetParent(canvasGO.transform);
                if (go.GetComponent<UIPopup>() != null) {
                    go.GetComponent<UIPopup>().temporaryCanvas = canvasGO;
                } else if (go.GetComponent<ScreenLocker>() != null) {
                    go.GetComponent<ScreenLocker>().temporaryCanvas = canvasGO;
                }
            } else {
                go.transform.SetParent(UIParentConfig.Instance.GetParent(uiID).transform);
            }

            go.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            go.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            go.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

            Selection.activeGameObject = go;
            EditorGUIUtility.PingObject(go);
            EditorUtility.SetDirty(UIParentConfig.Instance);
            isDirty = false;

            PrefabUtility.ApplyPrefabInstance(go, InteractionMode.UserAction);
            //UIMakerMainMenuWindow windowMainMenu = (UIMakerMainMenuWindow)EditorWindow.GetWindow(typeof(UIMakerMainMenuWindow), false, "UI POPUP MAKER");
            //windowMainMenu.Close();
        }
    }

    private GameObject DuplicatePrefab(UnityEngine.GameObject go) {
        // FYI:  Don't need to call this if go is already a prefab:
        //UnityEngine.Object prefab = UnityEditor.PrefabUtility.GetPrefabObject( go );
        return UnityEditor.PrefabUtility.InstantiatePrefab(go, EditorSceneManager.GetActiveScene()) as GameObject;
    }

    private void MainMenu() {
        CreateHeaderBox("MAIN MENU", new Color32(51, 145, 255, 255));

        popUpReordableList.DoLayoutList();
        screenPopUpReordableList.DoLayoutList();
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

    private void UpdateUIEnumList() {
        string dataScriptsPath = string.Format("{0}/ElpapagUIMaker/UIPopUpMaker/Data/UIList.cs", Application.dataPath);
        string scriptTemplateTexts = string.Format("{0}/ElpapagUIMaker/UIPopUpMaker/ScriptTemplateTexts/UIListTemplate.txt", Application.dataPath);
        using (StreamWriter writer = new StreamWriter(dataScriptsPath)) {
            string uiEnumListScript;

            using (TextReader reader = File.OpenText(scriptTemplateTexts)) {
                uiEnumListScript = reader.ReadToEnd();
            }

            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            GameObject[] uiPrefabs = Resources.LoadAll<GameObject>("Elpapag/UIPrefabs");
            for (int i = 0; i < uiPrefabs.Length; i++) {
                string lastIndexComma = "";
                if (i < (uiPrefabs.Length - 1)) {
                    lastIndexComma = ",";
                }
                builder.Append(string.Format("\t\t {0}{1}", uiPrefabs[i].name, lastIndexComma));
            }

            uiEnumListScript = uiEnumListScript.Replace("// ITEMS", builder.ToString());
            writer.Write(uiEnumListScript);
        }

        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
    }

}
