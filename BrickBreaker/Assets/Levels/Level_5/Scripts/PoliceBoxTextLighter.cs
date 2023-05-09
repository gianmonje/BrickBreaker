using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoliceBoxTextLighter : MonoBehaviour {
    public UnityEvent OnLitUpCompletely;

    private SpriteMask spriteMask { get { return GetComponent<SpriteMask>(); } }

    [System.Serializable]
    public class MaskFillData {
        public Vector3 maskPos;
        public Vector3 maskScale;
        [HideInInspector]
        public Transform maskTransform;

        [Button("Set To This Value"), BoxGroup("NoTitle", false)]
        public void SetToThisColliderValue() {
            maskTransform.transform.localPosition = maskPos;
            maskTransform.transform.localScale = maskScale;
        }
    }
    [SerializeField]
    private List<MaskFillData> maskFillDataList;
    private int lightIndex = 0;

    [Button("Get Current Transform Values"), BoxGroup("NoTitle", false)]
    public void GetMaskValue() {
        MaskFillData maskFillData = new MaskFillData();
        maskFillData.maskPos = transform.localPosition;
        maskFillData.maskScale = transform.localScale;
        maskFillDataList.Add(maskFillData);
    }

    public void HandleLight() {
        lightIndex++;
        if (lightIndex >= maskFillDataList.Count) {
            if (OnLitUpCompletely != null) OnLitUpCompletely.Invoke();
            return;
        }

        transform.localPosition = maskFillDataList[lightIndex].maskPos;
        transform.localScale = maskFillDataList[lightIndex].maskScale;
    }

    private void OnEnable() {
        lightIndex = 0;
        transform.localPosition = maskFillDataList[lightIndex].maskPos;
        transform.localScale = maskFillDataList[lightIndex].maskScale;
    }

    [Button("Set Mask Transform"), BoxGroup("NoTitle", false)]
    public void SetMaskTransfrom() {
        for (int i = 0; i < maskFillDataList.Count; i++) {
            maskFillDataList[i].maskTransform = this.transform;
            maskFillDataList[i].maskPos = new Vector3(this.transform.localPosition.x, maskFillDataList[i].maskPos.y, maskFillDataList[i].maskPos.z);
        }
    }

}
