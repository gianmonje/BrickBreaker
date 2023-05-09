using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaker : Singleton<UIMaker>{

    public enum UIID {
        DoraPopup
    }

	public void OpenUI(UIID uiID) {
        GameObject ui = Instantiate(Resources.Load<GameObject>(string.Format("Elpapag/UIPrefabs/{0}", uiID)));
    }
}
