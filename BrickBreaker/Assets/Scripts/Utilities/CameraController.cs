using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public bool MaintainWidth = true;
    public Vector3 CameraPos;
    public float DefaultWidth;
    public float DefaultHeight;
    void Start() {
        //float aspectRatioDesign = (9f / 16f);
        //float orthographicStartSize = 3.8f;

        //float inverseAspectRatio = 1 / aspectRatioDesign;
        //float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        //if (currentAspectRatio > aspectRatioDesign) {
        //    currentAspectRatio -= (currentAspectRatio - aspectRatioDesign);
        //} else if (currentAspectRatio < inverseAspectRatio) {
        //    currentAspectRatio += (currentAspectRatio - inverseAspectRatio);
        //}

        //Camera.main.orthographicSize = aspectRatioDesign * (orthographicStartSize / currentAspectRatio);
    }
}
