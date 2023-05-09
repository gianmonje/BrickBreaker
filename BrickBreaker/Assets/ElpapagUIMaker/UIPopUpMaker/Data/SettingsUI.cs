using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SettingsUI : UIScreen<SettingsUI> {

    public Slider notificationToggle;
    public void OnToggleNotification() {
        if (notificationToggle.value == 0) {
            notificationToggle.value = 1;
        } else {
            notificationToggle.value = 0;
        }
    }
}