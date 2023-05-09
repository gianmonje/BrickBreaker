using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupMaker : Singleton<UIPopupMaker> {

    public UIPopup Popup(UIList uIList, Transform parent = null) {
        Debug.Log(string.Format("Elpapag/UIPrefabs/{0}", uIList.ToString()));
        GameObject popupPrefab = Resources.Load<GameObject>(string.Format("Elpapag/UIPrefabs/{0}", uIList.ToString()));
        Debug.Log(popupPrefab);

        GameObject popupGO = Instantiate<GameObject>(popupPrefab, parent);
        popupGO.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        popupGO.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        popupGO.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        return popupGO.GetComponent<UIPopup>();
    }

}
