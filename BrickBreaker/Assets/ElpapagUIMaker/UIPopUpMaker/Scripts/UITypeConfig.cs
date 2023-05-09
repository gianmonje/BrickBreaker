using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UITypeConfig", menuName = "UIMaker/UITypeConfig")]
public class UITypeConfig : ScriptableObject {
    private static UITypeConfig instance;
    public static UITypeConfig Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<UITypeConfig>("Elpapag/Config/UITypeConfig");
            }
            return instance;
        }
    }

    public GameObject[] UITypePrefabs {
        get {
            return Resources.LoadAll<GameObject>("Elpapag/UITypePrefabs"); 
        }
    }
    
}
