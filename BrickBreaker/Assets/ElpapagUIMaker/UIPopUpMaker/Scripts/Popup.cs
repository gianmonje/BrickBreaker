using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Popup : Singleton<Popup> {
    private GameObject holder;
    private GameObject layout;
    private GameObject buttonParent;
    private GameObject buttonPrefab;
    private GameObject headerText;
    private GameObject captionText;
    
    private void Awake() {
        holder = transform.Find("Holder").gameObject;
        layout = holder.transform.Find("Layout").gameObject;
        headerText = layout.transform.Find("Header").gameObject;
        captionText = layout.transform.Find("Caption").gameObject;
        buttonParent = layout.transform.Find("ButtonParent").gameObject;
        buttonPrefab = buttonParent.transform.Find("ButtonPrefab").gameObject;
    }
    
    public Popup Show(string header = "", string caption = "") {
        holder.SetActive(true);
        headerText.GetComponent<Text>().text = header;
        captionText.GetComponent<Text>().text = caption;
        buttonPrefab.SetActive(false);
        return this;
    }

    public Popup AddButton(string buttonText, UnityAction onClickEvent = null) {
        GameObject bgo = Instantiate(buttonPrefab, buttonParent.transform);
        bgo.SetActive(true);
        bgo.GetComponentInChildren<Text>().text = buttonText;
        if(onClickEvent != null) bgo.GetComponent<Button>().onClick.AddListener(onClickEvent);
        bgo.GetComponent<Button>().onClick.AddListener(()=> {
            Hide();
            bgo.GetComponent<Button>().onClick.RemoveAllListeners();
        });
        return this;
    }

    private void Hide() {

        holder.SetActive(false);
        Destroy(gameObject);
    }

  
}
