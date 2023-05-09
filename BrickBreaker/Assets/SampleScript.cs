using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScript : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    public void OnClickShowPopup() {
        SamplePopup samplePopup = (SamplePopup)UIPopupMaker.Instance.Popup(UIList.SamplePopup);
        samplePopup.AddButton("Yes", delegate { Debug.Log("I CLICKED YES!"); });
        samplePopup.AddButton("No", delegate { Debug.Log("I CLICKED NO!"); });
        samplePopup.Show("Hello There!", "Would you like to try?");
    }

    public void OnClickShowScreen() {
        //MainMenu.Instance.ShowScreen();
    }


}
