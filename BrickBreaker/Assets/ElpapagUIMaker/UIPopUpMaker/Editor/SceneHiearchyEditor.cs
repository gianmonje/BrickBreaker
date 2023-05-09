using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class SceneHiearchyEditor : Editor {

    static SceneHiearchyEditor() {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
    }

    private static void HierarchyItemCB(int instanceID, Rect selectionRect) {
        GameObject go = (GameObject)EditorUtility.InstanceIDToObject(instanceID);

        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;

        string[] scenes = new string[sceneCount];

        // create the menu and add items to it
        GenericMenu menu = new GenericMenu();
        GenericMenu menuAdd = new GenericMenu();
        for (int i = 0; i < scenes.Length; i++) {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            if (instanceID.GetHashCode() == SceneManager.GetSceneByName(scenes[i]).GetHashCode()) {
                currentOpenedScene = SceneManager.GetSceneByName(scenes[i]);
                
                Rect rect = new Rect(selectionRect);
                rect.x = rect.width - 60;
                rect.width = 42;
                Texture nextIcon = Resources.Load("Elpapag/Textures/arrow") as Texture;
                if (GUI.Button(new Rect(rect.x - 25, rect.y + 1.5f, rect.width - 20, rect.height - 2), nextIcon)) {
                    if (GetClosedScenesStringPath(i, true) != "") {
                        EditorSceneManager.OpenScene(GetClosedScenesStringPath(i, true), OpenSceneMode.Additive);
                        EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByBuildIndex(i), true);
                    }
                }
                Texture backIcon = Resources.Load("Elpapag/Textures/arrowback") as Texture;
                if (GUI.Button(new Rect(rect.x - 50, rect.y + 1.5f, rect.width - 20, rect.height - 2), backIcon)) {
                    if (GetClosedScenesStringPath(i, true) != "") {
                        EditorSceneManager.OpenScene(GetClosedScenesStringPath(i, false), OpenSceneMode.Additive);
                        EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByBuildIndex(i), true);
                    }
                }

                Texture listIcon = Resources.Load("Elpapag/Textures/list") as Texture;
                if (GUI.Button(new Rect(rect.x + 25, rect.y + 1.5f, rect.width - 20, rect.height - 2), listIcon)) {
                    //Build Generic Menu
                    for (int x = 0; x < sceneCount; x++) {
                        string scenePath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(x);
                        // forward slashes nest menu items under submenus
                        scenes[x] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(x));
                        Scene scene = SceneManager.GetSceneByName(scenes[x]);
                        if (!IsSceneOpen(scene)) {
                            MenuItemScene menuItemScene = new MenuItemScene();
                            menuItemScene.scenePath = scenePath;
                            menuItemScene.indexToClose = i;
                            AddMenuItem(menu, System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(x)), menuItemScene);
                        }
                    }
                    menu.ShowAsContext();
                }

                Texture addIcon = Resources.Load("Elpapag/Textures/add") as Texture;
                if (GUI.Button(new Rect(rect.x, rect.y + 1.5f, rect.width - 20, rect.height - 2), addIcon)) {
                    //Build Generic Menu

                    for (int x = 0; x < sceneCount; x++) {
                        string scenePath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(x);
                        // forward slashes nest menu items under submenus
                        scenes[x] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(x));
                        Scene scene = SceneManager.GetSceneByName(scenes[x]);
                        if (!IsSceneOpen(scene)) {
                            MenuItemScene menuItemScene = new MenuItemScene();
                            menuItemScene.scenePath = scenePath;
                            menuItemScene.indexToClose = i;
                            AddMenuItemAddScene(menuAdd, System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(x)), menuItemScene);
                        }
                    }
                    menuAdd.ShowAsContext();
                }
            }
        }
    }

    // serialize field on window so its value will be saved when Unity recompiles
    [SerializeField]
    static Scene currentOpenedScene;
    private struct MenuItemScene {
        public string scenePath;
        public int indexToClose;
    }
    
    // a method to simplify adding menu items
    private static void AddMenuItem(GenericMenu menu, string menuPath, MenuItemScene menuItemScene) {
        // the menu item is marked as selected if it matches the current value of m_Color
        menu.AddItem(new GUIContent(menuPath), currentOpenedScene.Equals(EditorSceneManager.GetSceneByPath(menuItemScene.scenePath)), OnSceneSelected, menuItemScene);
    }

    // a method to simplify adding menu items
    private static void AddMenuItemAddScene(GenericMenu menu, string menuPath, MenuItemScene menuItemScene) {
        // the menu item is marked as selected if it matches the current value of m_Color
        menu.AddItem(new GUIContent(menuPath), currentOpenedScene.Equals(EditorSceneManager.GetSceneByPath(menuItemScene.scenePath)), OnAddSceneSelected, menuItemScene);
    }

    // the GenericMenu.MenuFunction2 event handler for when a menu item is selected
    public static void OnSceneSelected(object menuItemScene) {
        MenuItemScene mS = (MenuItemScene)menuItemScene;
        EditorSceneManager.OpenScene(mS.scenePath, OpenSceneMode.Additive);
        EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByBuildIndex(mS.indexToClose), true);
    }

    // the GenericMenu.MenuFunction2 event handler for when a menu item is selected
    public static void OnAddSceneSelected(object menuItemScene) {
        MenuItemScene mS = (MenuItemScene)menuItemScene;
        EditorSceneManager.OpenScene(mS.scenePath, OpenSceneMode.Additive);
    }

    private static bool IsSceneOpen(Scene scene) {
        Scene[] openScenes = EditorSceneManager.GetAllScenes();

        bool isOpen = false;
        for (int x = 0; x < openScenes.Length; x++) {
            if (scene == openScenes[x]) {
                isOpen = true;
            }
        }
        return isOpen;
    }

    private static string GetClosedScenesStringPath(int index, bool isNext) {
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        string[] scenes = new string[sceneCount];

        if (isNext) {
            int counter = index;
            scenes[counter] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(counter));
            Scene scene = SceneManager.GetSceneByName(scenes[counter]);
            if (EditorSceneManager.GetAllScenes().Length >= sceneCount) return "";
            do {
                counter++;
                if (counter >= (scenes.Length)) {
                    counter = 0;
                }
                scenes[counter] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(counter));
                scene = SceneManager.GetSceneByName(scenes[counter]);
            } while (IsSceneOpen(scene));
            return UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(counter);
        } else {
            int counter = index;
            scenes[counter] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(counter));
            Scene scene = SceneManager.GetSceneByName(scenes[counter]);
            if (EditorSceneManager.GetAllScenes().Length >= sceneCount) return "";
            do {
                counter--;
                if (counter < 0) {
                    counter = scenes.Length - 1;
                }
                scenes[counter] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(counter));
                scene = SceneManager.GetSceneByName(scenes[counter]);
            } while (IsSceneOpen(scene));
            return UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(counter);
        }
        return "";
    }

}