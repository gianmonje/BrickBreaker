using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DailyRewardScreen : UIScreen<DailyRewardScreen> {
    public void ShowRewardClaimPopup() {
        RewardClaimPopup rewardClaimPopup = (RewardClaimPopup)UIPopupMaker.Instance.Popup(UIList.RewardClaimPopup, transform.parent);
    }
}