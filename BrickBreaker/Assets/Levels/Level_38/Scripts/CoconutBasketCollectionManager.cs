using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutBasketCollectionManager : MonoBehaviour {
    public MoveToTarget[] coconutBasketCollection;
    public MoveToTarget[] coconutBasketCollectionInit;
    public float basketMoveDelay;
    public Transform startTarget;
    public Transform endTarget;

    private void Awake() {
        coconutBasketCollectionInit = new MoveToTarget[coconutBasketCollection.Length];
        for (int i = 0; i < coconutBasketCollectionInit.Length; i++) {
            coconutBasketCollectionInit[i] = coconutBasketCollection[i];
        }
    }

    private void OnEnable() {
        coconutBasketCollection = new MoveToTarget[coconutBasketCollectionInit.Length];
        for (int i = 0; i < coconutBasketCollectionInit.Length; i++) {
            coconutBasketCollection[i] = coconutBasketCollectionInit[i];
        }

        StopCoroutine("MoveBasket");
        StartCoroutine("MoveBasket");
    }

    public IEnumerator MoveBasket() {
        yield return new WaitForSeconds(basketMoveDelay);
        for (int i = 0; i < coconutBasketCollection.Length; i++) {
            if (i == 0) {
                coconutBasketCollection[i].transform.position = endTarget.position;
            } else
            if (i == 1) {
                coconutBasketCollection[i].Play(startTarget);
            } else {
                coconutBasketCollection[i].Play(coconutBasketCollection[i - 1].transform);
            }
        }

        MoveToTarget[] coconutBasketCollection2 = new MoveToTarget[coconutBasketCollection.Length];
        for (int i = 0; i < coconutBasketCollection2.Length; i++) {
            if (i == 0) {
                coconutBasketCollection2[i] = coconutBasketCollection[i + 1];
            } else
           if (i == coconutBasketCollection2.Length - 1) {
                coconutBasketCollection2[i] = coconutBasketCollection[0];
            } else {
                coconutBasketCollection2[i] = coconutBasketCollection[i + 1];
            }
        }
        coconutBasketCollection = coconutBasketCollection2;
        StartCoroutine("MoveBasket");
    }

    private CoconutBasket GetBasketByIndex(int index) {
        for (int i = 0; i < coconutBasketCollection.Length; i++) {
            CoconutBasket coconutBasket = coconutBasketCollection[i].GetComponent<CoconutBasket>();
            if (coconutBasket.index == index) return coconutBasket;
        }
        return null;
    }

    private void ChangeCollectionContent(MoveToTarget moveToTargetToPlace, int index) {
        coconutBasketCollection[index] = moveToTargetToPlace;
    }
}
