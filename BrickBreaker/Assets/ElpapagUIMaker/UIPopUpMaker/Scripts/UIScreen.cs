using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class UIScreen<T> : MonoBehaviour where T : Component {
    private static GameObject holder;
    private static GameObject layout;

    public static bool IsScreenShown {
        get {
            return holder.activeInHierarchy;
        }
    }

    #region Fields
    /// <summary>
    /// The instance.
    /// </summary>
    private static T instance;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the instance.
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<T>();
                if (instance == null) {
                    GameObject obj = Instantiate(Resources.Load(string.Format("Elpapag/UIScreenPrefabs/{0}", typeof(T).Name)) as GameObject);
                    obj.name = typeof(T).Name;
                    //instance = obj.AddComponent<T>();

                    //Add to Canvas if no parent
                    string uiID = obj.GetComponent<ScreenLocker>().uiID;
                    if (UIParentConfig.Instance.GetParent(uiID) == null) {
                        GameObject canvasGO = Instantiate(Resources.Load("Elpapag/MiscPrefabs/Canvas") as GameObject);
                        obj.transform.SetParent(canvasGO.transform);
                    } else {
                        obj.transform.SetParent(UIParentConfig.Instance.GetParent(uiID).transform);
                    }

                    obj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                    obj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
                    obj.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                }
            }
            return instance;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    protected virtual void Awake() {
        holder = transform.Find("Holder").gameObject;
        if (holder.transform.Find("Layout") != null) layout = holder.transform.Find("Layout").gameObject;
        Initialize();
    }

    protected virtual void Initialize() {

    }

    public UIScreen<T> Show() {
        holder.SetActive(true);
        return this;
    }

    public virtual void ShowScreen(float delay = 0) {
        if (delay <= 0) {
            holder.SetActive(true);
        } else {
            StartCoroutine(ShowDelay(delay));
        }
    }

    private IEnumerator ShowDelay(float delay) {
        yield return new WaitForSeconds(delay);
        holder.SetActive(true);
    }

    public virtual void Hide(bool destroyThis = false) {
        holder.SetActive(false);
        if (destroyThis) Destroy(gameObject);
    }

    #endregion
}
