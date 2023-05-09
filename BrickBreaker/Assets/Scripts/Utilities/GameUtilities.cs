using System;
using System.Collections.Generic;
using UnityEngine;

public class EnumParser {
    public static T ParseEnum<T>(string value) {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}

public class ScreenSize {
    public static float GetScreenToWorldHeight {
        get {
            Vector2 topRightCorner = new Vector2(1, 1);
            Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
            var height = edgeVector.y * 2;
            return height;
        }
    }
    public static float GetScreenToWorldWidth {
        get {
            Vector2 topRightCorner = new Vector2(1, 1);
            Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
            var width = edgeVector.x * 2;
            return width;
        }
    }
}

public class ShuffleList {
    static public void Shuffle<T>(List<T> a) {
        // Loop array
        for (int i = a.Count - 1; i > 0; i--) {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = UnityEngine.Random.Range(0, i);

            // Save the value of the current i, otherwise it'll overwrite when we swap the values
            T temp = a[i];

            // Swap the new and old values
            a[i] = a[rnd];
            a[rnd] = temp;
        }

        // Print
        for (int i = 0; i < a.Count; i++) {
            Debug.Log(a[i]);
        }
    }
}