using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class UISaver : Editor {
    private static float addToX = -3;

    private void Awake() {
        Debug.Log("Refreshed AssetDatabase");
        AssetDatabase.Refresh();
    }

    static UISaver() {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
    }


    private static void HierarchyItemCB(int instanceID, Rect selectionRect) {
        GameObject go = (GameObject)EditorUtility.InstanceIDToObject(instanceID);

        if (go == null) return;
        if (EditorApplication.isPlaying) return;

        GameObject screenGO = Resources.Load(string.Format("Elpapag/UIScreenPrefabs/{0}", go.name)) as GameObject;

        if (go.GetComponent<UIPopup>() == null && screenGO == null) return;

        Rect rect = new Rect(selectionRect);
        rect.x = rect.width - 60;
        rect.width = 42;

        GUIStyle toggleStyle = new GUIStyle() { normal = new GUIStyleState() { background = EditorGUIUtility.Load("Icons/animationvisibilitytoggleoff.png") as Texture2D }, onNormal = new GUIStyleState() { background = EditorGUIUtility.Load("Icons/animationvisibilitytoggleon.png") as Texture2D }, fixedHeight = 11, fixedWidth = 13, border = new RectOffset(2, 2, 2, 2), overflow = new RectOffset(-1, 1, -2, 2), padding = new RectOffset(3, 3, 3, 3), richText = false, stretchHeight = false, stretchWidth = false, };

        if (screenGO != null) {
            GUIStyle emptyButtonStyle = new GUIStyle();
            Texture lockIcon = (screenGO.GetComponent<ScreenLocker>().isLocked) ? Resources.Load("Elpapag/Textures/Lock") as Texture : Resources.Load("Elpapag/Textures/UnLock") as Texture;
            if (GUI.Button(new Rect(rect.x + rect.width + 50, rect.y + 4, rect.width - 33, rect.height - 7), lockIcon, emptyButtonStyle)) {
                if (screenGO.GetComponent<ScreenLocker>().isLocked) {
                    screenGO.GetComponent<ScreenLocker>().isLocked = false;
                } else {
                    screenGO.GetComponent<ScreenLocker>().isLocked = true;
                }
            }

            if (screenGO.GetComponent<ScreenLocker>().isLocked) {
                go.GetComponent<ScreenLocker>().transform.Find("Holder").gameObject.SetActive(EditorGUI.Toggle(new Rect(rect.x + 72, rect.y + 2, rect.width - 18, rect.height), go.GetComponent<ScreenLocker>().transform.Find("Holder").gameObject.activeSelf, toggleStyle));
            }
        }

        if (screenGO != null) if (screenGO.GetComponent<ScreenLocker>().isLocked && screenGO != null) return;

        if (go.transform.parent == null || go.transform.parent.GetComponent<Canvas>() == null) {
            GUI.Box(new Rect(selectionRect.x - 1.5f, selectionRect.y, selectionRect.width - 50, selectionRect.height), "", GetStyle());
            Texture warningIcon = Resources.Load("Elpapag/Textures/warning") as Texture;
            GUI.Button(new Rect(rect.x + rect.width - 25 + addToX, rect.y, rect.width - 18, rect.height), new GUIContent("", warningIcon, "No Parent Canvas"), new GUIStyle());
        }

        EditorGUI.BeginDisabledGroup(go.transform.parent == null || go.transform.parent.GetComponent<Canvas>() == null);
        {
            if (go.transform.parent != null && go.transform.parent.GetComponent<Canvas>() != null) {
                Texture setParentIcon = Resources.Load("Elpapag/Textures/parent") as Texture;
                Texture visibilityIconOpen = Resources.Load("Elpapag/Textures/eye_open") as Texture;
                Texture visibilityIconClose = Resources.Load("Elpapag/Textures/eye_close") as Texture;

                if (go.GetComponent<UIPopup>() != null && go.transform.parent.GetComponent<Canvas>() != null) {
                    if (UIParentConfig.Instance.GetParent(go.GetComponent<UIPopup>().uiID) == null) {
                        if (GUI.Button(new Rect(rect.x + rect.width - 25 + addToX, rect.y, rect.width - 18, rect.height), new GUIContent("", setParentIcon, "Set As Parent"))) {
                            UIParentConfig.Instance.AddParent(go.GetComponent<UIPopup>().uiID, UIParentConfig.Instance.GetParentCanvasID(go));
                            ChangeTemporaryCanvas(go);
                        }
                    } else {
                        if (UIParentConfig.Instance.GetParent(go.GetComponent<UIPopup>().uiID).GetInstanceID() != go.transform.parent.GetInstanceID()) {
                            if (GUI.Button(new Rect(rect.x + rect.width - 25 + addToX, rect.y, rect.width - 18, rect.height), new GUIContent("", setParentIcon, "Set As Parent"))) {
                                UIParentConfig.Instance.SetParent(go.GetComponent<UIPopup>().uiID, UIParentConfig.Instance.GetParentCanvasID(go));
                                ChangeTemporaryCanvas(go);
                            }
                        }
                    }
                } else {
                    if (UIParentConfig.Instance.GetParent(go.GetComponent<ScreenLocker>().uiID) == null && go.transform.parent.GetComponent<Canvas>() != null) {
                        if (GUI.Button(new Rect(rect.x + rect.width - 25 + addToX, rect.y, rect.width - 18, rect.height), new GUIContent("", setParentIcon, "Set As Parent"))) {
                            UIParentConfig.Instance.AddParent(go.GetComponent<ScreenLocker>().uiID, UIParentConfig.Instance.GetParentCanvasID(go));
                            ChangeTemporaryCanvas(go);
                        }

                        go.GetComponent<ScreenLocker>().transform.Find("Holder").gameObject.SetActive(EditorGUI.Toggle(new Rect(rect.x + rect.width - 42 + addToX, rect.y + 2, rect.width - 18, rect.height), go.GetComponent<ScreenLocker>().transform.Find("Holder").gameObject.activeSelf, toggleStyle));
                    } else {
                        if (UIParentConfig.Instance.GetParent(go.GetComponent<ScreenLocker>().uiID).GetInstanceID() != go.transform.parent.GetInstanceID()) {
                            if (GUI.Button(new Rect(rect.x + rect.width - 25 + addToX, rect.y, rect.width - 18, rect.height), new GUIContent("", setParentIcon, "Set As Parent"))) {
                                UIParentConfig.Instance.SetParent(go.GetComponent<ScreenLocker>().uiID, UIParentConfig.Instance.GetParentCanvasID(go));
                                ChangeTemporaryCanvas(go);
                            }
                            go.GetComponent<ScreenLocker>().transform.Find("Holder").gameObject.SetActive(EditorGUI.Toggle(new Rect(rect.x + rect.width - 42 + addToX, rect.y + 2, rect.width - 18, rect.height), go.GetComponent<ScreenLocker>().transform.Find("Holder").gameObject.activeSelf, toggleStyle));
                        } else {
                            go.GetComponent<ScreenLocker>().transform.Find("Holder").gameObject.SetActive(EditorGUI.Toggle(new Rect(rect.x + rect.width - 18 + addToX, rect.y + 2, rect.width - 18, rect.height), go.GetComponent<ScreenLocker>().transform.Find("Holder").gameObject.activeSelf, toggleStyle));
                        }
                    }
                }
            }

            Texture saveIcon = Resources.Load("Elpapag/Textures/save") as Texture;
            if (GUI.Button(new Rect(rect.x + rect.width + addToX, rect.y, rect.width - 18, rect.height), new GUIContent("", saveIcon, "Save"))) {
                if (go.GetComponent<UIPopup>() != null) {
                    PrefabUtility.ReplacePrefab(go, Resources.Load(string.Format("Elpapag/UIPrefabs/{0}", go.name)) as GameObject, ReplacePrefabOptions.ConnectToPrefab);
                } else {
                    PrefabUtility.ReplacePrefab(go, Resources.Load(string.Format("Elpapag/UIScreenPrefabs/{0}", go.name)) as GameObject, ReplacePrefabOptions.ConnectToPrefab);
                }
            }
        }
        EditorGUI.EndDisabledGroup();

        Texture closeIcon = Resources.Load("Elpapag/Textures/close") as Texture;
        if (GUI.Button(new Rect(rect.x + rect.width + 25 + addToX, rect.y, rect.width - 18, rect.height), new GUIContent("", closeIcon, "Close"))) {
            UIMakerMainMenuWindow windowMainMenu = (UIMakerMainMenuWindow)EditorWindow.GetWindow(typeof(UIMakerMainMenuWindow), false, "UI POPUP MAKER");

            if (go.GetComponent<UIPopup>() != null) {
                if (go.GetComponent<UIPopup>().temporaryCanvas != null) {
                    GameObject temporaryCanvas = go.GetComponent<UIPopup>().temporaryCanvas;
                    go.GetComponent<UIPopup>().temporaryCanvas = null;
                    DestroyImmediate(temporaryCanvas);
                    if (go != null) DestroyImmediate(go);
                } else {
                    DestroyImmediate(go);
                }
            } else {
                if (go.GetComponent<ScreenLocker>().temporaryCanvas != null) {
                    GameObject temporaryCanvas = go.GetComponent<ScreenLocker>().temporaryCanvas;
                    go.GetComponent<ScreenLocker>().temporaryCanvas = null;
                    DestroyImmediate(temporaryCanvas);
                    if (go != null) DestroyImmediate(go);
                } else {
                    DestroyImmediate(go);
                }
            }
        }
    }

    private static GUIStyle GetStyle() {
        GUIStyle style = new GUIStyle();
        style = new GUIStyle(GUI.skin.box);
        style.normal.background = MakeTex(2, 2, new Color32(255, 0, 0, 60));
        return style;
    }

    private static Texture2D MakeTex(int width, int height, Color col) {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i) {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    private static void ChangeTemporaryCanvas(GameObject go) {
        if (go.GetComponent<UIPopup>() != null) {
            if (go.GetComponent<UIPopup>().temporaryCanvas != null) {
                go.transform.parent.name = "Canvas";
                go.GetComponent<UIPopup>().temporaryCanvas = null;
            }
        } else {
            if (go.GetComponent<ScreenLocker>().temporaryCanvas != null) {
                go.transform.parent.name = "Canvas";
                go.GetComponent<ScreenLocker>().temporaryCanvas = null;
            }
        }
    }

}
