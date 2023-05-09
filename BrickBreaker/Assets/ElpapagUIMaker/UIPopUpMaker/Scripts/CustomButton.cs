﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Button{
    [SerializeField, HideInInspector]
    public ButtonClickedEvent onClickEvent;

    public override void OnPointerClick(PointerEventData eventData) {
        base.OnPointerClick(eventData);
        if (onClickEvent != null) onClickEvent.Invoke();
    }
    
    public Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos) {
        //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        //Convert the screenpoint to ui rectangle local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        //Convert the local point to world point
        return parentCanvas.transform.TransformPoint(movePos);
    }
}
