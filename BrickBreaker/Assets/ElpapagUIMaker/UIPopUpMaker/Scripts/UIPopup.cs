using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPopup : MonoBehaviour {
    [HideInInspector]
    public string uiID;
    [HideInInspector]
    public GameObject temporaryCanvas;

    private GameObject holder;
    private GameObject layout;
    private GameObject imageHolder;
    private GameObject buttonParent;
    private GameObject buttonPrefab;
    private GameObject headerText;
    private GameObject captionText;

    public void Awake() {
        if (transform.Find("Holder") != null) holder = transform.Find("Holder").gameObject;
        if (holder.transform.Find("Layout") != null) layout = holder.transform.Find("Layout").gameObject;
        if (layout.transform.Find("ImageHolder") != null) imageHolder = layout.transform.Find("ImageHolder").gameObject;
        if (layout.transform.Find("ButtonParent") != null) buttonParent = layout.transform.Find("ButtonParent").gameObject;
        if (buttonParent != null) if (buttonParent.transform.Find("ButtonPrefab") != null) buttonPrefab = buttonParent.transform.Find("ButtonPrefab").gameObject;
        if (layout.transform.Find("Header") != null) headerText = layout.transform.Find("Header").gameObject;
        if (layout.transform.Find("Caption") != null) captionText = layout.transform.Find("Caption").gameObject;
    }

    public virtual UIPopup Show(string header = "", string caption = "", Sprite imageSprite = null) {
        //Add to Canvas if no parent
        if (UIParentConfig.Instance.GetParent(uiID) == null) {
            GameObject canvasGO = Instantiate(Resources.Load("Elpapag/MiscPrefabs/TemporaryCanvas") as GameObject);
            transform.SetParent(canvasGO.transform);
        } else {
            transform.SetParent(UIParentConfig.Instance.GetParent(uiID).transform);
        }

        GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

        holder.SetActive(true);
        if (header != "") headerText.GetComponent<Text>().text = header;
        if (caption != "") captionText.GetComponent<Text>().text = caption;
        if (imageSprite != null) {
            if (layout.transform.Find("ImageHolder") != null) imageHolder.transform.Find("ImageHolder").GetComponent<Image>().sprite = imageSprite;
        } else {
            if (layout.transform.Find("ImageHolder") != null) imageHolder.SetActive(false);
        }
        buttonPrefab.SetActive(false);
        return this;
    }

    public UIPopup AddButton(string buttonText, Sprite buttonSprite, Color32? color, UnityAction onClickEvent = null) {
        GameObject bgo = InstantiateButton();
        ButtonCreate(bgo, buttonText, onClickEvent);
        if (buttonSprite != null) bgo.GetComponentInChildren<Image>().sprite = buttonSprite;
        if (color != null) bgo.GetComponentInChildren<Image>().color = color ?? Color.white;

        return this;
    }

    public UIPopup AddButton(string buttonText, Sprite buttonSprite = null, UnityAction onClickEvent = null) {
        GameObject bgo = InstantiateButton();
        ButtonCreate(bgo, buttonText, onClickEvent);
        if (buttonSprite != null) bgo.GetComponentInChildren<Image>().sprite = buttonSprite;
        return this;
    }

    public UIPopup AddButton(string buttonText, Color32 color, UnityAction onClickEvent = null) {
        GameObject bgo = InstantiateButton();
        ButtonCreate(bgo, buttonText, onClickEvent);
        bgo.GetComponentInChildren<Image>().color = color;
        return this;
    }

    public UIPopup AddButton(string buttonText, UnityAction onClickEvent = null) {
        GameObject bgo = InstantiateButton();
        ButtonCreate(bgo, buttonText, onClickEvent);
        return this;
    }

    public void ButtonCreate(GameObject bgo, string buttonText, UnityAction onClickEvent = null) {
        bgo.GetComponentInChildren<Text>().text = buttonText;
        if (onClickEvent != null) bgo.GetComponent<Button>().onClick.AddListener(onClickEvent);
        bgo.GetComponent<Button>().onClick.AddListener(() => {
            Hide();
            bgo.GetComponent<Button>().onClick.RemoveAllListeners();
        });
    }

    public void HideHeader() {
        if (layout.transform.Find("Header") != null) headerText.SetActive(false);
    }

    public void HideCaption() {
        if (layout.transform.Find("Caption") != null) captionText.SetActive(false);
    }

    public void HideImage() {
        if (layout.transform.Find("ImageHolder") != null) imageHolder.SetActive(false);
    }

    private GameObject InstantiateButton() {
        GameObject bgo = Instantiate(buttonPrefab, buttonParent.transform);
        bgo.SetActive(true);
        return bgo;
    }

    public void Hide() {
        holder.SetActive(false);
        Destroy(gameObject);
    }
}
