using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockedBrick : MonoBehaviour {
    public int unlockCount;
    public UnityEvent onUnlocked;

    private int unlockCounter;

    private void OnEnable() {
        unlockCounter = unlockCount;
    }

    public void Unlock() {
        unlockCounter--;
        if (unlockCounter <= 0) {
            if (onUnlocked != null) onUnlocked.Invoke();
        }
    }
}
