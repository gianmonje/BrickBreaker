using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIParentConfig", menuName = "UIMaker/UIParentConfig")]
public class UIParentConfig : ScriptableObject {
    private static UIParentConfig instance;
    public static UIParentConfig Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<UIParentConfig>("Elpapag/Config/UIParentConfig");
            }
            return instance;
        }
    }

    [System.Serializable]
    public class UIParent {
        public string uiID;
        public string parentID;
    }
    public List<UIParent> uiParents;

    public void AddParent(string uiID, string parentID) {
        UIParentConfig.UIParent uIParent = new UIParentConfig.UIParent();
        uIParent.uiID = uiID;
        uIParent.parentID = parentID;
        uiParents.Add(uIParent);
    }

    public Transform GetParent(string uiID) {
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        for (int i = 0; i < uiParents.Count; i++) {
            if (uiParents[i].uiID == uiID) {
                for (int x = 0; x < canvases.Length; x++) {
                    if (canvases[x].GetComponent<CanvasID>() != null) {
                        if (canvases[x].GetComponent<CanvasID>().ID == uiParents[i].parentID) {
                            return canvases[x].transform;
                        }
                    }
                }
            }
        }
        return null;
    }

    public void SetParent(string uiID, string parentID) {
        CanvasID[] canvasIDs = FindObjectsOfType<CanvasID>();
        for (int i = 0; i < uiParents.Count; i++) {
            if (uiParents[i].uiID == uiID) {
                uiParents[i].parentID = parentID;
            }
        }
    }

    public void Remove(string uiID) {
        for (int i = 0; i < uiParents.Count; i++) {
            if (uiParents[i].uiID == uiID) {
                uiParents.Remove(uiParents[i]);
            }
        }
    }

    public string GetParentCanvasID(GameObject popupChild) {
        if (popupChild.transform.parent.GetComponent<CanvasID>() == null) {
            popupChild.transform.parent.gameObject.AddComponent<CanvasID>().GenerateRandomID();
            return popupChild.transform.parent.gameObject.GetComponent<CanvasID>().ID;
        }
        return popupChild.transform.parent.gameObject.GetComponent<CanvasID>().ID;
    }

    public string GetCanvasID(GameObject canvas) {
        if (canvas.GetComponent<CanvasID>() == null) {
            canvas.AddComponent<CanvasID>().GenerateRandomID();
            return canvas.GetComponent<CanvasID>().ID;
        }
        return canvas.GetComponent<CanvasID>().ID;
    }


}
