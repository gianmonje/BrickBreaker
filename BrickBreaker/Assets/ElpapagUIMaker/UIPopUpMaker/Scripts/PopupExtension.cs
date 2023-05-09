using System;
using UnityEngine.UI;

public static class PopupExtension {
    public static Text AddButton(this Text value, string buttonText) {
        return value;
    }

    public static int MultiplyBy(this int value, int mulitiplier) {
        // Uses a second parameter after the instance parameter.
        return value * mulitiplier;
    }
}